using UnityEngine;
using System.Collections;

public class Done_DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;

	private Done_GameController gameController;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <Done_GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter (Collider other)
	{

		if(this.tag == "LaserInimigo" && other.tag == "Untagged"){

			Destroy (other.gameObject);
			
			Destroy (gameObject);

			Debug.Log("Chocaram!");
			return;

		}

		if (other.tag == "Barreira" || other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "GameController" || other.tag == "Asteroide" || other.tag == "LaserInimigo"){
			return;
		}

		if (explosion != null){
			Instantiate(explosion, transform.position, transform.rotation);
		}

		if (other.tag == "Player"){

			/*
			 * Adicionando ao game controller, informacao sobre as colisoes do player.
			 */
			if(this.tag == "Enemy")
			{
				gameController.navesColididas++;
				gameController.SetAlvoColidido();
				//Debug.Log("Nave colidiu");
			}
			else if (this.tag == "Asteroide")
			{
				gameController.asteroidesColididos++;
				gameController.SetAlvoColidido();
				//Debug.Log("Aste colidiu!");
			}else if (this.tag == "LaserInimigo")
			{
				gameController.SetJogadorLevouUmTiro();
				//Debug.Log("Levou um tiro!");
			}

			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);

			gameController.SetDanoRecebido(); // Tirando 0.25 para resultar na barra.

			if (gameController.GetVida_Jogador() <= 0) 
			{
				gameController.GameOver();

				Destroy (other.gameObject);

				Destroy (gameObject);
			}
		}


		gameController.AddScore(scoreValue);

		if (other.tag != "Player")
		{
			Destroy (other.gameObject);

			gameController.SetAlvoAcertado();

		}

		Destroy (gameObject);
	}
}