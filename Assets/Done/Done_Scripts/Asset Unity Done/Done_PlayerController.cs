using UnityEngine;
using System.Collections;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;

	//Para poder acessar o tempo do jogo.
	public Done_GameController gameControle;

	public Player player;

	void Update (){

		if (Input.GetKeyDown("space") && Time.time > nextFire) {

			nextFire = Time.time + fireRate;

			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

			GetComponent<AudioSource>().Play ();

			player.tirosDisparados++;

			if(gameControle.contaDelay == true)
			{
				player.tempoDoUltimoDisparo = gameControle.GetTempoTotal();

			}else if (gameControle.contaDelay == false)
			{
				player.VerificaMaiorDelay(gameControle.GetTempoTotal());

				player.CalculaDelays(gameControle.GetTempoTotal());

				player.tempoDoUltimoDisparo = gameControle.GetTempoTotal();
				//Debug.Log("Maior Delay: " + this.maiorDelay);
			}
		}

		if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)){
			player.quantidadeDeMovimentos++;
			//Debug.Log("Quantidade De Movimentos: " + this.quantidadeDeMovimentos);
		}
	}

	/*
	 * Não modificar. Don't modify.
	 */
	void FixedUpdate (){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;
		
		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);
		
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}
}
