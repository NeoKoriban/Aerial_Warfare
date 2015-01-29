using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Klasa odpowiedzialna za obsługę rankingu graczy.
 * 
 * @author Denis Wychowałek
 * @version 1.0
 * */
public class Leaderboard : MonoBehaviour {

	// Use this for initialization
	void OnGUI () {
		GUI.skin.label.fontSize = 30;
		GUI.skin.label.normal.textColor = Color.grey;
		FileOperationScript fileOperations = new FileOperationScript ("Players/PlayerGameResults/");
		List <string []> leadeboard = fileOperations.createLeaderboard ();
		for (int i = 0; i < 5; i++) 
		{
			string [] nameDir = { "Players/PlayerGameResults/" };
			string [] tmpResult = leadeboard[i];
			GUI.Label (new Rect (140,170+50*i,250,50),(i+1).ToString()+". " + 
			           tmpResult[0].Split(nameDir,System.StringSplitOptions.None)[1]);

			GUI.Label (new Rect (480,170+50*i,200,50),"Level " + tmpResult[1]);
			GUI.Label (new Rect (740,170+50*i,250,50), tmpResult[2]);
		}
		if (GUI.Button (new Rect (20,480,200, 60), "Main Menu"))
		{
			Application.LoadLevel(0);
		}
	}

}
