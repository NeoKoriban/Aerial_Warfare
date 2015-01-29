using UnityEngine;
using System.Collections;

public class ExplosionInitiation : MonoBehaviour {

	private GameObject collideObject;

	private Rigidbody explosionParticle;


	public ExplosionInitiation(GameObject collisionObject, Rigidbody explosionPart)
	{
		collideObject = collisionObject;
		explosionParticle = explosionPart;
	}

	public void explosionDetectionWithPlane ()
	{
		Rigidbody explosion = Instantiate(explosionParticle,collideObject.transform.position,Quaternion.identity) as Rigidbody;
		Destroy (collideObject,0.5f);
	}
}
