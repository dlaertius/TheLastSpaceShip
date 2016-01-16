/*
 * (EN) In this script will happen the processment of data, in case, to discuss what the right player level.
 * (PT-BR) Nesse script, sera processado os dados, nesse caso, para discutir qual o nivel certo do jogador.
 * 
 * The variable string playerLevel will be used by WaveController object to change the characteristic of wave in game.
 * 
 */

using UnityEngine;
using System.IO;
using System.Collections;

public class Modeler : MonoBehaviour {

	public Done_GameController game;
	public Player player;

	private string playerLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GetData () {

		double[] dataPlayer = player.PlayerStatus();
		Debug.LogError(dataPlayer);

		double[] dataGame = game.GameStatus();
		Debug.LogError(dataGame);


	}

	void KNN(){}
}
