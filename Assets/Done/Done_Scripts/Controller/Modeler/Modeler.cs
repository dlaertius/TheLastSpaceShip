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
using System.Collections.Generic;

public class Modeler : MonoBehaviour {

	public Done_GameController game;
	public Player player;
	public Database data;
	
	private int waveNumberTrigger = 0;

    private string playerLevelModeler;

    /*
    * Used to save the knn distance for each cell comparated with players data.
    */
   
    private Dictionary<float, Database.Cell> knnDic = new Dictionary<float, Database.Cell>();

	/*
	 * Types: amateur, intermediate, hardcore.
	 */
	private string playerType = "";

	private float[] dataPlayer, dataGame;

	// Use this for initialization
	void Start () {
        
        this.playerLevelModeler = "none";

        this.waveNumberTrigger = game.numeroDaOnda;
   
	}
	
	// Update is called once per frame
	void Update () {

		if(waveNumberTrigger != game.numeroDaOnda) {

			Debug.Log("Trigger used for Modeler.cs linha 30 to watch the waves alteration.");

            KNN(); //trigger to calculate player level reccomender.

            waveNumberTrigger = game.numeroDaOnda;

		}
	}


	public string GetPlayerType() { return this.playerType; }

	void GetData () {

		dataPlayer = player.PlayerStatus();
		Debug.LogError(dataPlayer);

		dataGame = game.GameStatus();
		Debug.LogError(dataGame);
	}

	void ProcessingData(){

		//ShotDelay - DelayTiro.
		float normalizeValue_Delay = Normalize((float)dataPlayer[0], 23.0f, 0.0f);
		this.dataPlayer[0] = normalizeValue_Delay;
		Debug.Log(normalizeValue_Delay + "-" + dataPlayer[0]);

		//MovimentPrSecond - MovPorSegund.
		float normalizeValue_Movim = Normalize((float)dataPlayer[dataPlayer.Length-1], 4.43f, 0.0f);
		this.dataPlayer[dataPlayer.Length-1] = normalizeValue_Movim;
		Debug.Log(normalizeValue_Movim + "-" + dataPlayer[dataPlayer.Length-1]);


	}

	/*
	 * It's necessary only normalize movPerSecnd and QntDelay, because the other ones are already btwen 0 and 1.
	 * E necessario apenas normalizar moviPorSegundo e QntDelay, pois os demais ja sao de 0 a 1 .
	 * 
	 */

	float Normalize(float value, float valueMax, float valueMin){
		return ((value - valueMin)/valueMax);
	}

	float EuclideanDistance (float[] dataPlayer, float[] dataBase) {

		float euclideanDistance = 0.0f;

		for (int x = 0; x < dataPlayer.Length ; x++){
			euclideanDistance += (float) Mathf.Pow((dataPlayer[x] - dataBase[x]),2);
		}

		return Mathf.Sqrt(euclideanDistance);
	}

	void KNN(){

        if (game.numeroDaOnda == 1) { }

        else if (game.numeroDaOnda == 2) { }

        else if (game.numeroDaOnda == 3) { }

        else if (game.numeroDaOnda == 4) { }

        else if (game.numeroDaOnda == 5) { }

        else if (game.numeroDaOnda == 6) { }

        else if (game.numeroDaOnda == 7) { }

        else if (game.numeroDaOnda == 8) { }

        else if (game.numeroDaOnda == 9) { }

        else if (game.numeroDaOnda == 10) { }

        else if (game.numeroDaOnda == 11) { }

        else if (game.numeroDaOnda == 12) { }


    }
}
