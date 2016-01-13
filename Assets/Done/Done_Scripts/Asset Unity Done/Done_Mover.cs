using UnityEngine;
using System.Collections;

public class Done_Mover : MonoBehaviour
{
	public float speed = -5;

	private Done_GameController gameController;

	void Start ()
	{
		GetComponent<Rigidbody>().velocity = transform.forward * speed;

		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null){
			gameController = gameControllerObject.GetComponent <Done_GameController>();
		}
		if (gameController == null){
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter (Collider other){


		if (other.tag == "Boundary" || other.tag == "GameController" || other.tag == "LaserInimigo"){
			return;
		}

		/*
		 * Adicionando aos objetos destruidos pelo laser do player.
		 */

		if(this.tag == "Untagged")
		{

			if(other.tag == "Asteroide")
			{
				//Debug.Log("asteroide");
				gameController.totalDeAsteroidesDestruidos++;

			}else if (other.tag == "Enemy")
			{
				//Debug.Log("Nave atingida!");
				gameController.totalDeNavesDestruidas++;
			}

		}
	}
}
