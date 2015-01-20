using UnityEngine;
using System.Collections;

public class guiButtonScript : MonoBehaviour {

	public bool gameLost;
	public Texture lose;
	public Texture win;
	public Texture retry;
	public Texture menu;



	// Use this for initialization
	void Start () {
		gameLost = GameObject.Find ("Rotor_Prefab").GetComponent<Kapselmoverscript> ().gameLost;
	}
	
	// Update is called once per frame
	void OnGUI() {
		GUI.backgroundColor = Color.clear;


		/*
		GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
		GUIStyle myLabelStyle = new GUIStyle(GUI.skin.button);

		
		myButtonStyle.fontSize = 50;
		myButtonStyle.normal.textColor = Color.blue;
		myButtonStyle.hover.textColor = Color.red;

		myLabelStyle.fontSize = 80;
		myLabelStyle.normal.textColor = Color.red;
		myLabelStyle.hover.textColor = Color.red;
		myLabelStyle.border.Remove;
*/
		if (gameLost) {
			GUI.Label(new Rect((Screen.width / 2) - (lose.width/2), (Screen.height / 8), lose.width, lose.height), lose);
		}

		else if (!gameLost) {
			GUI.Label(new Rect((Screen.width / 2)- (win.width/2), (Screen.height / 8), win.width, win.height), win);
		}


		if (GUI.Button (new Rect ((Screen.width / 2) - (menu.width + 30), (Screen.height / 1.6f), menu.width, menu.height), menu)) {
			Application.LoadLevel("Menu");

		}

		if (GUI.Button (new Rect ((Screen.width / 2) + 30, (Screen.height / 1.6f), retry.width, retry.height), retry)) {

			GameObject.Find("Level").GetComponent<LevelCreator>().setPlayerAndTargetPosition ();
			GameObject.Find ("Rotor_Prefab").GetComponent<Kapselmoverscript>().playerHP = 3;
			GameObject.Find("Rotor_Prefab").GetComponent<Kapselmoverscript>().gameLost = false;
			GameObject.Find("WinCube").GetComponent<Winner>().instantiated = false;
			GameObject.Find ("Rotor_Prefab").GetComponent<Kapselmoverscript>().vulnerable = true;

			Destroy (this);

		//	Application.LoadLevel("LevelCreatorScene(CopyLater)");

		}
	}
}
