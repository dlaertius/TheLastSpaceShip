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

	public UserDataMenu userData;
	
	private bool ativaMensagemEro = false;
	private bool ativaMensagemErroSemToogle = false;
	
	void Start () {

		userData = FindObjectOfType(typeof(UserDataMenu)) as UserDataMenu;
		
		janela = new Rect(largura - largura/1.55f, altura/2.5f + 60,300,170);
	}
	
	void OnGUI () {
		if(habilitar){
			janela = GUI.Window(0, janela,DoMyWindow, "Capitan's Name");
		}

		//Debug.Log("T. Eas: " + this.toogleEasy + ", T.Med: " + this.toogleMed + ", T.Hard: " + this.toogleHard + ", T.Adapt: " + this.toogleAdap);
	}
	
	void DoMyWindow (int windowID){
		
		nomeCapitao = GUI.TextField(new Rect(60,35, 200, 30),nomeCapitao,10);
		
		if (ativaMensagemEro) {
			GUI.Label(new Rect(60,135,220,30), "Input a capitan's name valid!");
		}
		
		if(ativaMensagemErroSemToogle){
			GUI.Label(new Rect(60,135,220,30), "Choose a difficulty level!");
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
		}
		
		if(toogleEasy)
		{
			toogleAdap = false;
			toogleMed = false;
			toogleHard = false;
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

			else if(nomeCapitao.Length > 2 && toogleAdap != false || toogleEasy != false || toogleMed != false || toogleHard != false){

				this.ativaMensagemEro = false;

				Debug.Log(nomeCapitao);
		
				userData.SetName(nomeCapitao);
				
				if(toogleAdap == true && toogleEasy == false && toogleMed == false && toogleHard == false) userData.SetMode("adapt");
				else if(toogleAdap == false && toogleEasy == true && toogleMed == false && toogleHard == false) userData.SetMode("easy");
				else if(toogleAdap == false && toogleEasy == false && toogleMed == true && toogleHard == false) userData.SetMode("med");
				else if(toogleAdap == false && toogleEasy == false && toogleMed == false && toogleHard == true) userData.SetMode("hard");
				
				Debug.Log(userData.GetMode());
				
				Application.LoadLevel(2);
				
			}else if (toogleAdap == false && toogleEasy == false && toogleMed == false && toogleHard == false){

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
