using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;


public class PuzzleManager : MonoBehaviour
{
	[Header("Puzzle 1 — The Offering")]
	public int fragmentsPlaced = 0;
	public int fragmentsRequired = 3;

	[Header("Puzzle 2 — The Scales")]
	public int orbsPlaced = 0;
	public int orbsRequired = 2;

	[Header("Puzzle 3 — The Cartouche")]
	public DialController dial1;
	public DialController dial2;
	public DialController dial3;

	[Header("Doors")]
	public GameObject door_Puzzle1;
	public GameObject door_Puzzle2;
	public GameObject door_Exit;

	[Header("Runes")]
	public RuneActivator[] puzzle1Runes;
	public RuneActivator[] puzzle2Runes;
	public RuneActivator[] puzzle3Runes;

	[Header("Particles")]
	public ParticleSystem altarParticles;
	public ParticleSystem scalesParticles;
	public ParticleSystem exitParticles;

	[Header("Audio")]
	public AudioSource audioSource;
	public AudioClip placeSound;
	public AudioClip puzzle1Sound;
	public AudioClip puzzle2Sound;
	public AudioClip completionSound;

	[Header("UI")]
	public GameObject completionCanvas;
	public HUDController hud;

	private bool p1Done = false;
	private bool p2Done = false;
	private bool p3Done = false;

	// ── Puzzle 1 ──────────────────────────────────────────────────────
	public void OnFragmentPlaced()
	{
		if (p1Done) return;
		fragmentsPlaced++;
		PlaySound(placeSound);
		if (fragmentsPlaced <= puzzle1Runes.Length)
			puzzle1Runes[fragmentsPlaced - 1].Activate();
		hud?.UpdatePuzzle1(fragmentsPlaced);

		if (fragmentsPlaced >= fragmentsRequired)
		{
			p1Done = true;
			StartCoroutine(CompletePuzzle1());
		}
	}

	IEnumerator CompletePuzzle1()
	{
		yield return new WaitForSeconds(0.3f);
		PlaySound(puzzle1Sound);
		if (altarParticles != null) altarParticles.Play();
		yield return new WaitForSeconds(0.8f);
		if (door_Puzzle1 != null) door_Puzzle1.SetActive(false);
	}

	// ── Puzzle 2 ──────────────────────────────────────────────────────
	public void OnOrbPlaced()
	{
		if (p2Done) return;
		orbsPlaced++;
		PlaySound(placeSound);
		if (orbsPlaced <= puzzle2Runes.Length)
			puzzle2Runes[orbsPlaced - 1].Activate();
		hud?.UpdatePuzzle2(orbsPlaced);

		if (orbsPlaced >= orbsRequired)
		{
			p2Done = true;
			StartCoroutine(CompletePuzzle2());
		}
	}

	IEnumerator CompletePuzzle2()
	{
		yield return new WaitForSeconds(0.3f);
		PlaySound(puzzle2Sound);
		if (scalesParticles != null) scalesParticles.Play();
		yield return new WaitForSeconds(0.8f);
		if (door_Puzzle2 != null) door_Puzzle2.SetActive(false);
	}

	// ── Puzzle 3 ──────────────────────────────────────────────────────
	public void CheckDials()
	{
		if (p3Done || !p2Done) return;
		int correct = (dial1 != null && dial1.IsCorrect() ? 1 : 0)
					+ (dial2 != null && dial2.IsCorrect() ? 1 : 0)
					+ (dial3 != null && dial3.IsCorrect() ? 1 : 0);
		hud?.UpdatePuzzle3(correct);
		for (int i = 0; i < correct && i < puzzle3Runes.Length; i++)
			puzzle3Runes[i].Activate();

		if (dial1.IsCorrect() && dial2.IsCorrect() && dial3.IsCorrect())
		{
			p3Done = true;
			StartCoroutine(CompleteAll());
		}
	}

	IEnumerator CompleteAll()
	{
		yield return new WaitForSeconds(0.3f);
		PlaySound(completionSound);
		if (exitParticles != null) exitParticles.Play();
		StartCoroutine(CameraShake(0.5f, 0.012f));
		yield return new WaitForSeconds(1.0f);
		if (door_Exit != null) door_Exit.SetActive(false);
		yield return new WaitForSeconds(0.3f);
		if (completionCanvas != null) completionCanvas.SetActive(true);
	}

	IEnumerator CameraShake(float duration, float magnitude)
	{
		Transform cam = Camera.main.transform;
		Vector3 orig = cam.localPosition;
		float elapsed = 0f;
		while (elapsed < duration)
		{
			cam.localPosition = orig + new Vector3(
				Random.Range(-magnitude, magnitude),
				Random.Range(-magnitude, magnitude), 0);
			elapsed += Time.deltaTime;
			yield return null;
		}
		cam.localPosition = orig;
	}

	void PlaySound(AudioClip clip)
	{
		if (audioSource != null && clip != null)
			audioSource.PlayOneShot(clip);
	}
}