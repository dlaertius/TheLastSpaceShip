using UnityEngine;
using System.Collections;

public class Done_PowerUpDestroy : MonoBehaviour {

	private Done_GameController gameController;
	
	void Start (){
		
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null){
			gameController = gameControllerObject.GetComponent <Done_GameController>();
		}
		if (gameController == null){
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "GameController"){
			return;
		}
		
		if (other.tag == "Player"){
			
			//gameController.RecebeuPowerUp();
			
		}
		Destroy (gameObject);
	}
}
