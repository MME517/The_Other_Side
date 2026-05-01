using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
	public TextMeshProUGUI puzzle1Text;
	public TextMeshProUGUI puzzle2Text;
	public TextMeshProUGUI puzzle3Text;

	void Start()
	{
		puzzle1Text.text = "I. THE OFFERING   [ ] [ ] [ ]";
		puzzle2Text.text = "II. THE SCALES     <color=#9090B0>LOCKED</color>";
		puzzle3Text.text = "III. THE CARTOUCHE <color=#9090B0>LOCKED</color>";
	}

	public void UpdatePuzzle1(int placed)
	{
		string slots = "";
		for (int i = 0; i < 3; i++)
			slots += i < placed ? "<color=#06D6A0>[✓]</color> " : "[ ] ";
		puzzle1Text.text = "I. THE OFFERING   " + slots;
		if (placed >= 3) puzzle2Text.text = "II. THE SCALES     <color=#FFD166>ACTIVE</color>";
	}

	public void UpdatePuzzle2(int placed)
	{
		string slots = "";
		for (int i = 0; i < 2; i++)
			slots += i < placed ? "<color=#06D6A0>[✓]</color> " : "[ ] ";
		puzzle2Text.text = "II. THE SCALES     " + slots;
		if (placed >= 2) puzzle3Text.text = "III. THE CARTOUCHE <color=#FFD166>ACTIVE</color>";
	}

	public void UpdatePuzzle3(int correct)
	{
		string slots = "";
		for (int i = 0; i < 3; i++)
			slots += i < correct ? "<color=#06D6A0>[✓]</color> " : "[ ] ";
		puzzle3Text.text = "III. THE CARTOUCHE " + slots;
	}
}