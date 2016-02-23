using UnityEngine;
using System.Collections;

public class Done_Menu : MonoBehaviour {

	public int screenX = Screen.width;
	public int screenY = Screen.height;

	private Do_Window janela;

	private bool playClicado = false;



	// Use this for initialization
	void Start () {
	
		janela = FindObjectOfType(typeof(Do_Window)) as Do_Window;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {

		if(playClicado != true){
			if (GUI.Button(new Rect(screenX - screenX/2.1f, screenY/2 + 45, 150, 35), "Play")){
				janela.habilitar = true;
				playClicado = true;
			}
		}
		/*if (GUI.Button(new Rect(screenX - screenX/5, screenY/0.87f, 120, 35), "About")){
			print("Sobre");
		}*/else if(GUI.Button(new Rect(screenX - screenX/1.37f, screenY/0.87f, 120, 35), "Exit")){
			print ("Saiu");
			Application.Quit();
		}
	}
}
