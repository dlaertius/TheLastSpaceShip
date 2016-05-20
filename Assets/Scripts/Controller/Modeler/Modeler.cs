/*
 * (EN) In this script will happen the processment of data, in case, to discuss what the right player level.
 * (PT-BR) Nesse script, sera processado os dados, nesse caso, para discutir qual o nivel certo do jogador.
 * 
 * The variable string playerLevel will be used by WaveController object to change the characteristic of wave in game.
 * 
 */

using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Modeler : MonoBehaviour {

    public Done_GameController game;
    public Player player;
    public Database data;
	
	/*
	 * Types: amateur, intermediate, hardcore.
	 */
	private string playerType;

	private int waveNumberTrigger;

	private float distance;

    private List<float> union_PlayerAndGameStatus;

	private List<Database.Cell> waveListUsed;

    /*
    * Used to save and after to show, the most ocurrency of level indicator.
    */
    public List<int> dataUserModeler;

	/*
	 * Local List used in method only.
	 */

	private List<float> distancesValuesSort;
	
	private List<int> modelList;
	
	private List<Database.Cell> value1;
	
	private List<Database.Cell> value2;
	
	private List<Database.Cell> value3;
	
	/*
    * Used to save the knn distance for each cell comparated with players data.
    */

	private Dictionary<float, List<Database.Cell>> knnDic;

	private float[] dataPlayer, dataGame;

	public bool triggerForWaveStatus;

	// Use this for initialization
	void Start () {

        // Really need it?
        player = FindObjectOfType(typeof(Player)) as Player;

        game = FindObjectOfType(typeof(Done_GameController)) as Done_GameController;

		union_PlayerAndGameStatus = new List<float>();

        dataUserModeler = new List<int>();

        knnDic = new Dictionary<float, List <Database.Cell>>();

		distancesValuesSort = new List<float>();

		/*
		 *If player mode equal to "adapt", so at first iterator this will be change for a player level recomender.
		 */
		this.playerType  = "";

		this.waveNumberTrigger =  1;

		triggerForWaveStatus = false;
	}
	
	// Update is called once per frame
	void Update () {

        //trigger to calculate player level reccomender after first wave.
		if (waveNumberTrigger != game.numeroDaOnda && game.numeroDaOnda != 1) {
			
			//Debug.Log("Trigger used for Modeler.cs linha 30 to watch the waves alteration.");

			if(player.GetGameMode().Equals("adapt")) KNN(5); //Use only five neighbors in this knn example.

            waveNumberTrigger = game.numeroDaOnda;

			triggerForWaveStatus = true;

		}else{
			triggerForWaveStatus = false;
		}
	}

	//---------------------------------------------------------------------------------
	public string GetPlayerType()
    {
		if(player.GetGameMode().Equals("adapt"))
        {
			return this.playerType;
		}else
        {
			return "There is no return for other game mode.";
		}
	}

	//---------------------------------------------------------------------------------
	public int GetWaveNumberTrigger()
    {
		return this.waveNumberTrigger;
	}

	//---------------------------------------------------------------------------------
    public string GetMajorOccurrence()
    {

		int v0 = 0;
		int v1 = 0;
		int v2 = 0;

        var g = dataUserModeler.GroupBy(i => i);

        foreach (var grp in g)
        {
			if(grp.Key == 0)
			{
				v0 = grp.Count();
			}
			else if(grp.Key == 1)
			{
				v1 = grp.Count();
			}
			else{
				v2 = grp.Count();
			}
			//Debug.Log("Ocor : " + grp.Key + "-" + grp.Count());
        }
        
		//Debug.Log("V0: " + v0 + " - V1: " + v1 + " - V2: " + v2);

		return "V0: " + v0 + " - V1: " + v1 + " - V2: " + v2;   
    }

	//---------------------------------------------------------------------------------
	public string GetDataUserModelerList () 
	{
		string s = "";

		foreach(int a in dataUserModeler){
			s += a + "|";
		}

		return s;
	}

	//---------------------------------------------------------------------------------
    void GetData () {

		dataPlayer = player.PlayerStatus();
		//dataPlayer.ToList().ForEach(i => Debug.Log("Data Player List: " + i.ToString())); //Used to confirm player dela per wave only.

		dataGame = game.GameStatus();
		//dataGame.ToList().ForEach(i => Debug.Log(i.ToString()));

        this.union_PlayerAndGameStatus.AddRange(dataGame);
        this.union_PlayerAndGameStatus.AddRange(dataPlayer);

        ProcessingData();

    }

	//---------------------------------------------------------------------------------
	void ProcessingData(){

		//ShotDelay - DelayTiro.
		float normalizeValue_Delay = Normalize((float)dataPlayer[0], 13.0f, 0.0f);
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
	//---------------------------------------------------------------------------------
	public float Normalize(float value, float valueMax, float valueMin){
		return ((value - valueMin)/valueMax);
	}

	//---------------------------------------------------------------------------------
	/*
	 * @input: (1) A list with actual player. (2) An array from players dataset.
	 * @output: Distance euclidean between their status combined.
	 * 
	 */
	float EuclideanDistance (List<float> dataPlayer, float[] dataBase) {

		float euclideanDistance = 0.0f;

		for (int x = 0; x < dataPlayer.Count ; x++){
			euclideanDistance += (float) Mathf.Pow((dataPlayer.ElementAt(x) - dataBase.ElementAt(x)),2);
		}

		return Mathf.Sqrt(euclideanDistance);
	}

	//---------------------------------------------------------------------------------
	void Verify (int v1, int v2, int v3, int qwe, int neighbors){

		try{
			if(v1 > v2 && v1 > v3)
			{
				this.playerType =  "amateur" ;
				dataUserModeler.Add(0);
			}
			
			else if (v2 > v1 && v2 > v3)
			{
				this.playerType =  "intermediate" ;
				dataUserModeler.Add(1);
			}
			
			else if (v3 > v2 && v3 > v1)
			{
				this.playerType =  "hardcore" ;
				dataUserModeler.Add(2);
			}
			
			/*
			 * Specify here a solution to problem if the classifier returned 3 different modes.
			 * In this solution above we use a k equals a random number between the old neighbors and the math.sqrt value from the size of list.
			 * 
			 */
			else if (v3 == v2 || v3 == v1 || v2 == v1)
			{
				Debug.Log("Equal!");

				int newNeighbors = UnityEngine.Random.Range(neighbors + 2, (int) Math.Sqrt(waveListUsed.Count()));

				Debug.Log("Random newNeighbors: " + newNeighbors);

				modelList.Clear();

				LoadingByDistance(newNeighbors);

				int counter = 0;

				while(counter < newNeighbors){
					qwe = modelList.ElementAt(counter);
					if (qwe == 0){ v1++; }
					else if (qwe == 1) { v2++; }
					else {v3++;}
					Debug.Log(qwe);
					counter++;
				}
				
				Verify(v1,v2,v3,qwe,newNeighbors);
			}
		}catch(Exception e) {
			Debug.LogError("Verify problem.");
			Debug.LogError(e.ToString());
		}
	}

	//---------------------------------------------------------------------------------
	void LoadingByDistance (int neighbors) {

		int counter = 0;
		int index = 0;

		while(counter < neighbors){
			
			try{
				
				if(knnDic.TryGetValue(distancesValuesSort.ElementAt(index), out value1)){
					
					foreach(Database.Cell z in value1){ 										
						
						if(counter >= neighbors) break;
						
						if (counter < neighbors) { 
							
							modelList.Add(z.GetCellModelLevel()); 
							counter++ ; 
						} 
					}						
					
					value1.Clear();
					index++;
					
				}
				
				else
				{
					index++;
				}
				
			}catch(Exception e) {
				Debug.LogWarning("Error in get and add value in model list."); 
				Debug.LogError(e.ToString());
			}
		}
	}

	//---------------------------------------------------------------------------------
	void KNN(int neighbors){

		modelList = new List<int>();
		
		/*
		 *Get Player and Game data. 
		 */
		try{
			GetData();			
		}catch{ Debug.LogError("Cannot possible get data."); }

       /*
        * Union two arrays, GameStatus and PlayerStatus;
        */

		try{	      

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

		}catch{ Debug.LogError("Cannot possible load cell info per wave. "); }

		/*
		* Moving into Cells data and calculate euclidean distance of each one inside dataset.
		*
		*/	
        foreach (Database.Cell cell_values in waveListUsed)
        {

			try{
								
				distance = EuclideanDistance(this.union_PlayerAndGameStatus, cell_values.ReturnValues());
				//Debug.Log("Distance: " + distance);
			}catch (Exception e){ Debug.LogError ("Euclidean Distance problem value: " + distance +"_" + e.ToString()); }

			/*
			 * Save every distance for use to recovery the most upcoming.
			 */
			distancesValuesSort.Add(distance);


			/*
			 * Add the value with distance as a key.
			 */
            try{

				if(knnDic.ContainsKey(distance)){
					knnDic[distance].Add(cell_values);
					//Debug.Log("Duplicated Key ADD");

				}else{
					knnDic.Add(distance, new List<Database.Cell>{cell_values});
					//Debug.Log("Everyting works fine!");
				}
							
			}catch (Exception e){

				Debug.LogError("Two value with same key: " + distance);
				Debug.LogError("Class of players: " + cell_values.GetCellModelLevel());
				Debug.LogError(e.ToString());
			}
        }

		/*
		 * Sorting the distances the lower for the most. 
		 */
		distancesValuesSort.Sort();

		LoadingByDistance(neighbors);

		try{				

			int v1 = 0;
			int v2 = 0;
			int v3 = 0;
			int counter = 0;
			int qwe = 0;

			while(counter < neighbors){
				try{
					qwe = modelList.ElementAt(counter);
					if (qwe == 0){ v1++; }
					else if (qwe == 1) { v2++; }
					else {v3++;}
					Debug.Log(qwe);

				}catch{
					Debug.LogError("Houve erro a partir. " + counter);
				}
				counter++;
			}

			/*
			 * Used to verify the class of player.
			 */
			Verify(v1,v2,v3,qwe,neighbors);
			
			Debug.Log("PLayer model > " + this.playerType);
			
			Debug.LogWarning(GetMajorOccurrence());
			
			/*Cleaning to use in another execution time, it's very important!!.*/
            knnDic.Clear();
			modelList.Clear();
			waveListUsed.Clear();
			union_PlayerAndGameStatus.Clear();
			distancesValuesSort.Clear();

		}catch(Exception e){ Debug.LogError(e.ToString()); Debug.LogError("Cannot be get value and comparated final part."); }
    }
}
