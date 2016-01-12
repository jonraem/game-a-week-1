using UnityEngine;
using System.Collections;

public class PulsingLight : MonoBehaviour {

	private Light myLight;
	public float maxIntensity = 5f;
	public float minIntensity = 0f;
	public float pulseSpeed = 1f;
	private float targetIntensity = 1f;
	private float currentIntensity;

	// Use this for initialization
	void Start () {
		myLight = GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		currentIntensity = Mathf.MoveTowards (myLight.intensity, targetIntensity, Time.deltaTime * pulseSpeed);
		if (currentIntensity >= maxIntensity) {
			currentIntensity = maxIntensity;
			targetIntensity = minIntensity;
		} else if (currentIntensity <= minIntensity) {
			currentIntensity = minIntensity;
			targetIntensity = maxIntensity;
		}
		myLight.intensity = currentIntensity;
	}
}
