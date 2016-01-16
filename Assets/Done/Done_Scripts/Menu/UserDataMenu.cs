using UnityEngine;
using System.Collections;

public class UserDataMenu : MonoBehaviour {

	private string playerName;
	
	private string mode;

	void Awake () {
		DontDestroyOnLoad(this);
	}

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	public void DestroyUserDataMenu(){
		DestroyImmediate(this);
	}

	public void SetName(string name){
		this.playerName = name;
	}
	public void SetMode(string mode){
		this.mode = mode;
	}
	public string GetName() {
		return this.playerName;
	}
	public string GetMode(){
		return this.mode;
	}
}
