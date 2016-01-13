using UnityEngine;
using System.Collections;

public class Done_Barreira : MonoBehaviour {

	private Done_GameController gameController;
	
	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null){
			gameController = gameControllerObject.GetComponent <Done_GameController>();
		}
		if (gameController == null){
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	
	// Update is called once per frame
	void Update () {}


	void OnTriggerEnter (Collider other) 
	{
		
		if(other.tag != "LaserInimigo" || other.tag != "Boundary" || other.tag != "GameController" || other.tag != "Player")
		{
			gameController.elementosQueCruzaramAFronteira++;
			
			Debug.Log("Cruzou a fronteira! n: " + gameController.elementosQueCruzaramAFronteira);
		}

		Destroy(other.gameObject);
	}
}
