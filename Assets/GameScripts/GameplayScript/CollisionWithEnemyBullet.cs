using UnityEngine;
using System.Collections;

public class CollisionWithEnemyBullet : MonoBehaviour {

	public static int live = 5;
	void OnTriggerEnter(Collider col)
	{
		if (col.rigidbody.name.Equals("bulletEnemy")) 
		{
			
			if (live.Equals(0))
			{
				gameObject.GetComponent<Rigidbody>().useGravity = true;
			}
			else
			{
				live--;
			}

		}
		
	}
}
