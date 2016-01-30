using UnityEngine;
using System.Collections;

public class Done_GameController : MonoBehaviour{
	

	private int numeroDeOndas = 12;
	public int numeroDaOnda;

	/*
	 * Atributos modelaveis referetes as ondas.
	 * 
	 */
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	private float waveWait;

	/*
	 * Caso o usuario morra na metade da onda, ele iria passar a onda que ele morreu, prejudicando a modelagem.
	 */
	public int numeroDaOndaParaMedia;

	public int tempoTotal;
	public int alvosAcertados;
	public int elementosShow;
	public int onda100Kill;
	public int alvosPorOnda; //Objetivo de verificar se todos os mobs da onda foram mortos. Pode Indicar Pericia. Facilidade.

	private bool gameOver;
	private bool restart;
	private bool finishGame = false;

	public int score;
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText mensagemCentral;
	private int ondaShow; //Utilizada pra mostrar no painel do lado a onda de acordo com o estagio.

	public int tempoJogo = 0;
	public int elementosQueCruzaramAFronteira = 0; //Numero de elmentos que cruzaram a fronteira (broundary).
	public bool jogoQueSegue = false;
	public int diminuirNoDelay = 0;


	/*
	 * Usados para calculos de taxas, represetam TODAS as naves ou asteroides do jogo. 
	 */
	public int navesTotais = 0;
	public int asteroidesTotais = 0;
	
	/*
	 * Tiros Disparados Naves.
	 */
	public int tirosDisparadosNaves;

	/*
	 * Usado para calculo da TAXA DE COLISAO COM NAVES.
	 */
	public int navesColididas = 0;

	/*
	 * Usado para calculo da TAXA DE COLISAO COM ASTEROIDES.
	 */
	public int asteroidesColididos = 0;

	/*
	 * Usado para calculo da Taxa de Destruicao de Naves.
	 */
	public int totalDeNavesDestruidas = 0;

	/*
	 * Usado para calculo da Taxa de Destruicao de Naves.
	 */
	public int totalDeAsteroidesDestruidos = 0;

	/*
	 * Usada para nao capturar o delay entre as ondas.
	 */
	public bool contaDelay = false;

	/*
	 * Usada para nao evitar que uma onda venha antes de ter acabado a outra.
	 */
	public bool todosPassaram = true;

	/*
	 * Para armazenar o nome do capitao da nave colhida na janela inicial.
	 */
	public Player player;

	/*
	 * Para efetuar a operacao de escrita e leitura do arquivo.csv
	 */
	public Coletor coletor;

	public WaveController waveController;

	void Start (){

		player = FindObjectOfType(typeof(Player)) as Player; //Porque usar ele?

		coletor.gameObject.GetComponent<Coletor>();

		InitialStatusGame();

		UpdateScore ();

		StartCoroutine (SpawnWaves ());

		waveController.InitialStatusMobs();

		waveController.InitialStatusPlayer();
	}
	
	void Update (){

		if (gameOver != true){
			UpdateScore();
			tempoTotal = (int) Time.timeSinceLevelLoad;

		}

		if (restart){
			if (Input.GetKeyDown (KeyCode.R)){
				Application.LoadLevel (Application.loadedLevel);
			}else if(Input.GetKeyDown (KeyCode.Home)){
				Application.LoadLevel(1);
			}else if (Input.GetKeyDown(KeyCode.Escape))
				Application.Quit();
		}

		if(this.elementosQueCruzaramAFronteira + this.alvosPorOnda == this.hazardCount) {

			if (!this.todosPassaram) Debug.Log ("Já pode passar!");

			this.todosPassaram = true;
		}

	}

		
	void OnGUI () {
		if(gameOver != true){
			if(GUI.Button(new Rect(10, 110, 80, 25), "Menu")){
				Application.LoadLevel(1);
			}
		}
	}

	/*
	 * Controlador e Gerador das Ondas.
	 * 
	 */

	IEnumerator SpawnWaves (){

		DebugCheckUpInitial();

		//Debug.Log(player.GetNomeJogador() + '-' + player.GetGameMode());
		
		yield return new WaitForSeconds (startWait);
		
		while (numeroDaOnda <= numeroDeOndas + 1){

			//Funçao de zeramento.
			if(numeroDaOnda == numeroDeOndas + 1){
				this.finishGame = true;
			}

			TextoMensagemCentral("Wave " + this.numeroDaOnda);

			this.contaDelay = true;
			yield return new WaitForSeconds(waveWait);
			this.contaDelay = false;

			LimpaTextoTela();
			
			int quantidadeNaves = 0;
			int quantidadeAste = 0;

			this.todosPassaram = false;

			for (int i = 0; i < hazardCount; i++)
			{
				if(!gameOver){
					
					int setando = Random.Range(0,2); //Mob escolhido aleatoriamente.
					int mobCorrente = 0;
					
					if(setando == 0){
						if(quantidadeAste < hazardCount * 0.6f - 1 ){
							mobCorrente = waveController.GetGenericShipOrAste('a',this.numeroDaOnda);
							quantidadeAste++;
							this.asteroidesTotais++;
						}else{
							mobCorrente = waveController.GetGenericShipOrAste('n',this.numeroDaOnda);
							quantidadeNaves++;
							this.navesTotais++;
						}
					}else{
						if(quantidadeNaves < hazardCount * 0.4f - 1 ){
							mobCorrente = waveController.GetGenericShipOrAste('n',this.numeroDaOnda);
							quantidadeNaves++;
							this.navesTotais++;
						}else{
							mobCorrente = waveController.GetGenericShipOrAste('a',this.numeroDaOnda);
							quantidadeAste++;
							this.asteroidesTotais++;
						}
					}
					
					GameObject hazard = hazards [mobCorrente];
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (hazard, spawnPosition, spawnRotation);
					yield return new WaitForSeconds (spawnWait);

					//Debug.LogError ("elementosQueCruzaramAFronteira" + this.elementosQueCruzaramAFronteira + "- alvosPorOnda:" +
					                //"" + this.alvosPorOnda + " - hazardCount:" + this.hazardCount);
					
				}
			}

			if(!todosPassaram && !gameOver){

				Debug.LogError("Entrou aqui!");

				bool x = false;

				while(!x){

					if(todosPassaram){
						this.elementosQueCruzaramAFronteira = 0;
						break;
					}else{
						this.contaDelay = true;
						yield return new WaitForSeconds(1);
						this.contaDelay = false;
					}

					Debug.LogError ("elementosQueCruzaramAFronteira" + this.elementosQueCruzaramAFronteira + "- alvosPorOnda:" +
					                "" + this.alvosPorOnda + " - hazardCount:" + this.hazardCount);
				}

			}

			if (!gameOver){ 

				this.numeroDaOndaParaMedia = this.numeroDaOnda;

				Onda100KillConfirma();

				this.numeroDaOnda++;
				//Debug.Log("Numero da onda: " + this.numeroDaOnda);
				
				waveController.IncreaseWave();
					
				waveController.IncreaseElements(this.numeroDaOnda);

				foreach(float element in GameStatus()){
					Debug.Log(element);
				}

				Debug.Log(player.MediaDelaysJogador());
				Debug.Log(player.MediaTirosLevados());
				Debug.Log(player.MediaCampanha100Kill());
				Debug.Log(player.MediaCampanhaMovimentoPorSegundo());

				Debug.Log(OrganizadorDeDados());
			}
			
			// CASO A ENERGIA ACABE.
			if (gameOver){

				restartText.text = "PRESS 'R' TO RESTART \n PRESS 'HOME' TO MENU \n PRESS 'ESC' TO EXIT";

				restart = true;

				//coletor.SaveToFile(OrganizadorDeDados()); // Salvando dados ao final.

				//Debug.Log(OrganizadorDeDados()); //Temporario.

				player.DestruirObjetoJogador();

				break;
			}
		}
	}

	public void AddScore (int newScoreValue){
		score += newScoreValue;
		UpdateScore ();
	}

	public void TextoMensagemCentral (string texto){
		this.mensagemCentral.text = texto;
	}

	public void LimpaTextoTela(){
		this.mensagemCentral.text = "";
	}

	/*
	 * Usado para verificar se todos os mobs da onda foram destruidos.
	 */
	public void Onda100KillConfirma ()
	{
		//obserando que ambos começam do 0 ou seja, 5 elementas equivalera a 4 na logica da variavel.
		if (this.alvosPorOnda == this.hazardCount)
		{ 
			this.onda100Kill += 1; //Bom.
			Debug.Log("ONDA 100%KILL!");
		}else
		{
			this.onda100Kill += 0; //Ruim, nao conseguiu 100% da onda.
		}
		
		this.alvosPorOnda = 0;
	}
	/*
	 * Usada para tirar uma media dos delays somados pela quantidade de delays.
	 */

	public void GameOver ()
	{
		mensagemCentral.text = "YOU LOSE!!!";
		gameOver = true;
	}

	public string Temporizador (int tempo)
	{

		string  segundos = (tempo %60).ToString();
		string  minutos = ((tempo/60)%60).ToString();
		string  horas = (tempo/86400).ToString();
		if(segundos.Length < 2){
			segundos = "0" + segundos;
		}
		if(minutos.Length < 2){
			minutos = "0" + minutos;
		}
		
		return "0" + horas + ":" + minutos + ":" + segundos;	
	}

	/*
	 * This original version used double values.
	 * A versao original usou valores double.
	 */
	public float[] GameStatus(){
		float[] gameStatusPlayer = {player.CalculaTaxaGenerica(this.totalDeNavesDestruidas,this.navesTotais), 
			player.CalculaTaxaGenerica(this.totalDeAsteroidesDestruidos, this.asteroidesTotais),
			player.CalculaTaxaGenerica(this.navesColididas,this.navesTotais),player.CalculaTaxaGenerica(this.asteroidesColididos, this.asteroidesTotais)};
		
		return gameStatusPlayer;
	}

	
	public string OrganizadorDeDados () 
	{
		return (this.numeroDaOnda + "," + player.CalculaTaxaGenerica(this.totalDeNavesDestruidas,this.navesTotais) + ","
		        + player.CalculaTaxaGenerica(this.totalDeAsteroidesDestruidos, this.asteroidesTotais) + "," + 
		        player.CalculaTaxaGenerica(this.navesColididas,this.navesTotais) + "," + player.CalculaTaxaGenerica(this.asteroidesColididos, this.asteroidesTotais)
		        + "," + player.MediaDelaysJogador() + "," + player.MediaTirosLevados() + "," + player.MediaCampanha100Kill() + "," + player.MediaCampanhaMovimentoPorSegundo() + "," + this.player.GetNomeJogador());
	}

	public float GetVida_Jogador(){
		return player.GetVidaJogador();
	}

	public double TaxaDeAcertoGeral (){
		double taxaDeAcerto;
		
		if(this.alvosAcertados != 0 && player.tirosDisparados != 0){
			taxaDeAcerto = ((double) this.alvosAcertados)/((double) (player.tirosDisparados));
			taxaDeAcerto = System.Math.Round(taxaDeAcerto, 2); //Pegando apenas dois digitos apos o "0.".
		}else{
			taxaDeAcerto = 0.0;
		}
		//Debug.Log("Acerto: " + taxaDeAcerto);
		
		Debug.Log("Alvos acertados: " + this.alvosAcertados);

		return taxaDeAcerto;
	}
	/*
	 * Tornando o dano dinamico pela onda.
	 */

	public void SetDanoRecebido(){

		int dano;

		if (this.numeroDaOnda <= 6) dano = 15;
		else if (this.numeroDaOnda > 6 && this.numeroDaOnda <= 12) dano = 20;
		else dano = 25;

		player.DanoRecebido(dano);
	}

	public float GetTempoTotal () {
		return tempoTotal;
	}

	public void SetAlvoAcertado () {

		this.alvosPorOnda++; //Pega apenas o valor de monstros mortos na onda, tem dependencia com o while par esperar todos os mobs passarem.

		this.alvosAcertados++; // Pega todos os alvos acertados durante a partida.
	}

	public void SetAlvoColidido () {
		this.alvosPorOnda++; //Pega apenas o valor de monstros mortos na onda, tem dependencia com o while par esperar todos os mobs passarem.
	}

	public void SetJogadorLevouUmTiro(){
		player.SetTiroLevado();
	}

	/*
	 * Atualizando as informaçoes do jogador.
	 */
	void UpdateScore (){
		scoreText.text = "Score: " + score + "\n" + "Energy: " + player.GetVidaJogador() + "%" + "\n" 
			+ "Time: " + Temporizador(tempoTotal) + 
				/*"\n" + "Elementos: " + this.elementosShow +*/ "\n" +
				"Capitan " + player.GetNomeJogador() + "\n" +
				"Wave " + this.numeroDaOnda;
	}

	void DebugCheckUpInitial(){
		Debug.Log("Player: " + player.InitialStatusPlayerDebug());
	}
	
	/*
	 * Resentando todos os atributos para possibilitar um reinicio de jogo do 0.
	 * 
	 */
	void InitialStatusGame () {
		this.onda100Kill = 0;
		this.numeroDaOnda = 1;
		this.waveWait = 2;
		this.gameOver = false;
		this.restart = false;
		this.restartText.text = "";
		this.mensagemCentral.text = "";
		this.score = 0;
		this.alvosAcertados = 0;
	}
}