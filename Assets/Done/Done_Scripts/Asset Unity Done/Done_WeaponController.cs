using UnityEngine;
using System.Collections;

public class Done_WeaponController : MonoBehaviour
{
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float delay;

	//Para poder acessar o tempo do jogo.
	public Done_GameController gameControle;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameControle = gameControllerObject.GetComponent <Done_GameController>();
		}
		if (gameControle == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}

		InvokeRepeating ("Fire", delay, fireRate);

		InvokeRepeating ("ContaTiro", delay, fireRate);
	}

	void Fire ()
	{
		Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		GetComponent<AudioSource>().Play();
	}

	void ContaTiro () 
	{
		gameControle.tirosDisparadosNaves++;
		//Debug.Log("Tiro Nave Disparado!");
	}
}
