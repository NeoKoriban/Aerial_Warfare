using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

/**
 * Klasa odpowiedzialna za operacje na plikach.
 * 
 * @author Denis Wychowałek
 * @version 1.0
 * 
 * */
public class FileOperationScript : MonoBehaviour {

	/**
	 * Zmienna przechowująca nazwę pliku
	 * */
	private string nameFile;

	/**
	 * Konstruktor jednoargumentowy przyjmujący jako argument nazwę pliku.
	 * Argumenty:
	 * 		string name - zmienna nazwy pliku
	 * */
	public FileOperationScript(string name)
	{
		nameFile = name;
	}

	/**
	 * Funkcja usuwająca tymczasowy plik.
	 * */
	public void deleteTemporaryFile()
	{
		File.Delete (nameFile);
	}

	/**
	 * Funkcja, która sprawdza czy plik istnieje.
	 * */
	public bool findFile()
	{
		if(File.Exists(nameFile))
		   return true;
		else 
		   return false;
	}

	/**
	 * Funkcja służąca do zapisu nazwy wybranego do pliku.
	 * Argumenty:
	 * 		string name - zmienna przechowująca nazwę.
	 * */
	public void saveChoose(string name)
	{
		StreamWriter file = new StreamWriter(nameFile);
		file.WriteLine(name);
		file.Close ();
	}
	
	/**
	 * Funkcja służąca do wczytywania nazwy z pliku.
	 * Zwraca:
	 * 		Nazwę string, który był zapisany w pliku.
	 * */
	public string loadChoose()
	{
		StreamReader file = new StreamReader (nameFile);
		string name;
		name = file.ReadLine ();
		file.Close ();

		return name;
	}

	/**
	 * Funkcja tworząca listę z danymi pochodzącymi z plików o określonej nazwie katalogu.
	 * Argumenty:
	 * 		string driectoryName - nazwa folderu, z którego pobiera się nazwy plików
	 * Zwraca:
	 * 		Listę <string[]> zawierajacą tablicę stringów z danymi.
	 * */
	private List<string[]> createList(string directoryName) 
	{
		//Utworzenie listy z danymi
		List<string[]> newList = new List<string[]> ();
		//Pobieranie nazw wszystkich plików z folderu o określonej nazwie
		string [] fileNames = Directory.GetFiles(directoryName);

		for (int i = 0; i < fileNames.Length; i++) 
		{
			//Otwarcie określonego pliku
			StreamReader file = new StreamReader(fileNames[i]);
			//Liczba linii w pliku, każda linia to inne dane
			int numberOfData = File.ReadAllLines(fileNames[i]).Length;
			//Tablica z danymi o rozmiarze odczytanym wcześniej
			string [] data = new string[numberOfData];

			for (int j = 0; j < numberOfData; j++)
			{
				//Zapisywanie linii danych do pliku
				data[j] = file.ReadLine();
			}
			//Dodawanie tablicy z danymi do listy
			newList.Add (data);
			file.Close();
		}
		return newList;
	}

	/**
	 * Funkcja sprawdzająca czy istnieje login na liście.
	 * Argumenty:
	 * 		string name - nazwa użytkownika
	 * Zwraca:
	 * 		Wartość bool, czy został użytkownik znaleziony.
	 * */
	private bool findLoginOnList (string name) 
	{
		List<string[]> loginList = createList ("Players/PlayerRegistered/");
		bool foundIt = false;
		for (int i = 0; i < loginList.Count; i++) 
		{
			string [] user = loginList[i];
			if( user[0].Equals(name))
				foundIt = true;
		}
		return foundIt;
	}
	
	/**
	 * Funkcja sprawdzająca czy hasło jest prawidłowe i czy użytkownik istnieje.
	 * Argumenty:
	 * 		string name - nazwa użytkownika
	 * 		string password - hasło użytkownika
	 * Zwraca:
	 * 		Wartość bool, czy hasło jest prawidłowe.
	 * */
	private bool findPasswordOnList (string name, string password)
	{
		List<string[]> loginList = createList ("Players/PlayerRegistered/");
		bool correctPassword = false;

		for (int i = 0; i < loginList.Count; i++) 
		{
			string [] user = loginList[i];
			if(user[0].Equals(name))
			{
				if(user[1].Equals(password))
					correctPassword = true;
			}
		}

		return correctPassword;

	}

	public void saveSettings (string density, string enemy)
	{
		StreamWriter file = new StreamWriter (nameFile);
		file.WriteLine (density);
		file.WriteLine (enemy);
		file.Close ();
	}

	public List<int> loadSettings ()
	{
		List<int> values = new List<int> ();
		StreamReader file = new StreamReader (nameFile);
		values.Add (System.Int32.Parse(file.ReadLine ()));
		values.Add (System.Int32.Parse (file.ReadLine ()));
		file.Close ();

		return values;
	}

	/**
	 * Funkcja, która ma za zadanie utworzyć nowego użytkownika jeśli nie istnieje,
	 * jeśli istnieje zwraca odpowiedni komunikat. W momencie gdy można utworzyć nowego
	 * użytkownika tworzony jest jego katalog, gdzie zapisywane będą wyniki użytkowników
	 * oraz plik z nazwą użytkownika i hasłem użytkownika.
	 * Argumenty:
	 * 		string password - hasło użytkownika
	 * 		string passwordConfirm - hasło użytkownika, które jest potrzebne do jego potwierdzenia
	 * Zwraca:
	 * 		Komunikat string odpowiadającemu sytuacji, jaka wynikneła podczas działania funkcji.
	 * */
	public string createNewUserFile (string password, string passwordConfirm)
	{
		string communicate = "";

		if (nameFile.Equals ("") || password.Equals ("") || passwordConfirm.Equals ("")) 
		{
			communicate = "Complete all data";
		}
		else
		{
			if (findLoginOnList (nameFile) == true)
			{
				//Sprawdzanie czy użytkownik już istnieje.
				communicate = "User " + nameFile + " exist";
			}
			else 
			{	
				//Sprawdzanie poprawności dwóch haseł, które podano.
				if (password.Equals(passwordConfirm)) 
				{
					//zmienna do ścieżki gdzie ma zostać zapisany plik z danymi użytkownika
					string fileName = "Players/PlayerRegistered/" + nameFile;
					//utworzenie nowego katalogu
					Directory.CreateDirectory("Players/PlayerGameResults/" + nameFile);
					StreamWriter file = new StreamWriter(fileName);
					//zapisanie loginu do pliku
					file.WriteLine(nameFile);
					//zapisanie hasła do pliku
					file.WriteLine(password);
					file.Close();
					communicate = "User " + nameFile + " added";
				}
				else
					communicate = "Passwords not equals";
			}
		}
		return communicate;
	}

	/**
	 * Funkcja ma na celu sprawdzenie czy uzytkownik istnieje, zalogowanie go jeśli istnieje i sprawdzenie
	 * poprawności hasła.
	 * Argumenty:
	 * 		string password - hasło użytkownika
	 * 		string passwordConfirm - potwierdzenie hasła użytkownika
	 * Zwraca:
	 * 		Komunikat czy znaleziono użytkownika, czy hasło jest prawidłowe.
	 * */
	public string checkUser(string password, string passwordConfirm)
	{
		string communicate = "";

		if (password.Equals (passwordConfirm)) 
		{
			if (findPasswordOnList (nameFile, password)) 
			{
				communicate = "Login als " + nameFile;
			} 
			else 
			{
				communicate = "Bad password or user not found";
			}
		} 
		else
		{
			communicate = "Passwords not equals";
		}
		return communicate;
	}

	/**
	 * Funkcja, która zwraca listę samolotów, które mają posłużyć jako modele pod
	 * generowanych losowo przeciwników do każdego poziomu. W zależności od wyboru
	 * gracza wybierana jest przeciwna strona konfliktu.
	 * Argumenty:
	 * 		string name - nazwa samolotu
	 * Zwraca:
	 * 		List<GameObject> - lista samolotów pod generowanie przeciwników.
	 * */
	public List<GameObject> createEnemyList (string name)
	{
		List<GameObject> enemyList = new List<GameObject> ();
		string [] fileName = Directory.GetFiles ("Assets/Resources/PlaneModels/ListPlane");
		bool find = false;
		for (int i = 0; i < fileName.Length; i++) 
		{
			int lengthFile = File.ReadAllLines(fileName[i]).Length;
			StreamReader file = new StreamReader(fileName[i]);
			for(int j = 0; j < lengthFile/2; j++)
			{
				if(name.Equals(file.ReadLine()))
				{
					find = true;
					break;
				}
				file.ReadLine();
				   
			}
			file.Close();
			int index;
			if(i.Equals(fileName.Length-2))
				index = 0;
			else
				index = i+2;
			file = new StreamReader(fileName[index]);
			lengthFile = File.ReadAllLines(fileName[index]).Length;
			if (find.Equals(true))
			{
				for(int j = 0; j < lengthFile /2; j++)
				{
					file.ReadLine();
					enemyList.Add (Resources.Load (file.ReadLine()) as GameObject);
				}
				break;
			}
			file.Close();
		}
		return enemyList;
	}

	/**
	 * Funkcja, która zwraca listę budynków naziemnych.
	 * Zwraca:
	 * 		List<GameObject> - lista samolotów pod generowanie przeciwników.
	 * */
	public List<GameObject> createBuildingList ()
	{
		List<GameObject> buildList = new List<GameObject> ();
		string fileLocalisation = "Assets/Resources/Buildings/Buildings.txt";
		StreamReader file = new StreamReader(fileLocalisation);

		for(int i = 0; i < File.ReadAllLines(fileLocalisation).Length; i++)
		{
			buildList.Add (Resources.Load (file.ReadLine ()) as GameObject );
		}
		file.Close ();

		return buildList;
	}
	/*
	 * Funkcja zapisująca wszystkie parametry do pliku pod koniec grania, gdy gracz ma zamiar
	 * wyjść do menu głównego, zestrzelono go lub rozbił się.
	 * Argumenty:
	 * 		int levelNumber - numer poziomu
	 * 		int points - liczba punktów
	 * 		int enemyDestroyed - liczba zestrzelonych samolotów.
	 * */
	public void saveResultsToFile(int levelNumber, int points, int enemyDestroyed)
	{
		string [] fileName = Directory.GetFiles ("Players/PlayerGameResults/" + nameFile);
		StreamWriter file = new StreamWriter ("Players/PlayerGameResults/"+ nameFile + "/game" + fileName.Length.ToString ());
		file.WriteLine (levelNumber.ToString ());
		file.WriteLine (points.ToString ());
		file.WriteLine (enemyDestroyed.ToString ());
		file.Close ();
	}

	/**
	 * Funkcja służąca do załadowania wszystkich potrzebnych danych do statystyki dla konkretnego gracza.
	 * Na samym początku przeszukuje folder z jego wynikami w grze. Następnie sumowane są wszystkie
	 * zestrzelone samoloty we wszystkich grach, wyszukiwany jest najlepszy wynik gracza, jaki osiągnął
	 * poczas gry.
	 * Zwraca:
	 * 		string [] tablicę z wszystkimi potrzebnymi wartościami.
	 * */
	public string [] loadStats ()
	{
		string [] stats = new string[3];
		string [] fileName = Directory.GetFiles ("Players/PlayerGameResults/" + nameFile);
		int points = 0;
		int games = fileName.Length;
		int enemyDestroyed = 0;
		for (int i = 0; i < fileName.Length; i++) 
		{
			StreamReader file = new StreamReader(fileName[i]);
			file.ReadLine();
			int tmpValue = System.Int32.Parse (file.ReadLine());
			if (tmpValue > points)
				points = tmpValue;
			enemyDestroyed += System.Int32.Parse(file.ReadLine ());
			file.Close();
		}
		stats[0] = points.ToString();
		stats[1] = games.ToString();
		stats[2] = enemyDestroyed.ToString ();
		return stats;
	}

	/**
	 * Funkcja przygotowująca listę, która posłuży do stworzenia listy rankingowej.
	 * Najpierw przeszukuje cały folder i zapisuje do tablicy stringów ścieżki do 
	 * wszystkich znalezionych podfolderów, które stanowią nazwy użytkowników.
	 * W nich szukają wszystkich plików z grami i odczytują kolejno dane porównując
	 * je z danymi już umieszczonymi na liście.
	 * Zwraca:
	 * 		List <string[]> lista rankingowa z wartościami.
	 * */
	public List<string[]> createLeaderboard ()
	{
		string [] directoryName = Directory.GetDirectories (nameFile);
		List <string[]> leadeboard = new List<string[]> ();
		for (int i = 0; i < 5; i++) 
		{
			string [] blank = new string[3];
			blank[0] = "";
			blank[1] = "";
			blank[2] = "0";
			leadeboard.Add(blank);
		}
		for (int i = 0; i < directoryName.Length; i++) 
		{
			string [] fileName = Directory.GetFiles(directoryName[i]);

			for (int j = 0; j < fileName.Length; j++)
			{
				StreamReader file = new StreamReader(fileName[j]);
				string [] tmpData = new string[3];
				tmpData[0] = directoryName[i];
				tmpData[1] = file.ReadLine ();
				tmpData[2] = file.ReadLine ();
				file.Close ();
				for (int k = 0; k < 5; k++)
				{
					string [] tmpData2 = leadeboard[k];
					if(System.Int32.Parse(tmpData[2]) > System.Int32.Parse (tmpData2[2]))
					{
						int index = 0;
							index = k;

						for(int l = 4; l > index; l--)
						{
							leadeboard[l] = leadeboard [l-1];
						}
						leadeboard[index] = tmpData;
						break;
					}
				}
			}
		}
		return leadeboard;
	}
}