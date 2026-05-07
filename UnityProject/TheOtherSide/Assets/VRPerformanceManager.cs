using System.Collections.Generic;
using UnityEngine;

public class VRPerformanceManager : MonoBehaviour
{
    [Header("Platform")]
    public VRPlatform targetPlatform = VRPlatform.PCVRorQuest;

    [Header("FPS Monitoring")]
    public float targetFPS = 72f;
    public float downgradeCooldown = 3f;
    public float upgradeCooldown = 10f;

    [Header("Quality Tiers")]
    public int qualityHigh   = 2;
    public int qualityMedium = 1;
    public int qualityLow    = 0;

    [Header("Environment Batching")]
    public GameObject environmentRoot;

    [Header("Draw Call Budget")]
    public int drawCallBudget = 150;

    [Header("Debug")]
    public bool showFPSOverlay = true;

    public enum VRPlatform { MobileVR, PCVRorQuest }
    private enum QualityTier { High, Medium, Low }

    private float       _fpsAcc;
    private int         _fpsFrames;
    private float       _fps;
    private float       _timeLow, _timeHigh;
    private QualityTier _tier = QualityTier.High;
    private GUIStyle    _guiStyle;

    void Awake()
    {
        ConfigureVRSettings();
        EnableGPUInstancing();
        RunStaticBatching();
        ValidateScene();
    }

    void Update()
    {
        MeasureFPS();
        AdaptQuality();
    }

    void ConfigureVRSettings()
    {
        QualitySettings.vSyncCount  = 0;
        Application.targetFrameRate = targetPlatform == VRPlatform.MobileVR ? 72 : 90;

        if (targetPlatform == VRPlatform.MobileVR)
        {
            QualitySettings.shadowDistance = 12f;
            QualitySettings.shadowCascades = 0;
            QualitySettings.realtimeReflectionProbes = false;
        }

        SetQualityTier(QualityTier.High, force: true);
        Debug.Log($"[VRPerformanceManager] Started — {targetPlatform}, target {targetFPS} FPS, " +
                  $"quality: {QualitySettings.names[QualitySettings.GetQualityLevel()]}");
    }

    void EnableGPUInstancing()
    {
        var seen = new HashSet<int>();
        int total = 0, enabled = 0;

        foreach (Renderer r in FindObjectsByType<Renderer>(FindObjectsSortMode.None))
        {
            foreach (Material m in r.sharedMaterials)
            {
                if (m == null) continue;
                if (!seen.Add(m.GetInstanceID())) continue;
                total++;
                if (!m.enableInstancing) { m.enableInstancing = true; enabled++; }
            }
        }
        Debug.Log($"[VRPerformanceManager] GPU Instancing: {enabled}/{total} materials enabled");
    }

    void RunStaticBatching()
    {
        if (environmentRoot == null) return;
        StaticBatchingUtility.Combine(environmentRoot);
        Debug.Log($"[VRPerformanceManager] Static batching applied to '{environmentRoot.name}'");
    }

    void ValidateScene()
    {
        foreach (Camera cam in Camera.allCameras)
            if (!cam.useOcclusionCulling)
                Debug.LogWarning($"[VRPerformanceManager] '{cam.name}' has Occlusion Culling OFF");

        int active = 0;
        foreach (Renderer r in FindObjectsByType<Renderer>(FindObjectsSortMode.None))
            if (r.enabled && r.gameObject.activeInHierarchy) active++;

        string status = active <= drawCallBudget ? "WITHIN BUDGET" : "OVER BUDGET";
        Debug.Log($"[VRPerformanceManager] Renderers: {active}/{drawCallBudget} — {status}");
    }

    void MeasureFPS()
    {
        _fpsAcc += Time.unscaledDeltaTime;
        _fpsFrames++;
        if (_fpsAcc >= 0.5f)
        {
            _fps       = _fpsFrames / _fpsAcc;
            _fpsAcc    = 0f;
            _fpsFrames = 0;
        }
    }

    void AdaptQuality()
    {
        if (_fps <= 0f) return;
        bool low  = _fps < targetFPS * 0.90f;
        bool high = _fps > targetFPS * 1.05f;

        if (low)       { _timeLow  += Time.unscaledDeltaTime; _timeHigh  = 0f; }
        else if (high) { _timeHigh += Time.unscaledDeltaTime; _timeLow   = 0f; }
        else
        {
            _timeLow  = Mathf.Max(0, _timeLow  - Time.unscaledDeltaTime);
            _timeHigh = Mathf.Max(0, _timeHigh - Time.unscaledDeltaTime);
        }

        if (_timeLow  >= downgradeCooldown) { _timeLow  = 0; TryDowngrade(); }
        if (_timeHigh >= upgradeCooldown)   { _timeHigh = 0; TryUpgrade();   }
    }

    void TryDowngrade()
    {
        if      (_tier == QualityTier.High)   SetQualityTier(QualityTier.Medium);
        else if (_tier == QualityTier.Medium) SetQualityTier(QualityTier.Low);
        else Debug.LogWarning("[VRPerformanceManager] Already at lowest quality tier.");
    }

    void TryUpgrade()
    {
        if      (_tier == QualityTier.Low)    SetQualityTier(QualityTier.Medium);
        else if (_tier == QualityTier.Medium) SetQualityTier(QualityTier.High);
    }

    void SetQualityTier(QualityTier tier, bool force = false)
    {
        if (_tier == tier && !force) return;
        _tier = tier;
        int idx = tier == QualityTier.High ? qualityHigh :
                  tier == QualityTier.Medium ? qualityMedium : qualityLow;
        if (idx >= 0 && idx < QualitySettings.names.Length)
        {
            QualitySettings.SetQualityLevel(idx, applyExpensiveChanges: true);
            Debug.Log($"[VRPerformanceManager] Quality changed to {tier} | FPS: {_fps:0}");
        }
    }

    void OnGUI()
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (!showFPSOverlay) return;
        if (_guiStyle == null)
        {
            _guiStyle           = new GUIStyle(GUI.skin.box);
            _guiStyle.fontSize  = 20;
            _guiStyle.alignment = TextAnchor.MiddleLeft;
        }
        _guiStyle.normal.textColor = _fps >= targetFPS        ? Color.green  :
                                     _fps >= targetFPS * 0.85f ? Color.yellow : Color.red;
        GUI.Box(new Rect(10, 10, 220, 55), $"FPS: {_fps:0}  Quality: {_tier}", _guiStyle);
#endif
    }
}
