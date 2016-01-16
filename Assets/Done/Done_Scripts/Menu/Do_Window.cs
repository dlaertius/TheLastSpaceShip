using UnityEngine;
using System.Collections;

public class Do_Window : MonoBehaviour {
	
	private int largura = Screen.width;
	private int altura = Screen.height;
	
	public Rect janela;
	
	public bool habilitar = false;
	
	private bool toogleAdap = false;
	private bool toogleEasy = false;
	private bool toogleMed = false;
	private bool toogleHard = false;
	
	public string nomeCapitao = "";
	
	//public Player player;

	public UserDataMenu userData;
	
	private bool ativaMensagemEro = false;
	private bool ativaMensagemErroSemToogle = false;
	
	void Start () {
		
		//player = FindObjectOfType(typeof(Player)) as Player;

		userData = FindObjectOfType(typeof(UserDataMenu)) as UserDataMenu;
		
		janela = new Rect(largura - largura/1.55f, altura/2.5f + 60,300,170);
	}
	
	void OnGUI () {
		if(habilitar){
			janela = GUI.Window(0, janela,DoMyWindow, "Capitan's Name");
		}
	}
	
	void DoMyWindow (int windowID){
		
		nomeCapitao = GUI.TextField(new Rect(60,35, 200, 30),nomeCapitao,10);
		
		if (ativaMensagemEro) {
			GUI.Label(new Rect(60,135,220,30), "Digite um nome de capitão válido!");
		}
		
		if(ativaMensagemErroSemToogle){
			GUI.Label(new Rect(60,135,220,30), "Escolha um nivel de dificuldade!");
		}
		
		toogleEasy = GUI.Toggle(new Rect(30,70, 100, 30), toogleEasy, "Easy");
		toogleMed = GUI.Toggle(new Rect(80,70, 100, 30), toogleMed, "Medium");
		toogleHard = GUI.Toggle(new Rect(150,70, 100, 30), toogleHard, "Hard");
		toogleAdap = GUI.Toggle(new Rect(200,70, 100, 30), toogleAdap, "Adaptable");
		
		
		if(toogleAdap)
		{
			toogleEasy = false;
			toogleMed = false;
			toogleHard = false;
			//Debug.Log(toogleAdap + ", " + tooglePadrao);
		}
		
		if(toogleEasy)
		{
			toogleAdap = false;
			toogleMed = false;
			toogleHard = false;
			//Debug.Log(toogleAdap+ ", " +tooglePadrao);
		}
		
		if(toogleMed)
		{
			toogleAdap = false;
			toogleEasy = false;
			toogleHard = false;
			
		}
		
		if(toogleHard)
		{
			toogleAdap = false;
			toogleEasy = false;
			toogleMed = false;
			
		}
		
		if(GUI.Button(new Rect(80,100,150,30), "Go!") || Input.GetKeyDown(KeyCode.Return)){

			if(nomeCapitao.Equals("") || nomeCapitao.Length <= 2){
				this.ativaMensagemEro = true;
				this.ativaMensagemErroSemToogle = false; //Para nao dar conflito quando aparecer os dois erros.
			}

			else if(nomeCapitao.Length > 2 && toogleAdap != false || toogleEasy != false || toogleMed != false || toogleEasy != false){

				this.ativaMensagemEro = false;

				Debug.Log(nomeCapitao);
		
				userData.SetName(nomeCapitao);
				userData.SetMode("med");

				//player.SetNomeJogador(nomeCapitao);
				
				/*if(toogleAdap == true && toogleEasy == false && toogleMed == false && toogleEasy == false) player.SetGameMode("adapt");
				else if(toogleAdap == false && toogleEasy == true && toogleMed == false && toogleEasy == false) player.SetGameMode("easy");
				else if(toogleAdap == false && toogleEasy == false && toogleMed == true && toogleEasy == false) player.SetGameMode("med");
				else if(toogleAdap == false && toogleEasy == false && toogleMed == false && toogleEasy == true) player.SetGameMode("hard");
				
				Debug.Log(player.GetGameMode());*/
				
				Application.LoadLevel(2);
				
			}else if (toogleAdap == false && toogleEasy == false && toogleMed == false && toogleEasy == false){
				this.ativaMensagemErroSemToogle = true;
				this.ativaMensagemEro = false;	//Para nao dar conflito quando aparecer os dois erros.
			}
		}
	}
	
	void switchStatusToogle (bool toogle){
		if(toogle == true) toogle = false;
		else toogle = true;
		Debug.Log("Valor " + toogle);
	}
}
