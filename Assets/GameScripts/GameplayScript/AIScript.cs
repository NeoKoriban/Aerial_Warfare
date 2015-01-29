using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Klasa odpowiedzialna za generwoanie wrogów.
 * 
 * @author Denis Wychowałek
 * @version 1.0
 * 
 * */
public class AIScript : MonoBehaviour 
{
	public Rigidbody explosionEnemy;
	/**
	 * Początkowa wartość określającej liczby wrogów do wygenerowania
	 * */
	public static int generateNewLevelEnemy = 2;
	/**
	 * Środek mapy na osi z
	 * */
	private float zPlace = 1500.0f;
	/**
	 * Pocisk
	 * */
	public Rigidbody bulletRigidbody;
	/**
	 * Lot samolotu.
	 * */
	private AircraftFlightScript aircraft;
	/**
	 * Funkcja zajmująca się generacją listy przeciwników, którzy są stworzeni dla konkretnego
	 * poziomu.
	 * Argumenty:
	 * 		List<GameObject> opponentsFractionModels - lista z modelami przeciwników.
	 * 		int howMuchPlanes - jak duża ma być lista przeciwników.
	 * */
	private void generateEnemy (List<GameObject> opponentsFractionModels, int howMuchPlanes)
	{
		//Liczba modeli znajdująca się na liście z modelami przeciwnej frakcji do wybranej przez gracza
		int howMuchModelsList = opponentsFractionModels.Count;
		float zPlaceGeneration = 11000.0f;

		//Dodawanie przeciwnikow na listę
		for (int i = 0; i < howMuchPlanes; i++) 
		{

			//Numer samolotu, który ma zostać dodany do listy, wartość jest losowa
			int numberOfPlane = Random.Range(0,howMuchModelsList);

			GameObject newEnemy = Instantiate (opponentsFractionModels[numberOfPlane]) as GameObject;
			newEnemy.name = "Enemy";
			newEnemy.AddComponent("AIBehaviour");

			newEnemy.AddComponent<Rigidbody>();
			newEnemy.GetComponent<Rigidbody>().useGravity = false;
			newEnemy.AddComponent<MeshRenderer> ();
			newEnemy.AddComponent<BoxCollider> ();
			newEnemy.GetComponent<BoxCollider>().isTrigger = true;
			newEnemy.GetComponent<BoxCollider>().size = newEnemy.GetComponentInChildren<MeshFilter>().mesh.bounds.max;
			newEnemy.AddComponent<CapsuleCollider> ();
			newEnemy.GetComponent<CapsuleCollider>().isTrigger = false;
			newEnemy.GetComponent<CapsuleCollider>().height = newEnemy.GetComponentInChildren<MeshFilter>().mesh.bounds.max.y/2;
			newEnemy.GetComponent<CapsuleCollider>().radius = Mathf.Sqrt(Mathf.Pow (newEnemy.GetComponentInChildren<MeshFilter>().mesh.bounds.max.z/2,2) +
			                                                             Mathf.Pow (newEnemy.GetComponentInChildren<MeshFilter>().mesh.bounds.max.x/2,2))/2;
	
			
			newEnemy.GetComponent<AIBehaviour>().bulletObject = bulletRigidbody;
			newEnemy.GetComponent<AIBehaviour>().explosionEnemy = explosionEnemy;
			aircraft = new AircraftFlightScript (newEnemy);

			//ustalanie miejsca startowego, miejsce przy granicy mapy, z którego startuje jest wartością losową
			zPlaceGeneration += 1050.0f;

			//wektor pozycji startowej.

			Vector3 vectorStartPosition = new Vector3 (12600.0f, 700.0f,zPlaceGeneration);
			if (vectorStartPosition.z == 0.0f)
				vectorStartPosition.z = 50.0f;
			zPlace += 100.0f;
			aircraft.setPositionScaleAndRotate(vectorStartPosition);
				
		}
	}
	

	// Use this for initialization
	void Start () {
		HUD.restEnemy = generateNewLevelEnemy;

		generateNewLevelEnemy = 2;
		LevelSettings settings = new LevelSettings ("settings");
		generateNewLevelEnemy = settings.insertCountOfEnemy ();
		HUD.levelNumber = 1;
		FileOperationScript fileOperations = new FileOperationScript ("planeChoosen");
		generateEnemy (fileOperations.createEnemyList(fileOperations.loadChoose()), generateNewLevelEnemy);
	}


	// Update is called once per frame
	void Update () {
		if(HUD.restEnemy.Equals(0))
		{
			if (HUD.numberOfRocket < 10)
				HUD.numberOfRocket+=2;
			else if (HUD.numberOfRocket == 9)
				HUD.numberOfRocket++;
			FogParticleSystemTerrain.fogDensity+= 0.0001f;
			if (generateNewLevelEnemy < 10)
				generateNewLevelEnemy+=2;
			HUD.restEnemy = generateNewLevelEnemy;
			FileOperationScript fileOperations = new FileOperationScript ("planeChoosen");
			HUD.levelNumber++;
			generateEnemy (fileOperations.createEnemyList(fileOperations.loadChoose()), generateNewLevelEnemy);
		

		}


	}
}