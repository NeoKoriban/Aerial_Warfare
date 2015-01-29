using UnityEngine;
using System.Collections;

public class FogParticleSystemTerrain : MonoBehaviour {

	public static float fogDensity = 0.0001f;
	// Use this for initialization
	void Start () {
		LevelSettings settings = new LevelSettings ("settings");
		fogDensity *= settings.insertFogDensity ();
		RenderSettings.fog = true;

		if (fogDensity > 0.0010f)
			fogDensity = 0.0010f;

	}
	

	// Update is called once per frame
	void Update () {
		RenderSettings.fogDensity = fogDensity;
		RenderSettings.fogEndDistance = 5.0f;
	}
}
