using UnityEngine;
using System.Collections;

/**
 * Klasa odpowiedzialna za kolizję z pociskiem obiektu wroga.
 * 
 * @author Denis Wychowałek
 * @version 1.0
 * 
 * */
public class CollisionWithBullet : MonoBehaviour {

	public Rigidbody explosionEnemy;
	/**
	 * Liczba kolizji z pociskiem
	 * */
	private int bulletCollisions = 0;
	void OnTriggerEnter(Collider col)
	{
		if (col.rigidbody.name.Equals("bullet")) 
		{
		
			if (bulletCollisions.Equals(2))
			{
				HUD.destroyed = true;
				gameObject.GetComponent<Rigidbody>().useGravity = true;
				ExplosionInitiation explode = new ExplosionInitiation(gameObject,explosionEnemy);
				explode.explosionDetectionWithPlane();
			}
			else
			{
				HUD.collisionDetected = true;
			}
			bulletCollisions++;
		}

		else if (col.rigidbody.name.Equals("rocket"))
		{
			gameObject.GetComponent<Rigidbody>().useGravity = true;
			HUD.destroyed = true;
			HUD.ifRocket = true;
			HUD.collisionDetected= true;
			ExplosionInitiation explode = new ExplosionInitiation(gameObject,explosionEnemy);
			explode.explosionDetectionWithPlane();
		}

	}
}
