using UnityEngine;
using System.Collections;

/**
 * Klasa odpowiedzialna za wyświetlanie HUDa, informacji o zdrowiu,
 * liczbie samolotów do zestrzelenia, punktach, aktualnym poziomie.
 * 
 * @author Denis Wychowałek
 * @version 1.0
 * 
 * */
public class HUD : MonoBehaviour {

	/**
	 * Czy samolot został zestrzelony
	 * */
	public static bool destroyed = false;

	/**
	 * Czy zaistniała kolizja z samolotem
	 * */
	public static bool collisionDetected = false;

	/**
	 * Wartość aktualnego poziomu
	 * */
	public static int levelNumber = 1;

	/**
	 * Zmienna przechowująca liczbę punktów
	 * */
	private int pointsCounter = 0;
	public static bool ifRocket = false;
	/**
	 * 
	 * Liczba informująca ile samolotów jest do zestrzelenia podczas trwania poziomu
	 * */
	private int howMuchDestroyPlane = 0;

	public static int numberOfRocket = 10;
	/**
	 * Liczba samootów zestrzelonych
	 * */
	public int planeDestroyed = 0;

	/**
	 * Liczba samolotów niezestrzelonych w rundzie.
	 * */
	public static int restEnemy = AIScript.generateNewLevelEnemy;

	// Update is called once per frame
	void OnGUI() {
		if (destroyed.Equals(true))
		{
			planeDestroyed++;
			howMuchDestroyPlane++;
			destroyed = false;
			restEnemy--;
		}

		if (collisionDetected.Equals (true)) 
		{
			if(ifRocket.Equals(true))
			{
				pointsCounter+= 500;
				collisionDetected = false;
				ifRocket = false;
			}
				
			else
			{
				pointsCounter += 75;
				collisionDetected = false;
			}
		}

		if (restEnemy.Equals (0)) 
		{
			planeDestroyed = 0;
		}


		string levelText = "Level \t\t\t" + levelNumber.ToString ();
		GUI.Box (new Rect (20, 20, 200, 20), levelText);
		GUI.skin.box.alignment = TextAnchor.MiddleLeft;
		string pointsText = "Points \t\t\t" + pointsCounter.ToString ();
		GUI.Box (new Rect (20, 50, 200, 20), pointsText);
		string destroyedText = "Enemy \t\t" + planeDestroyed.ToString() + " / " + AIScript.generateNewLevelEnemy.ToString();
		GUI.Box (new Rect (20, 80, 200, 20), destroyedText);
		string liveText = "Live \t\t\t\t" + CollisionWithEnemyBullet.live.ToString();
		GUI.Box (new Rect (20, 110, 200, 20), liveText);
		string rocketsText = "Rockets \t\t" + numberOfRocket.ToString();
		GUI.Box (new Rect (20, 140, 200, 20), rocketsText);

		if (Input.GetKeyDown ("z") || CollisionTerrainAndBuildings.crashed.Equals(true)
		    || CollisionWithEnemyBullet.live.Equals(0) ) 
		{
			CollisionTerrainAndBuildings.crashed = false;
			CollisionEnvironoment.destroyPlayer = false;
			endGame ();
		}
	}

	/**
	 * Funkcja odpowiedzialna za obsługę zakończenia gry po zniszczeniu samolotu lub
	 * po wyjściu z gry do menu głównego.
	 * */
	public void endGame()
	{
		FileOperationScript deletePlaneChoosed = new FileOperationScript ("planeChoosen");
		deletePlaneChoosed.deleteTemporaryFile ();
		FileOperationScript fileOperations = new FileOperationScript("UserChoosen");
		FileOperationScript fileOperationsSave = new FileOperationScript (fileOperations.loadChoose());
		fileOperationsSave.saveResultsToFile(levelNumber, pointsCounter , howMuchDestroyPlane);

		Destroy(GameObject.Find ("Enemy"));

		pointsCounter = 0;
		howMuchDestroyPlane = 0;
		planeDestroyed = 0;

		Destroy(GameObject.Find("Player"));
		Application.LoadLevel (0);
	}
}
