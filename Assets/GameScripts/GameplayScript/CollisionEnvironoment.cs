using UnityEngine;
using System.Collections;

/**
 * Klasa odpowiedzialna za kolizję z otoczeniem.
 * 
 * @author Denis Wychowałek
 * @version 1.0
 * 
 * */
public class CollisionEnvironoment : MonoBehaviour {

	public static bool destroyPlayer = false;
	/**
	 * Model samolotu
	 * */ 
	private GameObject planeObject;
	/**
	 * Stała określająca granicę mapy xBegin
	 * */
	private const float xBegin = -20.0f;
	/**
	 * Stała określająca granicę mapy zBegin
	 * */
	private const float zBegin = -20.0f;
	/**
	 * Stała określająca granicę mapy xEnd
	 * */
	private const float xEnd = 25000.0f;
	/**
	 * Stała określająca granicę mapy xEnd
	 * */
	private const float zEnd = 25000.0f;

	/**
	 * Konstruktor jednoargumentowy, przyjmujący jako argument GameObject samolotu.
	 * Argumenty:
	 * 		GameObject aircraft - obiekt samolotu
	 * */
	public CollisionEnvironoment(GameObject aircraft)
	{
		planeObject = aircraft;
	}

	/**
	 * Funkcja obsługująca kolizje z terenem oraz sprawdza czy samolot wykroczył poza teren.
	 * */
	public void collisionWithTerrainDetected()
	{
		float terrainBorder = Terrain.activeTerrain.SampleHeight (planeObject.transform.position);
		GameObject terrain = GameObject.Find ("Terrain");
		terrain.GetComponent<TerrainCollider> ().isTrigger = false;

		//Sprawdza czy samolot nie wykroczył poza teren o współrzędnej xEnd
		if (xEnd < planeObject.transform.position.x) 
		{
			planeObject.transform.position = new Vector3 (xBegin,
			                                              planeObject.transform.position.y,
			                                              planeObject.transform.position.z);
		}
		//Sprawdza czy samolot nie wykroczył poza teren o współrzędnej xBegin
		else if (xBegin > planeObject.transform.position.x)
			planeObject.transform.position = new Vector3 (xEnd,
			                                              planeObject.transform.position.y,
			                                              planeObject.transform.position.z);

		//Sprawdza czy samolot nie wykroczył poza teren o współrzędnej zEnd
		else if (zEnd < planeObject.transform.position.z) 
			planeObject.transform.position = new Vector3 (planeObject.transform.position.x,
			                                              planeObject.transform.position.y,
			                                              zBegin);

		//Sprawdza czy samolot nie wykroczył poza teren o współrzędnej zBegin
		else if (zBegin > planeObject.transform.position.z)
			planeObject.transform.position = new Vector3 ( planeObject.transform.position.x,
			                                              planeObject.transform.position.y,
			                                              zEnd);
	}


}