using UnityEngine;

public class EmissivePulse : MonoBehaviour
{
    public Color baseColor    = new Color(0f, 0.9f, 1f);
    public float minIntensity = 0.8f;
    public float maxIntensity = 2.5f;
    public float pulseSpeed   = 1.2f;

    private Renderer              _rend;
    private MaterialPropertyBlock _mpb;

    private static readonly int EmissionColorID = Shader.PropertyToID("_EmissionColor");

    void Awake()
    {
        _rend = GetComponent<Renderer>();
        _mpb  = new MaterialPropertyBlock();

        if (_rend != null && _rend.sharedMaterial != null)
            _rend.sharedMaterial.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        if (_rend == null) return;

        float t         = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f;
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, t);

        _rend.GetPropertyBlock(_mpb);
        _mpb.SetColor(EmissionColorID, baseColor * intensity);
        _rend.SetPropertyBlock(_mpb);
    }

    void OnDisable()
    {
        if (_rend == null) return;
        _rend.GetPropertyBlock(_mpb);
        _mpb.SetColor(EmissionColorID, Color.black);
        _rend.SetPropertyBlock(_mpb);
    }
}
