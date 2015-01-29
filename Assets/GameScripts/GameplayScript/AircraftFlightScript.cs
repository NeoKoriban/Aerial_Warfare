using UnityEngine;
using System.Collections;

public class AircraftFlightScript : MonoBehaviour {

	/**
	 * Współczynnik do prędkości początkowej samolotu
	 * */
	private float planeSpeed = 5.0f;

	/**
	 * Obiekt samolotu, który będzie wprawiony w ruch
	 * */
	private GameObject aircraftObject;

	/**
	 * Konstruktor jednoargumentowy, w którym jako argument przekazuje
	 * się obiekt samolotu, który został wczytany na scenę.
	 * Argumenty:
	 * 		GameObject aircraft - model samolotu wczytanego na scenę.
	 * */
	public AircraftFlightScript(GameObject aircraft)
	{
		aircraftObject = aircraft;
	}

	/**
	 * Ustawienie wektora pozycji startowej obiektu samolotu.
	 * Argumenty:
	 * 		float x - pozycja x
	 * 		float y - pozycja y
	 * 		float z - pozycja z
	 * Zwraca:
	 * 		Vector3 potrzebny do innych funkcji.
	 * */ 
	public Vector3 prepareVector3 (float x, float y, float z)
	{
		float newX = aircraftObject.transform.position.x + x;
		float newY = aircraftObject.transform.position.y + y;
		float newZ = aircraftObject.transform.position.z + z;

		return new Vector3(newX,newY,newZ);
	}

	/**
	 * Ustawienie samolotu na odpowiedniej pozycji początkowej.
	 * Argumenty:
	 * 		Vector3 positionVector - pozycja samolotu na scenie.
	 * */
	public void setPositionScaleAndRotate (Vector3 positionVector)
	{
		aircraftObject.transform.position = positionVector;
		aircraftObject.transform.localScale = new Vector3 (2.5f, 2.5f, 2.5f);
		aircraftObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
	}

	/**
	 * Ustawianie pozycji kamery na scenie.
	 * Argumenty:
	 * 		GameObject camera - obiekt kamery.
	 * 		Vector3 positionVector - pozycja kamery na scenie.
	 * */
	public void setPositionCamera(GameObject camera, Vector3 positionVector)
	{
		camera.transform.position = positionVector;
		camera.transform.parent = aircraftObject.transform;
	}

	/**
	 * Ustawianie przyspieszenia dla samolotu.
	 * Argumenty:
	 * 		float value - współczynnik o ile ma być przyspieszenie.
	 * 		float speed - prędkość z jaką porusza się samolot.
	 * Zwraca:
	 * 		float nowej prędkości.
	 * */
	private float accelerationSet (float value, float speed)
	{
		if (speed > 20.0f)
			speed = 20.0f;
		else if (speed < 5.0f)
			speed = 5.0f;
		else
			speed *= value;

		return speed;
	}

	/**
	 * Funkcja, której zadaniem jest poruszanie się samolotu w górę lub w dół
	 * Argumenty:
	 * 		float value - wartość z jaką szybkością ma poruszać się samolot
	 * */
	private void moveUpOrDownAricraft(float value)
	{
		float moveSpeed = value * Time.deltaTime;
		Vector3 moveVector = new Vector3 (0.0f, 0.0f, moveSpeed);
		aircraftObject.transform.Rotate (moveVector);
	}

	/**
	 * Funkcja, której zadaniem jest obrót samolotu, a także skręcanie podczas lotu.
	 * Argumenty:
	 * 		float value - wartość z jaką szybkością ma poruszać się samolot
	 * */
	private void moveLeftOrRightAircraft(float value)
	{
		float moveSpeed = value * Time.deltaTime;
		Vector3 moveVector = new Vector3 (moveSpeed, 0.0f, 0.0f);
		aircraftObject.transform.Rotate (moveVector);
	}

	/**
	 * Funkcja wyboru rotacji w lewo lub w prawo w zależności od naciśniętego
	 * klawisza przez gracza.
	 * Argumenty:
	 * 		float speed - prędkość z jaką porusza się samolot gracza
	 * */
	private void rotationMove(float speed)
	{
		if (Input.GetKey ("left"))
						moveLeftOrRightAircraft (speed * 0.5f * 10.0f);
		else if (Input.GetKey("right"))
						moveLeftOrRightAircraft (-(speed * 0.5f * 10.0f));

	}
	
	/**
	 * Funkcja wzbijania się lub opadania samolotu
	 * Argumenty:
	 * 		float speed -  prędkość z jaką gracz się porusza
	 * */
	private void upAndDownMove(float speed)
	{
		if (Input.GetKey ("up"))
				moveUpOrDownAricraft (speed * 0.5f * 4.0f);
		else if (Input.GetKey("down"))
			moveUpOrDownAricraft (-(speed * 0.5f * 4.0f));
	}




	public void moveAircraftAI ()
	{
		aircraftObject.transform.Translate (planeSpeed * Time.deltaTime * 40.0f, 0.0f, 0.0f);

	}
	/**
	 * Funkcja obsługująca ruch gracza na scenie.
	 * */
	public void moveAircraft ()
	{
		aircraftObject.transform.Translate (planeSpeed * Time.deltaTime * 20.0f, 0.0f, 0.0f);
	
		if (Input.GetKey("a")) 
			planeSpeed = accelerationSet (1.05f, planeSpeed);
		else if (Input.GetKey ("s")) 
			planeSpeed = accelerationSet (0.95f, planeSpeed);
		if(Input.GetKey("up") || Input.GetKey ("down") || Input.GetKey("left") || Input.GetKey("right"))
		{
			upAndDownMove(planeSpeed);
			rotationMove(planeSpeed);
		}
	}
	
}
