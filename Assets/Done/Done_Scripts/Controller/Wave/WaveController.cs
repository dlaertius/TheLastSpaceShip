using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour {

	/*
	 *  Atributos Inicias do Asteroide.
	 * 
	 */
	public int scoreValue_Asteroide = 10;
	public int tumbleAsteroide = 5;
	public int speedAsteroide = -4;
	
	/*
	 * Atributos Iniciais da Nave Roxa.
	 * 
	 */
	public int scoreValue_NaveRoxa = 20;
	public float fireRateNave_Roxa = 1.5f;
	public float delayNave_Roxa = 1.5f;
	public float speedNave_Roxa = -4.0f;
	public int tiltNave_Roxa = 10;
	public int dodgeNave_Roxa = 5;
	
	/*
	 * Atributos Iniciais da Nave Vermellha.
	 * 
	 */
	public int scoreValue_NaveVermellha = 40;
	public float fireRateNave_Vermellha = 1.0f;
	public float delayNave_Vermellha = 1.0f;
	public float speedNave_Vermellha = -6.0f;
	public int tiltNave_Vermellha = 19;
	public int dodgeNave_Vermellha = 6;
	
	/*
	 * Atributos Iniciais da Nave Branca.
	 * 
	 */
	public int scoreValue_NaveBranca = 80;
	public float fireRateNave_Branca = 0.8f;
	public float delayNave_Branca = 0.8f;
	public float speedNave_Branca = -7.4f;
	public int tiltNave_Branca = 22;
	public int dodgeNave_Branca = 8;
	
	/*
	 * Get Components
	 */
	
	//Asteroide 1.
	public GameObject asteroide1;
	private Done_Mover velocidadeAsteroide_1;
	
	//Asteroide 2.
	public GameObject asteroide2;
	private Done_Mover velocidadeAsteroide_2;
	
	//Asteroide 3.
	public GameObject asteroide3;
	private Done_Mover velocidadeAsteroide_3;
	
	//Nave Roxa
	public GameObject naveRoxa;
	private Done_Mover velocidadeNaveRoxa;
	private Done_WeaponController armasNaveRoxa;
	private Done_EvasiveManeuver evazaoNaveRoxa;
	//Nave Vermelha
	public GameObject naveVermelha;
	private Done_Mover velocidadeNaveVermelha;
	private Done_WeaponController armasNaveVermelha;
	private Done_EvasiveManeuver evazaoNaveVermelha;
	//Nave Branca
	public GameObject naveBranca;
	private Done_Mover velocidadeNaveBranca;
	private Done_WeaponController armasNaveBranca;
	private Done_EvasiveManeuver evazaoNaveBranca;

	/*
	 * This two scripts above will be used to set the difficult.
	 * Estes dois scripts abaixo serao usados para setar as dificuldades.
	 * 
	 */

	public Player player;
	
	public Done_GameController game;
	
	/*
	 * This script above will be used to this object (WaveController) watch a variable call playerLevel and adapt the difficult.
	 * Este script abaixo sera usado por esse objeto (WaveController) para observar a variavel playerLevel e adaptar a dificultade.
	 */

	public Modeler modeler;


	void Start () {

		game = FindObjectOfType(typeof(Done_GameController)) as Done_GameController;

		player = FindObjectOfType(typeof(Player)) as Player;

		modeler = FindObjectOfType(typeof(Modeler)) as Modeler;

		/*
		 * Adiquirindo os script para efetuar a modificaçao por "Onda".
		 */
		velocidadeAsteroide_1 = asteroide1.GetComponent<Done_Mover>();
		velocidadeAsteroide_2 = asteroide2.GetComponent<Done_Mover>();
		velocidadeAsteroide_3 = asteroide3.GetComponent<Done_Mover>();
		
		velocidadeNaveRoxa = naveRoxa.GetComponent<Done_Mover>();
		armasNaveRoxa = naveRoxa.GetComponent<Done_WeaponController>();
		evazaoNaveRoxa = naveRoxa.GetComponent<Done_EvasiveManeuver>();
		
		velocidadeNaveVermelha = naveVermelha.GetComponent<Done_Mover> ();
		armasNaveVermelha = naveVermelha.GetComponent<Done_WeaponController>();
		evazaoNaveVermelha = naveVermelha.GetComponent<Done_EvasiveManeuver>();
		
		velocidadeNaveBranca = naveBranca.GetComponent<Done_Mover>();
		armasNaveBranca = naveBranca.GetComponent<Done_WeaponController>();
		evazaoNaveBranca = naveBranca.GetComponent<Done_EvasiveManeuver>();
	}
	
	// Update is called once per frame
	void Update () {

		/*if(player.GetGameMode().Equals("adapt")){
			//Adapta.
		}*/
	}

	/*
	 * Definindo asteroides e naves de acordo com o array que eu conheco no gameC.
	 */
	public int GetGenericShipOrAste (char type, int wave){
		if(type.Equals('a') && wave <= 4) return 0;
		else if (type.Equals('a') && wave > 4 && wave <= 8) return 1;
		else if (type.Equals('a') && wave > 8 && wave <= 12)return 2;
		else if(type.Equals('n') && wave <= 4) return 3;
		else if (type.Equals('n') && wave > 4 && wave <= 8) return 4;
		else return 5;
	}
	
	public void InitialStatusMobs() {

		this.velocidadeAsteroide_1.speed = this.speedAsteroide;
		this.velocidadeAsteroide_2.speed = this.speedAsteroide;
		this.velocidadeAsteroide_3.speed = this.speedAsteroide;
		
		//Debug.Log(" Atrib Iniciais: Ast 1 : " + this.velocidadeAsteroide_1.speed + " Ast 2: " + this.velocidadeAsteroide_2.speed + " Ast 3: " + this.velocidadeAsteroide_3.speed);
		
		this.velocidadeNaveRoxa.speed = this.speedNave_Roxa;
		this.armasNaveRoxa.fireRate = this.fireRateNave_Roxa;
		this.armasNaveRoxa.delay = this.delayNave_Roxa;
		this.evazaoNaveRoxa.tilt = this.tiltNave_Roxa;
		this.evazaoNaveRoxa.dodge = this.dodgeNave_Roxa;
		
		this.velocidadeNaveVermelha.speed = this.speedNave_Vermellha;
		this.armasNaveVermelha.fireRate = this.fireRateNave_Vermellha;
		this.armasNaveVermelha.delay = this.delayNave_Vermellha;
		this.evazaoNaveVermelha.tilt = this.tiltNave_Vermellha;
		this.evazaoNaveVermelha.dodge = this.dodgeNave_Vermellha;
		
		this.velocidadeNaveBranca.speed = this.speedNave_Branca;
		this.armasNaveBranca.fireRate = this.fireRateNave_Branca;
		this.armasNaveBranca.delay = this.delayNave_Branca;
		this.evazaoNaveBranca.tilt = this.tiltNave_Branca;
		this.evazaoNaveBranca.dodge = this.dodgeNave_Branca;
	}

	//Arrumar.
	public void InitialStatusPlayer (){
		player.speedPlayer = 10;
		player.fireRatePlayer = 0.3f;
	}
	
	public void JogadorBonusPorOnda () {
		//jogador.speed += 0.5f;
		//jogador.fireRate -= 0.01f; //Mnos e mais.
	}

	public void IncreaseElements (int wave) {

		if (wave <= 4) {
			if(player.GetGameMode() == "easy") EasyModeAdd(velocidadeNaveRoxa, armasNaveRoxa, evazaoNaveRoxa);
			
			else if(player.GetGameMode() == "med") MediumModeAdd(velocidadeNaveRoxa, armasNaveRoxa, evazaoNaveRoxa);
			
			else if(player.GetGameMode() == "hard") HardModeAdd(velocidadeNaveRoxa, armasNaveRoxa, evazaoNaveRoxa);
			
			else if(player.GetGameMode().Equals("adapt")) AdaptModeAdd(velocidadeNaveRoxa, armasNaveRoxa, evazaoNaveRoxa);

		}else if (wave > 4 && wave <= 8){
			if(player.GetGameMode() == "easy") EasyModeAdd(velocidadeNaveVermelha, armasNaveVermelha, evazaoNaveVermelha);
			
			else if(player.GetGameMode() == "med") MediumModeAdd(velocidadeNaveVermelha, armasNaveVermelha, evazaoNaveVermelha);
			
			else if(player.GetGameMode() == "hard") HardModeAdd(velocidadeNaveVermelha, armasNaveVermelha, evazaoNaveVermelha);
			
			else if(player.GetGameMode().Equals("adapt")) AdaptModeAdd(velocidadeNaveVermelha, armasNaveVermelha, evazaoNaveVermelha);

		}else if(wave > 8 && wave <= 12){
			if(player.GetGameMode() == "easy") EasyModeAdd(velocidadeNaveBranca, armasNaveBranca, evazaoNaveBranca);
			
			else if(player.GetGameMode() == "med") MediumModeAdd(velocidadeNaveBranca, armasNaveBranca, evazaoNaveBranca);
			
			else if(player.GetGameMode() == "hard") HardModeAdd(velocidadeNaveBranca, armasNaveBranca, evazaoNaveBranca);
			
			else if(player.GetGameMode().Equals("adapt")) AdaptModeAdd(velocidadeNaveBranca, armasNaveBranca, evazaoNaveBranca);
		}
	}

	/*
	 * Alterando atributos da onda no game controller.
	 */
	public void IncreaseWave () {

		if(player.GetGameMode() == "easy") game.spawnWait -= 0.1f; // Decrementando -0.4 enquanto maior que 1.

		else if(player.GetGameMode() == "med") game.spawnWait -= 0.15f; // Decrementando -0.4 enquanto maior que 1.

		else if(player.GetGameMode() == "hard") game.spawnWait -= 0.2f; // Decrementando -0.4 enquanto maior que 1.

		else if(player.GetGameMode().Equals("adapt")) game.spawnWait -= 0.4f; // Decrementando -0.4 enquanto maior que 1.
		
		game.hazardCount += 5; // Aumetnando o numero de mobs por onda.
	}

	void EasyMode (Done_Mover velo, Done_WeaponController armas, Done_EvasiveManeuver evazao){}

	void EasyModeAdd (Done_Mover velo, Done_WeaponController armas, Done_EvasiveManeuver evazao) {

		//Ships
		armas.fireRate -= 0.05f;
		armas.delay -= 0.05f;
		velo.speed -= 0.1f;
		evazao.tilt += 0.2f;
		evazao.dodge += 0.2f;

		//Ast
		this.velocidadeAsteroide_1.speed -= 0.1f;
		this.velocidadeAsteroide_2.speed -= 0.1f;
		this.velocidadeAsteroide_3.speed -= 0.1f;
	}

	void MediumMode(Done_Mover velo, Done_WeaponController armas, Done_EvasiveManeuver evazao){}

	void MediumModeAdd(Done_Mover velo, Done_WeaponController armas, Done_EvasiveManeuver evazao){

		//Ships
		armas.fireRate -= 0.1f;
		armas.delay -= 0.1f;
		velo.speed -= 0.5f;
		evazao.tilt += 0.3f;
		evazao.dodge += 0.3f;

		//Ast
		this.velocidadeAsteroide_1.speed -= 0.2f;
		this.velocidadeAsteroide_2.speed -= 0.2f;
		this.velocidadeAsteroide_3.speed -= 0.2f;
	}

	void HardMode(Done_Mover velo, Done_WeaponController armas, Done_EvasiveManeuver evazao){}

	void HardModeAdd(Done_Mover velo, Done_WeaponController armas, Done_EvasiveManeuver evazao){

		//Ships
		armas.fireRate -= 0.5f;
		armas.delay -= 0.5f;
		velo.speed -= 1.0f;
		evazao.tilt += 1.0f;
		evazao.dodge += 1.0f;

		//Ast
		this.velocidadeAsteroide_1.speed -= 1f;
		this.velocidadeAsteroide_2.speed -= 1f;
		this.velocidadeAsteroide_3.speed -= 1f;
	}

	void AdaptMode (Done_Mover velo, Done_WeaponController armas, Done_EvasiveManeuver evazao){}

	/*
	 * If the user was classify with amateur, the game will add easy level features, and ..
	 * Se o usuario for classificadao como amador, o jogo ira adicionar caracteristicas de nivel facil, e ..
	 * 
	 */
	void AdaptModeAdd (Done_Mover velo, Done_WeaponController armas, Done_EvasiveManeuver evazao){

		if(modeler.GetPlayerType() == "amateur"){
			EasyModeAdd(velo, armas, evazao);
			Debug.Log("Amateur");

		}else if (modeler.GetPlayerType() == "intermediate"){
			MediumModeAdd(velo,armas,evazao);
			Debug.Log("Intermediate");

		}else if (modeler.GetPlayerType() == "hardcore"){
			HardModeAdd(velo,armas,evazao);
			Debug.Log("Hardcore");
		}
	}
}
