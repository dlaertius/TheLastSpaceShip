using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	private string nomeJogador;

	private string mode; //= "hard"
	
	//Quanto de vida o jogador tem;
	private int life = 100;
	//Tiros levados.
	private int tirosLevados = 0;
	//Quanto de laser ele disparou;
	public int tirosDisparados = 0;
	//Verificar Qual o maior delay entre disparos.
	public float maiorDelay = 0;
	
	//Usado para calcular a media dos delays.
	public int quantidadeDeDelays;
	public float somaDosDelays;

	public float speedPlayer;
	public float fireRatePlayer;
	public float tiltPlayer;
	
	public float tempoDoUltimoDisparo = 0;
	//Capturando intensa movimentaçao do jogador.
	public int quantidadeDeMovimentos = 0;

	public Done_GameController game;

	public UserDataMenu userData;

	void Awake () {
		userData = FindObjectOfType(typeof(UserDataMenu)) as UserDataMenu;
		SetPlayerName();
		SetGameMode();
		userData.DestroyUserDataMenu();
	}
	
	// Use this for initialization
	void Start () {

		game = FindObjectOfType(typeof(Done_GameController)) as Done_GameController;
		
	}
	
	// Update is called once per frame
	void Update () {}
	
	public void DestruirObjetoJogador () {
		Destroy(this);
	}

	public string DadosToString () {
		return "Tiros: " + this.tirosDisparados + " Maior Delay: " + this.maiorDelay + " Energia: " + this.life;
	}
	
	public void EstadoInicial () {
		this.speedPlayer = 10;
		this.fireRatePlayer = 0.3f;
		this.tiltPlayer = 4;
	}

	public void DanoRecebido(int dano){
		if(this.life - dano <= 0)
			this.life = 0;
		else 
			this.life -= dano;
	}
	
	public void VerificaMaiorDelay (float tempo){
		float delayTemp = tempo - tempoDoUltimoDisparo;
		if(delayTemp > maiorDelay){
			maiorDelay = delayTemp;
		}
	}
	
	public void CalculaDelays(float tempo) {
		
		float delayTemp = tempo - this.tempoDoUltimoDisparo;
		
		if (delayTemp >= 3f) //3 representa o tempo necessario pra ser considerado delay.
		{
			somaDosDelays += delayTemp;
			
			quantidadeDeDelays++;
		}
	}

	public void SetPlayerName () {
		this.nomeJogador = userData.GetName();
	}
	public void SetGameMode () {
		this.mode = userData.GetMode();
	}
	
	public string GetNomeJogador () {
		//SetPlayerName();
		return nomeJogador;
	}
	public string GetGameMode () {
		//SetGameMode();
		return mode;
	}

	public int GetVidaJogador(){
		return this.life;
	}

	public float GetDelay () {
		return this.maiorDelay;
	}
	
	public void SetTiroLevado (){
		this.tirosLevados++;
	}
	
	public int GetTirosLevados(){
		return this.tirosLevados;
	}
	
	public int GetQuantidadeMovimentos () {
		return this.quantidadeDeMovimentos;
	}

	public string InitialStatusPlayerDebug(){
		return GetGameMode() + " - " + GetNomeJogador() + "-" + GetQuantidadeMovimentos() + " - " 
			+ GetTirosLevados() + "-" + GetVidaJogador();
	}

	public double CalculaTaxaGenerica (int v1, int v2) 
	{
		double taxa;
		
		if(v1 != 0 && v2 != 0){
			taxa = ((double) v1)/((double) (v2));
			taxa = System.Math.Round(taxa, 2); //Pegando apenas dois digitos apos o "0.".
		}else{
			taxa = 0.0;
		}
		
		return taxa;
	}
	
	public double CalculaTaxaGenericaFloat (float v1, int v2) 
	{
		double taxa;
		
		if(v1 != 0.0f && v2 != 0){
			taxa = ((double) v1)/((double) (v2));
			taxa = System.Math.Round(taxa, 2); //Pegando apenas dois digitos apos o "0.".
		}else{
			taxa = 0.0;
		}
		
		return taxa;
	}

	public double MediaDelaysJogador () 
	{
		if(this.tirosDisparados < 2)
		{
			return (-1); //Não efetuou nenhum tiro.
		}
		else return (CalculaTaxaGenericaFloat(this.somaDosDelays, this.quantidadeDeDelays));
	}
	
	/*
	 * Usada para tirar uma media das ondas que foram 100% kill com todas as ondas passsadas. 
	 */
	public double MediaCampanha100Kill () 
	{
		return (CalculaTaxaGenerica(game.onda100Kill, game.numeroDaOndaParaMedia));
	}
	
	/*
	 * Usada para tirar uma media dos movimentos/segundo do jogador.
	 */
	public double MediaCampanhaMovimentoPorSegundo () 
	{
		return (CalculaTaxaGenerica(this.GetQuantidadeMovimentos(), game.tempoTotal));
	}
	
	/*
	 * Usada para tirar uma media dos tiros levados (jogador), pelos disparados totais pelas naves.
	 */
	public double MediaTirosLevados () 
	{
		return (CalculaTaxaGenerica(GetTirosLevados(), game.tirosDisparadosNaves));
	}

	public double[] PlayerStatus(){

		double[] playerStatistics = {MediaDelaysJogador(), MediaTirosLevados(), MediaCampanha100Kill(), MediaCampanhaMovimentoPorSegundo()};
		Debug.Log (playerStatistics);
		return playerStatistics;
	}
}

