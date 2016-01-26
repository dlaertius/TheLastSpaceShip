using UnityEngine;
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
		private int cellModelLevel;
		
		
		public Cell (float hitRateEnemyShip, float hitRateAste, float colisionRateEnemyShip, float colisionRateAste, float delayMed, float bleachingRate, float qtMoviPerSecond,int cellModelLevel)
		{
			this.hitRateEnemyShip = hitRateEnemyShip;
			this.hitRateAste = hitRateAste;
			this.colisionRateEnemyShip = colisionRateEnemyShip;
			this.colisionRateAste = colisionRateAste;
			this.delayMed = delayMed;
			this.bleachingRate = bleachingRate;
			this.qtMoviPerSecond = qtMoviPerSecond;
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

		
		public float GetCellModelLevel ()
		{
			return this.cellModelLevel;
		}

		//Only for debug.
		public string ToStringCellValues ()
		{
			return this.hitRateEnemyShip + "-" + this.hitRateAste + "-" + this.colisionRateEnemyShip + "-" + this.colisionRateAste + "-" + this.delayMed + "-" +
				this.bleachingRate + "-" + this.qtMoviPerSecond + "-" + this.cellModelLevel;
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

		//string[] lines = System.IO.File.ReadAllLines(localFile); // localfile = "base/base.txt";

		//foreach (string line in localFile) {

		string[] textInFile = waveTextFile.text.Split("\n"[0]);

		foreach (string line in textInFile)
		{

			Debug.Log(line);
			/*
			string[] broke_string = line.Split(',');

			generic = new Cell(float.Parse(broke_string[0], CultureInfo.InvariantCulture.NumberFormat),
			                   float.Parse(broke_string[1], CultureInfo.InvariantCulture.NumberFormat),
			                   float.Parse(broke_string[2], CultureInfo.InvariantCulture.NumberFormat),
			                   float.Parse(broke_string[3], CultureInfo.InvariantCulture.NumberFormat),
			                   float.Parse(broke_string[4], CultureInfo.InvariantCulture.NumberFormat),
			                   float.Parse(broke_string[5], CultureInfo.InvariantCulture.NumberFormat),
			                   float.Parse(broke_string[6], CultureInfo.InvariantCulture.NumberFormat),
			                   int.Parse(broke_string[7]));


			list.Add(generic);*/

			//Debug.Log(line);
		}
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

		/*Debug.Log ("AQUI OOOh!: ");

		Debug.Log(wave_1_cell_list[0].ToStringCellValues());
		Debug.Log(wave_1_cell_list[1].ToStringCellValues());
		Debug.Log(wave_1_cell_list[2].ToStringCellValues());

		*/
	}
}
