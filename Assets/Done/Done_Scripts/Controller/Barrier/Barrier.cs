using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour {
	
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
		
		/*	Apparently this solutions worked.
		 * Aparentemente essa soluçao funcionou.
		 *
		 * The other way: if(other.tag != "LaserInimigo" || other.tag != "Boundary" || other.tag != "GameController" || other.tag != "Player")
		 * O outro jeito : if(other.tag != "LaserInimigo" || other.tag != "Boundary" || other.tag != "GameController" || other.tag != "Player")
		 */
		if(other.tag == "Enemy" || other.tag == "Asteroide")
		{
			//Debug.Log("PASSOU");
			
			gameController.elementosQueCruzaramAFronteira++;
			
			Debug.LogError("Cruzou a fronteira! n: " + gameController.elementosQueCruzaramAFronteira);
		}
		
		Destroy(other.gameObject);
	}
}
