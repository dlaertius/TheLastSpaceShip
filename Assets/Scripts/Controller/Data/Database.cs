using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

/*
 * Script function is, read .txt wave files and save in a array, each wave will have an array.
 * 
 */

public class Database : MonoBehaviour {

	public TextAsset wave_1, wave_2, wave_3, wave_4,wave_5,wave_6,wave_7,wave_8,wave_9,wave_10,wave_11,wave_12;

	public class Cell
	{
		private float hitRateEnemyShip;
		private float hitRateAste;
		private float colisionRateEnemyShip;
		private float colisionRateAste;
		private float delayMed;
		private float bleachingRate; //Alvejamento.
		private float qtMoviPerSecond;
        private float campainKill;
        private int cellModelLevel;
		
		
		public Cell (float hitRateEnemyShip, float hitRateAste, float colisionRateEnemyShip, float colisionRateAste, float delayMed, float bleachingRate, float campainKill, float qtMoviPerSecond,int cellModelLevel)
		{
			this.hitRateEnemyShip = hitRateEnemyShip;
			this.hitRateAste = hitRateAste;
			this.colisionRateEnemyShip = colisionRateEnemyShip;
			this.colisionRateAste = colisionRateAste;
			this.delayMed = delayMed;
			this.bleachingRate = bleachingRate;
			this.qtMoviPerSecond = qtMoviPerSecond;
            this.campainKill = campainKill;
            this.cellModelLevel = cellModelLevel;
		}
		
		public float GetHitRateEnemyShip ()
		{
			return this.hitRateEnemyShip;
		}
		
		public float GetHitRateAste ()
		{
			return this.hitRateAste;
		}
		
		public float GetColisionRateEnemyShip ()
		{
			return this.colisionRateEnemyShip;
		}
		
		public float GetColisionRateAste ()
		{
			return this.colisionRateAste;
		}
		
		public float GetDelayMed ()
		{
			return this.delayMed;
		}
		
		public float GetBleachingRate ()
		{
			return this.bleachingRate;
		}
		
		public float GetQtMoviPerSecond ()
		{
			return this.qtMoviPerSecond;
		}

        public float GetCampainKill()
        {
            return this.campainKill;
        }


        public int GetCellModelLevel ()
		{
			return this.cellModelLevel;
		}

        public float[] ReturnValues()
        {
            float[] array = {this.hitRateEnemyShip,this.hitRateAste,this.colisionRateEnemyShip,this.colisionRateAste,this.delayMed,
                this.bleachingRate,this.qtMoviPerSecond,this.campainKill,this.cellModelLevel };
            return array;
        }

        //Only for debug.
        public string ToStringCellValues ()
		{
			return this.hitRateEnemyShip + "-" + this.hitRateAste + "-" + this.colisionRateEnemyShip + "-" + this.colisionRateAste + "-" + this.delayMed + "-" +
				this.bleachingRate + "-" + this.qtMoviPerSecond + "-" + this.campainKill + "-" + this.cellModelLevel;
		}
	}


	/*
	 * Generic list, in this case, a list of cells, each cell is a player set values read by text file.
	 * 
	 */
	public List<Cell> wave_1_cell_list;
	public List<Cell> wave_2_cell_list;
	public List<Cell> wave_3_cell_list;
	public List<Cell> wave_4_cell_list;
	public List<Cell> wave_5_cell_list;
	public List<Cell> wave_6_cell_list;
	public List<Cell> wave_7_cell_list;
	public List<Cell> wave_8_cell_list;
	public List<Cell> wave_9_cell_list;
	public List<Cell> wave_10_cell_list;
	public List<Cell> wave_11_cell_list;
	public List<Cell> wave_12_cell_list;

    

	void Loading (List<Cell> list, TextAsset waveTextFile) //, string localFile)
	{

		Cell generic;

		string[] textInFile = waveTextFile.text.Split("\n"[0]);

		foreach (string line in textInFile)
		{

			//Debug.Log("Line: " + line);

			string[] broke_string = line.Split(',');

			try{
				generic = new Cell(Single.Parse(broke_string[0]),
			                   Single.Parse(broke_string[1]),
			                   Single.Parse(broke_string[2]),
			                   Single.Parse(broke_string[3]),
			                   Single.Parse(broke_string[4]),
			                   Single.Parse(broke_string[5]),
			                   Single.Parse(broke_string[6]),
			                   Single.Parse(broke_string[7]),
			                   Int32.Parse(broke_string[8])
			                   );
				list.Add(generic);

			}catch (Exception e){
				Debug.Log(e.InnerException);
				Debug.Log("The convertions doesn't work!"); 
			} 
		}
		Debug.Log("The convertion works fine!");
	}


	void Start () 
	{

		wave_1_cell_list = new List<Cell>();
		wave_2_cell_list = new List<Cell>();
		wave_3_cell_list = new List<Cell>();
		wave_4_cell_list = new List<Cell>();
		wave_5_cell_list = new List<Cell>();
		wave_6_cell_list = new List<Cell>();
		wave_7_cell_list = new List<Cell>();
		wave_8_cell_list = new List<Cell>();
		wave_9_cell_list = new List<Cell>();
		wave_10_cell_list = new List<Cell>();
		wave_11_cell_list = new List<Cell>();
		wave_12_cell_list = new List<Cell>();

		Loading(wave_1_cell_list, wave_1);
		Loading(wave_2_cell_list, wave_2);
		Loading(wave_3_cell_list, wave_3);
		Loading(wave_4_cell_list, wave_4);
		Loading(wave_5_cell_list, wave_5);
		Loading(wave_6_cell_list, wave_6);
		Loading(wave_7_cell_list, wave_7);
		Loading(wave_8_cell_list, wave_8);
		Loading(wave_9_cell_list, wave_9);
		Loading(wave_10_cell_list, wave_10);
		Loading(wave_11_cell_list, wave_11);
		Loading(wave_12_cell_list, wave_12);

	}
}