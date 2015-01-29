using UnityEngine;
using System.Collections;
using System.IO;

/**
 * Klasa odpowiedzialna za obsługę menu głównego.
 * 
 * @author Denis Wychowałek
 * @version 2.0
 * 
 * */
public class MainMenu : MonoBehaviour {

	/**
	 * Funkcja ustawiająca skina dla boxa wyświetlającego komunikaty
	 * w menu głównym.
	 * */
	private void skinMainMenuBox()
	{
		GUI.skin.box.normal.background = Resources.Load ("Button/buttonTexture") as Texture2D;
		GUI.skin.box.font = Resources.Load ("Font/HELVETICA") as Font;
		GUI.skin.box.fontSize = 20;
		
		GUI.skin.box.normal.textColor = Color.grey;
	}

	/**
	 * Funkcja ustawiająca skina dla buttona odpowiedzialnego za przejście do innych składowych gry
	 * w menu głównym.
	 * */
	private void skinMainMenuButton()
	{
		GUI.skin.button.normal.background = Resources.Load ("Button/buttonTexture") as Texture2D;
		GUI.skin.button.hover.background  = Resources.Load ("Button/buttonTextureHover") as Texture2D;
		GUI.skin.button.fontSize = 25;
		GUI.skin.button.font = Resources.Load ("Font/HELVETICA") as Font;
		GUI.skin.button.normal.textColor = Color.grey;
		GUI.skin.button.hover.textColor = Color.black;
	}

	/**
	 * Funkcja odpowiedzialna za wyświetlenie komunikatu w zależności od tego czy
	 * gracz się zalogował czy też nie.
	 * */
	private void displayCommunicate()
	{
		if (!File.Exists ("UserChoosen")) 
		{
			GUI.Box (new Rect (600, 50, 350, 30), "You must register or login!");
		} else 
		{
			FileOperationScript fileOperations = new FileOperationScript("UserChoosen");
			GUI.Box (new Rect (600, 50, 350, 30), "You login as " + fileOperations.loadChoose());
		}
	}

	void OnGUI()
	{	
		skinMainMenuBox ();
		skinMainMenuButton ();
		displayCommunicate ();

		if (GUI.Button (new Rect (20, 150, 350, 60), "Start Game")) 
		{
			if(File.Exists("UserChoosen"))
			{
				FileOperationScript fileOperations = new FileOperationScript("planeChoosen");
				fileOperations.deleteTemporaryFile();
				Application.LoadLevel(1);
			}
				
		}
		else if (GUI.Button (new Rect (20, 250, 350, 60),"Leaderboard"))
			Application.LoadLevel(4);
		else if (GUI.Button (new Rect (20, 350, 350, 60),"About Player"))
			Application.LoadLevel(3);
		else if (GUI.Button (new Rect (20, 450, 350, 60),"Exit"))
			Application.Quit ();

	}
}
