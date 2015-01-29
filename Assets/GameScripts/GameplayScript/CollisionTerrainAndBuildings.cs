using UnityEngine;
using System.Collections;

public class CollisionTerrainAndBuildings : MonoBehaviour {
	public static bool crashed = false;

	public Rigidbody explosionPlane;
	void OnCollisionEnter(Collision collision)
	{
		if (collision.rigidbody.name.Equals ("Player")) 
		{
			ExplosionInitiation explode = new ExplosionInitiation (GameObject.Find ("Player"),explosionPlane);
			explode.explosionDetectionWithPlane();
			crashed = true;
	
		}

		else if (collision.rigidbody.name.Equals("Enemy"))
		{
			ExplosionInitiation explode = new ExplosionInitiation (GameObject.Find ("Enemy"),explosionPlane);
			explode.explosionDetectionWithPlane();
			HUD.destroyed = true;
			Destroy(collision.gameObject,2.0f);
		}
	}
}
