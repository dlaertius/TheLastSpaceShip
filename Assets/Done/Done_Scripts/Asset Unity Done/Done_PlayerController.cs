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

	/*
	 * This two scripts above will be used to acess their objects in scene, getting some variables necessary.
	 * Os dois scripts abaixo serao usados para acessar seus objetos em cena, adquirindo algumas variaveis necessarias.
	 *
	 */

	public Done_GameController gameControle;

	public Player player;

	void Update (){

		/*
		 * In a case of a continuous change the state of the ship.
		 * No caso de uma mudanca contiuna do estado da nave.
		 */

		StatusShip();

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

				/* Used to verify the bigger delay.
				 * Usado para verificar qual o maior delay do jogador.
				 */
				player.VerificaMaiorDelay(gameControle.GetTempoTotal());
				
				/* Used to calculate IF fire rate time was > 3 seconds, so it's a delay.
				 * Usado para calcular se o tempo de tiro foi > 3 segundos, entao e um delay.
				 */
				player.CalculaDelays(gameControle.GetTempoTotal());

				/* Used to controll this time - the new time when the player shot.
				 * Usado para controlar o tempo dos delays, uma vez que quando o jogador atirar e so subtrair pelo anterior.
				 */
				player.tempoDoUltimoDisparo = gameControle.GetTempoTotal();

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

	void StatusShip(){
		this.speed = player.speedPlayer;
		this.fireRate = player.fireRatePlayer;
		this.tilt = player.tiltPlayer;
	}
}
