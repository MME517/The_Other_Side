using UnityEngine;

public class EmissivePulse : MonoBehaviour
{
	public Color baseColor = new Color(0f, 0.9f, 1f);
	public float minIntensity = 0.8f;
	public float maxIntensity = 2.5f;
	public float pulseSpeed = 1.2f;

	private Renderer rend;
	private Material mat;

	void Start()
	{
		rend = GetComponent<Renderer>();
		mat = new Material(rend.material);
		rend.material = mat;
	}

	void Update()
	{
		float t = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f;
		float intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
		mat.SetColor("_EmissionColor", baseColor * intensity);
	}
}