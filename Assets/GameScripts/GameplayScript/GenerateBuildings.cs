using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateBuildings : MonoBehaviour {

	private FileOperationScript fileOperations;

	private void newBuildSettings( GameObject building)
	{
		building.transform.localScale = new Vector3 (1.1f, 1.1f, 1.1f);
	}

	private void addNewBuildOnTerrain(List <GameObject> buildingsList)
	{
		for (int i = 0; i < 100; i++) 
		{
			int numberOfModel = Random.Range(0,buildingsList.Count-1);
			float placeModelX = Random.Range(12000.0f,22000.0f);
			float placeModelZ = Random.Range(12000.0f,22000.0f);

			GameObject newBuild = Instantiate (buildingsList[numberOfModel]) as GameObject;

			newBuild.name = "Build";
			newBuild.AddComponent<CollisionTerrainAndBuildings>();
			newBuild.transform.position = new Vector3(placeModelX,Terrain.activeTerrain.transform.position.y,placeModelZ) ;
			newBuildSettings(newBuild);

		}

	}
	private void addTerrainGameObjects ()
	{
		fileOperations = new FileOperationScript ("");
		List<GameObject> gameObjectList = fileOperations.createBuildingList ();
		addNewBuildOnTerrain (gameObjectList);
	}
	// Use this for initialization
	void Start () {
		addTerrainGameObjects ();

	}
	

}
