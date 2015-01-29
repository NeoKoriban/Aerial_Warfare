using UnityEngine;
using System.Collections;

/**
 * Klasa odpowiedzialna za lot samolotu gracza.
 * 
 * @author Denis Wychowałek
 * @version 1.0
 * 
 * */
public class PlayerAircraftMove : MonoBehaviour {
	/**
	 * Liczba punktów życia gracza.
	 * */
	public static int live = 5;

	/**
	 * Obiekt samolotu.
	 * */
	private GameObject planeObject;

	/**
	 * Obiekt pocisku.
	 * */
	public Rigidbody bulletObject;

	public Rigidbody rocketObject;
	/**
	 * Klasa obsługująca ruch samolotu podczas gry.
	 * */
	private AircraftFlightScript aircraft;

	/**
	 * Obsługa strzelania.
	 * */
	private ShootScript shoot;

	/**
	 * Obsługa kolizji z otoczeniem.
	 * */
	private CollisionEnvironoment collisionWithEnv;

	/**
	 * Funkcja odpowiadająca za ruch i za ustawienie startowych parametrów.
	 * */
	private void aircraftMove()
	{

		aircraft = new AircraftFlightScript (planeObject);
		aircraft.setPositionScaleAndRotate(aircraft.prepareVector3(12500.0f,700.0f,12565.0f));
		GameObject cameraObject = GameObject.Find ("Camera");
		aircraft.setPositionCamera(cameraObject,aircraft.prepareVector3 (-100.0f,5.0f,0.0f));

	}

	// Use this for initialization
	void Start () {
		//Sprawdzanie wyboru samolotu przez gracza.
		FileOperationScript fileOperation = new FileOperationScript ("planeChoosen");
		//Odnalezienie samolotu gracza
		planeObject = GameObject.Find (fileOperation.loadChoose());
		//Zmiana nazwy obiektu
		planeObject.name = "Player";
		planeObject.AddComponent<Rigidbody> ();
		planeObject.GetComponent<Rigidbody> ().useGravity = false;
		planeObject.AddComponent<MeshRenderer> ();
		planeObject.AddComponent<BoxCollider> ();
		planeObject.GetComponent<BoxCollider> ().size = planeObject.GetComponentInChildren<MeshRenderer> ().renderer.bounds.max;
		planeObject.GetComponent<BoxCollider> ().isTrigger = true;
		planeObject.AddComponent<CapsuleCollider> ();
		planeObject.GetComponent<CapsuleCollider> ().isTrigger = false;
		planeObject.GetComponent<CapsuleCollider> ().height = planeObject.GetComponentInChildren<MeshRenderer> ().renderer.bounds.max.y;
		planeObject.GetComponent<CapsuleCollider> ().radius = Mathf.Sqrt(Mathf.Pow (planeObject.GetComponentInChildren<MeshRenderer> ().renderer.bounds.max.z/2,2) +
															  Mathf.Pow (planeObject.GetComponentInChildren<MeshRenderer> ().renderer.bounds.max.x/2,2))/2;

		aircraftMove();
		shoot = new ShootScript (planeObject, bulletObject, rocketObject);
		collisionWithEnv = new CollisionEnvironoment (planeObject);
		planeObject.AddComponent<CollisionWithEnemyBullet> ();
	}

	// Update is called once per frame
	void Update () {
		aircraft.moveAircraft();
		shoot.shootFunction ();
		collisionWithEnv.collisionWithTerrainDetected ();
	}
}
