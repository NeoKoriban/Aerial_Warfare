using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/**
 * Klasa odpowiedzialna za obsługę menu wyboru samolotu do gry.
 * 
 * @author Denis Wychowałek
 * @version 1.0
 * */
public class PlaneMenu : MonoBehaviour {
	
	/**
	 * Instancja  klasy, która odpowiada za obsługę zapisania danych do pliku
	 * */
	private FileOperationScript fileOperations;

	/**
	 * Stała przechowująca rozmiar czcionki
	 * */
	private const int fontSize = 30;
	/**
	 * Zmienna przechowująca informację o stopniu gęstości mgły.
	 * */
	private string densityText = "0";

	/**
	 * Zmienna przechowująca informację o liczbie przeciwników.
	 * */
	private string enemyText = "2";

	/**
	 * Zmienna przechowująca nazwę czcionki
	 * */
	private const string fontName = "HELVETICA";

	/**
	 * Funkcja odpowiedzialna za ustawienia wyglądu napisów i pól tekstowych.
	 * */
	private void skinSettings()
	{
		GUI.skin.label.fontSize = fontSize;
		GUI.skin.label.normal.textColor = Color.black;
		GUI.skin.label.font = Resources.Load (fontName) as Font;
		GUI.skin.label.normal.textColor = Color.grey;
		
		GUI.skin.textField.font = Resources.Load (fontName) as Font;
		GUI.skin.textField.fontSize = fontSize;
		GUI.skin.textField.normal.textColor = Color.grey;
		GUI.skin.textField.focused.textColor = Color.black;
		
		GUI.skin.textField.focused.background = Resources.Load ("Button/panelHover") as Texture2D;
		GUI.skin.textField.normal.background = Resources.Load ("Button/panel") as Texture2D;
		GUI.skin.textField.hover.textColor = Color.black;
		GUI.skin.textField.hover.background = Resources.Load ("Button/panelHover") as Texture2D;
	}
	void OnGUI()
	{
		skinSettings ();

		fileOperations = new FileOperationScript("planeChoosen");
		GUI.Label (new Rect (400, 660, 100, 40), "Fog:");
		densityText = GUI.TextField (new Rect (500, 660, 50, 40), densityText,2);
		GUI.Label (new Rect (700, 660, 130, 40), "Enemy:");
		enemyText = GUI.TextField (new Rect (820, 660, 50, 40), enemyText,2);
		if (File.Exists ("planeChoosen")) 
		{

			GUI.Box (new Rect (600, 50, 350, 30), "You choosed " + fileOperations.loadChoose());

		}
		else
		{
			GUI.Box (new Rect (600, 50, 350, 30), "Choose one plane and press button!");

		}
		if (GUI.Button (new Rect (20,650,200, 60), "Main Menu"))
		{
			if(File.Exists("planeChoosen"))
				Destroy (GameObject.Find (fileOperations.loadChoose()));
			Application.LoadLevel(0);
		}
		else if (GUI.Button (new Rect (1050,650,200, 60), "Start Game"))
		{
			if(File.Exists("planeChoosen"))
			{
				FileOperationScript saveSettings = new FileOperationScript("settings");
				saveSettings.saveSettings(densityText,enemyText);
				GUI.Box (new Rect (300, 650, 200, 30), "Loading ... ");
				Application.LoadLevel (2);
			}
		}

		else if (GUI.Button(new Rect (100,500,240, 60), "Su-35s"))
		{
			DontDestroyOnLoad(GameObject.Find ("su35s"));
			fileOperations.saveChoose("su35s");
		}
		else if (GUI.Button(new Rect (400,500,240, 60), "PAK T-50"))
		{
			DontDestroyOnLoad(GameObject.Find ("PAK T-50"));
			fileOperations.saveChoose("PAK T-50");
		}
		else if (GUI.Button(new Rect (700,500,240, 60), "F-16"))
		{
			DontDestroyOnLoad(GameObject.Find ("f16"));
			fileOperations.saveChoose("f16");
		}
		else if (GUI.Button(new Rect (1000,500,240, 60), "F-18"))
		{
			DontDestroyOnLoad(GameObject.Find ("f18"));
			fileOperations.saveChoose("f18");
		}
	}
}
