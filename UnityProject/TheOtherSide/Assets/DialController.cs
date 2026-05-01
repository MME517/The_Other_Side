using UnityEngine;
using TMPro;

public class DialController : MonoBehaviour
{
	[Header("Settings")]
	public int dialIndex = 1;
	public int totalPositions = 4;
	public int currentPosition = 0;
	public int correctPosition = 0;

	[Header("References")]
	public PuzzleManager puzzleManager;
	public TextMeshProUGUI displayLabel;

	[Header("Visuals")]
	public Material defaultMat;
	public Material correctMat;

	private Renderer rend;
	private float lastClickTime = 0f;

	void Start()
	{
		rend = GetComponent<Renderer>();
		UpdateDisplay();
	}

	public void OnDialClicked()
	{
		if (Time.time - lastClickTime < 0.35f) return;
		lastClickTime = Time.time;
		currentPosition = (currentPosition + 1) % totalPositions;
		UpdateDisplay();
		puzzleManager?.CheckDials();
	}

	void UpdateDisplay()
	{
		string[] symbols = { "I", "II", "III", "IV" };
		if (displayLabel != null)
			displayLabel.text = symbols[currentPosition];
		bool correct = currentPosition == correctPosition;
		if (rend != null)
			rend.material = correct ? correctMat : defaultMat;
	}

	public bool IsCorrect() => currentPosition == correctPosition;
}