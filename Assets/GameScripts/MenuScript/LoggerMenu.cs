using UnityEngine;
using System.Collections;
using System.IO;
/**
 * Klasa odpowiedzialna za obsługę elementów GUI do logowania oraz statystyk.
 * 
 * @author Denis Wychowałek
 * @version 1.0
 * 
 * */
public class LoggerMenu : MonoBehaviour 
{
	/**
	 * Zmienna w której przechowywane są informacje o loginie
	 * */
	private string loginTextField = "";

	/**
	 * Zmienna w której przechowywane są informacje o haśle
	 * */
	private string passTextField = "";

	/**
	 * Zmienna w której przechowywane są dane, gdzie trzeba potwierdzić hasło użytkownika
	 * */ 
	private string confirmTextField = "";

	/**
	 * Zmienna przechowująca standardowy rozmiar czcionki dla elementów GUI
	 * */
	private const int fontSize = 30;

	/**
	 * Zmienna przechowująca nazwę czcionki
	 * */
	private const string fontName = "HELVETICA";

	/**
	 * Domyślna wartość logowania dla początkowego programu, czy użytkownik jest zalogowany
	 * */
	private bool logIn = false;

	/**
	 * Zmienna przechowująca GUIText, który wyświetla komunikat
	 * */
	private GameObject communicateGUIText;

	/**
	 * Zmienna przechowująca tytuł karty.
	 * */
	private GameObject titleStatLogin;

	/**
	 * Zmienna przechowująca przycisk LogOut
	 * */
	private GameObject logOutText;

	/**
	 * Zmienna przechowująca nazwę użytkownika
	 * */
	private GameObject nicknameText;

	/**
	 * Zmienna przechowująca liczbę zestrzelonych samolotów
	 * */
	private GameObject planeDestroyedText;

	/**
	 * Zmienna przechowująca liczbę odbytych gier
	 * */
	private GameObject gamesText;

	/**
	 * Zmienna przechowująca najlepszy wynik z gier
	 * */
	private GameObject bestResultText;

	/**
	 * Funkcja, która ustawia wartości dla GUI m.in. rozmiar czcionki, rodzaj czcionki, kolor czcionki.
	 * */
	private void skinSelected()
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

		GUI.skin.button.font = Resources.Load (fontName) as Font;
		GUI.skin.button.fontSize = fontSize;

	}

	/**
	 * Funkcja decydująca o tym które elementy są aktywne.
	 * Argumenty:
	 * 		bool active - wartość logiczna, czy ma być element aktywny
	 * */
	private void activeContent (bool active)
	{
		communicateGUIText.SetActive(active);
		nicknameText.SetActive(active);
		planeDestroyedText.SetActive(active);
		gamesText.SetActive(active);
		bestResultText.SetActive(active);
	}

	/**
	 * Utworzenie potrzebnych elementów do logowania.
	 * */
	private void createGUIElements()
	{
		skinSelected ();
		GUI.Label (new Rect (250, 200, 200, 50), "Login");
		GUI.Label (new Rect (250, 250, 200, 50), "Password");
		GUI.Label (new Rect (250, 300, 200, 50), "Confirm");

		titleStatLogin.guiText.text = "Register or LogIn";
		loginTextField = GUI.TextField (new Rect (450, 200, 300, 40), loginTextField, 16);
		passTextField = GUI.PasswordField(new Rect (450, 250, 300, 40),passTextField,'*',16);
		confirmTextField = GUI.PasswordField(new Rect (450, 300, 300, 40), confirmTextField,'*', 16);
	}

	/**
	 * Funkcja służąca do wyświetlania statystyk.
	 * */
	private void printStats()
	{
		titleStatLogin.guiText.text = "Stats";
		FileOperationScript fileOperations = new FileOperationScript ("UserChoosen");
		string nick = fileOperations.loadChoose ();
		nicknameText.guiText.text = "Nickname:  " + nick;
		fileOperations = new FileOperationScript (nick);
		string [] stats = fileOperations.loadStats ();
		gamesText.guiText.text = "Games: " + stats[1];
		bestResultText.guiText.text = "Best Result: " + stats[0];
		planeDestroyedText.guiText.text = "Plane Destroyed: " + stats[2];
		communicateGUIText.guiText.text = "Player stats more:";

	}

	/**
	 * Funkcja obsługująca system wyświetlania potrzebnych informacji w zależności od tego
	 * czy użytkownik jest zalogowany czy nie.
	 * Parametery:
	 * 		bool ifLogin - przyjmuje jako parametr informację czy użytkownik zalogował się już
	 * */
	private void loggerGUI (bool ifLogin)
	{
	 	
		if (ifLogin == false) 
		{
			activeContent(ifLogin);
			createGUIElements();
			FileOperationScript fileOperations = new FileOperationScript(loginTextField);

			if (GUI.Button (new Rect (550, 400, 200, 60), "Register"))
			{
				logIn = false;
				string communicate = fileOperations.createNewUserFile(passTextField, confirmTextField);
				communicateGUIText.SetActive(true);
				communicateGUIText.guiText.text = communicate;

			}
			else if (GUI.Button (new Rect (250, 400, 200, 60), "Login"))
			{
				string communicate = fileOperations.checkUser(passTextField, confirmTextField);
				communicateGUIText.guiText.text = communicate;
				if (communicate.Equals("Login als " + loginTextField))
				{
					FileOperationScript fileOperationsSaveChoose = new FileOperationScript ("UserChoosen");
					fileOperationsSaveChoose.saveChoose(loginTextField);
					logIn = true;
				}
				else 
				{
					logIn = false;
					communicateGUIText.SetActive(false);

				}
			}

			else if (GUI.Button (new Rect (50,600,200, 60), "Main Menu"))
			{
				Application.LoadLevel(0);
			}

		} 
		else 
		{

			printStats();
			activeContent (ifLogin);
			if (GUI.Button (new Rect (50,600,200, 60), "Main Menu"))
			{
				Application.LoadLevel(0);
			}
			else if (GUI.Button (new Rect (1000,600,200, 60), "LogOut"))
			{
				FileOperationScript fileOperations = new FileOperationScript ("UserChoosen");
				fileOperations.deleteTemporaryFile ();
			}
		}
	}

	void OnGUI()
	{
		if (File.Exists ("UserChoosen")) 
		{
			logIn = true;
			FileOperationScript fileOperation = new FileOperationScript ("UserChoosen");
			logIn = fileOperation.findFile ();
		}
						
		else
			logIn = false;

		loggerGUI (logIn);
    }

	// Use this for initialization
	void Start () {
	
		communicateGUIText = GameObject.Find ("Communicate");
		titleStatLogin = GameObject.Find ("Title");
		logOutText = GameObject.Find ("LogOut");
		nicknameText = GameObject.Find ("Nickname");
		planeDestroyedText = GameObject.Find ("PlaneDestroyed");
		gamesText = GameObject.Find ("Games");
		bestResultText = GameObject.Find ("BestResult");

	}
}
