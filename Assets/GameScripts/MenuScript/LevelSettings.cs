using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSettings : MonoBehaviour {

	private FileOperationScript fileOperations;

	private List<int> listValues;

	public LevelSettings( string nameFile) 
	{
		fileOperations = new FileOperationScript (nameFile);
		listValues = fileOperations.loadSettings ();	
	}

	public int insertCountOfEnemy()
	{
		if (listValues [1] > 10)
			return 10;
		else if (listValues [1] < 2)
			return 2;
		else
			return listValues [1];

	}

	public int insertFogDensity()
	{
		if (listValues [0] > 10)
			return 10;
		else if (listValues [0] < 0)
			return 0;
		else
			return listValues [0];
	}
}
