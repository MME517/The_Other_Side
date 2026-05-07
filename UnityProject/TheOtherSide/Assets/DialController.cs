using UnityEngine;
using TMPro;

public class DialController : MonoBehaviour
{
    [Header("Settings")]
    public int dialIndex       = 1;
    public int totalPositions  = 4;
    public int currentPosition = 0;
    public int correctPosition = 0;

    [Header("References")]
    public PuzzleManager   puzzleManager;
    public TextMeshProUGUI displayLabel;

    [Header("Visuals")]
    public Color correctTint = new Color(0.2f, 1f, 0.4f);
    public Color defaultTint = Color.white;

    [Header("Accessibility")]
    public Color labelCorrectColor = new Color(0.1f, 0.9f, 0.3f);
    public Color labelDefaultColor = Color.white;

    private Renderer              _rend;
    private MaterialPropertyBlock _mpb;
    private float                 _lastClickTime;

    private static readonly int BaseColorID = Shader.PropertyToID("_BaseColor");

    void Start()
    {
        _rend = GetComponent<Renderer>();
        _mpb  = new MaterialPropertyBlock();
        UpdateDisplay();
    }

    public void OnDialClicked()
    {
        if (Time.time - _lastClickTime < 0.35f) return;
        _lastClickTime  = Time.time;
        currentPosition = (currentPosition + 1) % totalPositions;
        UpdateDisplay();
        puzzleManager?.CheckDials();
    }

    void UpdateDisplay()
    {
        string[] symbols = { "I", "II", "III", "IV" };
        bool correct = currentPosition == correctPosition;

        if (displayLabel != null)
        {
            displayLabel.text  = symbols[currentPosition];
            displayLabel.color = correct ? labelCorrectColor : labelDefaultColor;
        }

        if (_rend != null)
        {
            _rend.GetPropertyBlock(_mpb);
            _mpb.SetColor(BaseColorID, correct ? correctTint : defaultTint);
            _rend.SetPropertyBlock(_mpb);
        }
    }

    public bool IsCorrect() => currentPosition == correctPosition;
}
