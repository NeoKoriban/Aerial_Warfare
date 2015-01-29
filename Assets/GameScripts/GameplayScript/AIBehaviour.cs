using UnityEngine;
using System.Collections;
/**
 * Klasa odpowiedzialna za zachowanie wroga.
 * 
 * @author Denis Wychowałek
 * @version 1.0
 * 
 * */
public class AIBehaviour : MonoBehaviour {

	/**
	 * Kolizja z otoczeniem
	 * */
	private CollisionEnvironoment collisionEnv;

	/**
	 * Rigidbody pocisku
	 * */
	public Rigidbody bulletObject;

	public Rigidbody explosionEnemy;

	/**
	 * GameObject samolotu gracza
	 * */
	private GameObject playerAircraft;

	/**
	 * Strzelanie
	 * */
	private ShootScript shoot;

	/**
	 * Latanie
	 * */
	private AircraftFlightScript aircraft;

	/**
	 * Liczba wykrytych kolizji
	 * */
	public int collistionDetected = 0;

	/**
	 * Funkcja znajdująca obiekt samolotu gracza na liście obiektów.
	 * */
	private void findPlayerAircraft()
	{
		playerAircraft = GameObject.Find ("Player");
	}
	
	/**
	 * Funkcja odpowiedzialna za mechanizm ataku przez przeciwników gracza i podążanie za nim w momencie odpowiedniego dystansu.
	 * */
	private void followPlayer()
	{

		if (Vector3.Distance (playerAircraft.transform.position, transform.position) <= 50.0f) {
			transform.rotation = Quaternion.Slerp (transform.rotation,
			                                       Quaternion.LookRotation (playerAircraft.transform.position -
			                         transform.position), 2.5f* Time.deltaTime);
			shoot.shootFunctionForAI ();
			transform.position = Vector3.MoveTowards (transform.position, playerAircraft.transform.position, Time.deltaTime);
		} else if (Vector3.Distance (playerAircraft.transform.position, transform.position) <= 300.0f)
		{
			shoot.shootFunctionForAI ();
		}
	}
	
	void Awake()
	{
		playerAircraft = GameObject.FindGameObjectWithTag ("Player");
	}

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<BoxCollider> ().size *= 2.5f;
		gameObject.GetComponent<CapsuleCollider>().height *= 2.5f;
		gameObject.GetComponent<CapsuleCollider>().radius *= 2.5f;

		gameObject.AddComponent<CollisionWithBullet>();
		gameObject.GetComponent<CollisionWithBullet> ().explosionEnemy = explosionEnemy;
		collisionEnv = new CollisionEnvironoment(gameObject);
		findPlayerAircraft ();
		shoot = new ShootScript (gameObject, bulletObject);
		aircraft = new AircraftFlightScript (gameObject);
	

		
	}
	
	// Update is called once per frame
	void Update () {


		collisionEnv.collisionWithTerrainDetected();
		aircraft.moveAircraftAI ();
		
		followPlayer ();
		transform.Translate (2.0f, 0.0f, 0.0f);

	}

}
