using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Klasa odpowiedzialna za mechanizm strzelania.
 * 
 * @author Denis Wychowałek
 * @version 1.0
 * 
 * */
public class ShootScript : MonoBehaviour 
{
	/**
	 * obiekt pocisku
	 * */
	private Rigidbody bulletObject;

	private Rigidbody rocketObject;
	/**
	 * prędkość pocisku
	 * */
	private float speedBullet = 800.0f;


	/**
	 * Model amunicji do działka typu minigun
	 * */
	GameObject miniGunStandard;

	/**
	 * Model samolotu
	 * */
	GameObject choosenPlane;

	/**
	 * Wartość odstępu pomiędzy pojedynczymi strzałami
	 * */
	private float nextTimeShoot = 0.5f;

	private float nextTimeRocketLaunched = 2.0f;
	/**
	 * Konstruktor dwuargumentowy, w którym przekazywane są obiekty
	 * samolotu i pocisku.
	 * Argumenty:
	 * 		GameObject aircraft - obiekt samolotu
	 * 		Rigidbody bullet - obiekt pocisku
	 * */
	public ShootScript(GameObject aircraft, Rigidbody bullet)
	{
		bulletObject = bullet;
		choosenPlane = aircraft;
	}

	public ShootScript(GameObject aircraft, Rigidbody bullet, Rigidbody rocket)
	{
		bulletObject = bullet;
		choosenPlane = aircraft;
		rocketObject = rocket; 
	}

	/**
	 * Funkcja tworząca wektor dla danego pocisku, który będzie potrzebny przy umiescowieniu
	 * jego punktu startu.
	 * Argumenty:
	 * 		float xPlane - współrzędna x samolotu
	 * 		float yPlane - współrzędna y samolotu
	 * 		float zPlane - współrzędna z samolotu
	 * 		float positionOnPlane - miejsce gdzie ma zostać umiejscowiony pocisk
	 * Zwraca:
	 * 		Vector3 potrzebny do umiejscowienia pocisku przy samolocie, który jest zarazem
	 * 		punktem startu pocisku.
	 * */
	private Vector3 bulletPlace(float xPlane, float yPlane, float zPlane, float positionOnPlane)
	{
		Vector3 newBulletVector = new Vector3 (xPlane+2.0f, yPlane, zPlane + positionOnPlane);
		return newBulletVector;
	}

	private Vector3 rocketPlace(float xPlane, float yPlane, float zPlane, float positionOnPlane)
	{
		Vector3 newRocketVector = new Vector3 (xPlane+2.0f, yPlane, zPlane + positionOnPlane);
		return newRocketVector;
	}

	private Rigidbody rocketCreated( float positionOnPlane)
	{
		Rigidbody newRocket = Instantiate (rocketObject.GetComponent<Rigidbody> (),rocketPlace (
		                                  choosenPlane.transform.position.x,
		                                  choosenPlane.transform.position.y,
		                                  choosenPlane.transform.position.z,
			positionOnPlane), Quaternion.Euler (0.0f, 90.0f, 0.0f)) as Rigidbody;
		if(choosenPlane.name.Equals("Player"))
			newRocket.name = "rocket";

		newRocket.collisionDetectionMode = CollisionDetectionMode.Continuous;
		newRocket.velocity = choosenPlane.transform.TransformDirection(new Vector3(speedBullet, 0.0f,0.0f));

		return newRocket;
	}
	/**
	 * Funkcja tworząca nowy pocisk, nazywająca go, umiejscawiająca i wprawiająca w ruch.
	 * Argumenty:
	 * 		float positionOnPlane - miejsce gdzie ma zostać umieszczony pocisk
	 * Zwraca:
	 * 		Rigidbody pocisku.
	 * */
	private Rigidbody bulletCreated(float positionOnPlane)
	{
		Rigidbody newBullet = Instantiate(bulletObject,bulletPlace(choosenPlane.transform.position.x,
		                                                           choosenPlane.transform.position.y,
		                                                           choosenPlane.transform.position.z,
		                                                           positionOnPlane),choosenPlane.transform.rotation) as Rigidbody;
		if (choosenPlane.name.Equals ("Player")) 
		{
			newBullet.name = "bullet";
		
		} 
		else 
		{
			newBullet.name = "bulletEnemy";

		}

		newBullet.collisionDetectionMode = CollisionDetectionMode.Continuous;
		newBullet.velocity = choosenPlane.transform.TransformDirection(new Vector3(speedBullet, 0.0f,0.0f));


		return newBullet;
	}

	/**
	 * Funkcja osługująca strzelanie przez wrogie jednostki.
	 * */
	public void shootFunctionForAI()
	{
		if (Time.time - 0.5f > nextTimeShoot) 
		{
			nextTimeShoot = Time.time - Time.deltaTime;
			Destroy(bulletCreated(-2.0f).gameObject,5.0f);
			Destroy(bulletCreated(2.0f).gameObject,5.0f);
		}
	}

	/**
	 * Funkcja odpowiadająca za wystrzelenie pocisku, wyboru pocisku, który ma zostać wystrzelony
	 * w zależności od naciśnietego przycisku, decyduje także o żywotności pocisku na scenie.
	 * */
	public void shootFunction()
	{

			if (Input.GetKey ("q")) 
			{
				if (Time.time - 0.5f > nextTimeShoot)
				{
					nextTimeShoot = Time.time - Time.deltaTime;
					Destroy(bulletCreated(-2.0f).gameObject,5.0f);
					Destroy(bulletCreated(2.0f).gameObject,5.0f);
				}
				
			}
			else if (Input.GetKey ("r")) 
			{
				if(HUD.numberOfRocket > 0)
					if (Time.time - 2.0f > nextTimeRocketLaunched)
					{
						nextTimeRocketLaunched = Time.time - Time.deltaTime;
						Destroy(rocketCreated(-4.0f).gameObject,5.0f);
						Destroy(rocketCreated(4.0f).gameObject,5.0f);
						HUD.numberOfRocket--;
					}
			}
	}


}