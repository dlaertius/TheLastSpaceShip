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
using System.Linq;
using System.Collections.Generic;

public class Modeler : MonoBehaviour {

    public Done_GameController game;
    public Player player;
    public Database data;

	private int waveNumberTrigger;

    private string playerLevelModeler;

    private float[] smallerDistance = { 100, 100, 100};

    private List<float> union_PlayerAndGameStatus;

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

        // Really need it?
        player = FindObjectOfType(typeof(Player)) as Player;

        game = FindObjectOfType(typeof(Done_GameController)) as Done_GameController;

          union_PlayerAndGameStatus = new List<float>();

        this.playerLevelModeler = "none";

		this.waveNumberTrigger =  1;
   
	}
	
	// Update is called once per frame
	void Update () {

        //trigger to calculate player level reccomender after first wave.
        if (waveNumberTrigger != game.numeroDaOnda && game.numeroDaOnda != 1) {

			//Debug.Log("Trigger used for Modeler.cs linha 30 to watch the waves alteration.");

            KNN(); //Use only three neighbors in this knn example.

            waveNumberTrigger = game.numeroDaOnda;

		}
	}


	public string GetPlayerType() { return this.playerType; }

	void GetData () {

		dataPlayer = player.PlayerStatus();
		//dataPlayer.ToList().ForEach(i => Debug.Log(i.ToString()));

		dataGame = game.GameStatus();
		//dataGame.ToList().ForEach(i => Debug.Log(i.ToString()));

        this.union_PlayerAndGameStatus.AddRange(dataGame);
        this.union_PlayerAndGameStatus.AddRange(dataPlayer);

        ProcessingData();

    }

	void ProcessingData(){

		//ShotDelay - DelayTiro.
		float normalizeValue_Delay = Normalize((float)dataPlayer[0], 23.0f, 0.0f);
		//Debug.Log("Delay Normalized - After/Before: " + normalizeValue_Delay + "-" + dataPlayer[0]);
		this.dataPlayer[0] = normalizeValue_Delay;

		//MovimentPrSecond - MovPorSegund.
		float normalizeValue_Movim = Normalize((float)dataPlayer[dataPlayer.Length-1], 4.43f, 0.0f);
		//Debug.Log("Mov Per S. Normalized - After/Before: " + normalizeValue_Movim + "-" + dataPlayer[dataPlayer.Length-1]);
		this.dataPlayer[dataPlayer.Length-1] = normalizeValue_Movim;
	}

	/*
	 * It's necessary only normalize movPerSecnd and QntDelay, because the other ones are already btwen 0 and 1.
	 * E necessario apenas normalizar moviPorSegundo e QntDelay, pois os demais ja sao de 0 a 1 .
	 * 
	 */

	float Normalize(float value, float valueMax, float valueMin){
		return ((value - valueMin)/valueMax);
	}

	float EuclideanDistance (List<float> dataPlayer, float[] dataBase) {

		float euclideanDistance = 0.0f;

		for (int x = 0; x < dataPlayer.Count ; x++){
			euclideanDistance += (float) Mathf.Pow((dataPlayer.ElementAt(x) - dataBase.ElementAt(x)),2);
		}

		return Mathf.Sqrt(euclideanDistance);
	}

	void KNN(){

        //Get Player and Game data.
        GetData();

        /*
        * Union two arrays, GameStatus and PlayerStatus;
        */

        List<Database.Cell> waveListUsed;

		if (this.waveNumberTrigger == 1) { waveListUsed = data.wave_1_cell_list; }

		else if (this.waveNumberTrigger == 2) { waveListUsed = data.wave_2_cell_list; }

        else if (this.waveNumberTrigger == 3) { waveListUsed = data.wave_3_cell_list; }

        else if (this.waveNumberTrigger == 4) { waveListUsed = data.wave_4_cell_list; }

        else if (this.waveNumberTrigger == 5) { waveListUsed = data.wave_5_cell_list; }

        else if (this.waveNumberTrigger == 6) { waveListUsed = data.wave_6_cell_list; }

        else if (this.waveNumberTrigger == 7) { waveListUsed = data.wave_7_cell_list; }

        else if (this.waveNumberTrigger == 8) { waveListUsed = data.wave_8_cell_list; }

        else if (this.waveNumberTrigger == 9) { waveListUsed = data.wave_9_cell_list; }

        else if (this.waveNumberTrigger == 10) { waveListUsed = data.wave_10_cell_list; }

        else if (this.waveNumberTrigger == 11) { waveListUsed = data.wave_11_cell_list; }

        else waveListUsed = data.wave_12_cell_list; //(this.waveNumberTrigger == 12)

        /*
        * Moving into Cells data and calculate euclidean distance of each one inside dataset.
        *
        */
        foreach (Database.Cell cell_values in waveListUsed)
        {

            float distance = EuclideanDistance(this.union_PlayerAndGameStatus, cell_values.ReturnValues());

            /* Save the key for the three distance smallers values.
            */

            if (distance < smallerDistance[2]) {
                if (distance < smallerDistance[1]){
                    if (distance < smallerDistance[0]) {
                        smallerDistance[0] = distance;
                    }else {
                        smallerDistance[1] = distance;
                    }
                }else {
                    smallerDistance[2] = distance;
                }
            }

            try{
				knnDic.Add(distance, cell_values);
			}catch{

				//Pega a key que contem no dic, compara as classes se igual descarta senao sujou.

				Debug.Log("Two value with same key: " + distance);
				Debug.Log("Class of players: " + cell_values.GetCellModelLevel());
			}
        }

		Debug.Log(this.smallerDistance[0] + "-" + this.smallerDistance[1] + "-" + this.smallerDistance[2]);

        /*
        Debug.Log("Menor proximidade 1 : " + knnDic[smallerDistance[0]]);
        Debug.Log("Menor proximidade 1 : " + knnDic[smallerDistance[1]]);
        Debug.Log("Menor proximidade 1 : " + knnDic[smallerDistance[2]]);
        */

        //Cleaning to use in another execution time.
        knnDic.Clear();
    }
}
