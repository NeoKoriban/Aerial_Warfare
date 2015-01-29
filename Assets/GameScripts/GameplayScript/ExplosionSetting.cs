using UnityEngine;
using System.Collections;

public class ExplosionSetting : MonoBehaviour {
	private float spanTime = 0.1f;

	// Use this for initialization
	void Start () {
		particleEmitter.minSize = 500.0f;
		particleEmitter.maxSize = 750.0f;
		particleEmitter.localVelocity = new Vector3 (10.0f, 10.0f, 10.0f);
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time - 0.1f > spanTime) 
		{
			spanTime = Time.time - Time.deltaTime;
			particleEmitter.maxSize += 200.0f;
			particleEmitter.maxEnergy += 5.0f;
		}


	}
}
