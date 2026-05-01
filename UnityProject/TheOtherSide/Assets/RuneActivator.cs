using UnityEngine;

public class RuneActivator : MonoBehaviour
{
	public Material dimMaterial;
	public Material activeMaterial;
	private Renderer rend;

	void Start()
	{
		rend = GetComponent<Renderer>();
		rend.material = dimMaterial;
	}

	public void Activate()
	{
		rend.material = activeMaterial;
		EmissivePulse pulse = gameObject.AddComponent<EmissivePulse>();
		pulse.baseColor = new Color(1f, 0.78f, 0f);
		pulse.minIntensity = 1.5f;
		pulse.maxIntensity = 4.0f;
		pulse.pulseSpeed = 2.5f;
	}
}