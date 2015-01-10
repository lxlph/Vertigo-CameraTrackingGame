using UnityEngine;
using System.Collections;

public class guiButtonScript : MonoBehaviour {

	public bool gameLost;


	// Use this for initialization
	void Start () {
		gameLost = GameObject.Find ("Rotor_Prefab").GetComponent<Kapselmoverscript> ().gameLost;
	}
	
	// Update is called once per frame
	void OnGUI() {



		GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
		GUIStyle myLabelStyle = new GUIStyle(GUI.skin.button);

		
		myButtonStyle.fontSize = 50;
		myButtonStyle.normal.textColor = Color.blue;
		myButtonStyle.hover.textColor = Color.red;

		myLabelStyle.fontSize = 80;
		myLabelStyle.normal.textColor = Color.red;
		myLabelStyle.hover.textColor = Color.red;
	//	myLabelStyle.border.Remove;

		if (gameLost) {
			GUI.Label(new Rect((Screen.width / 4), (Screen.height / 8), 700, 200), "YOU LOSE!", myLabelStyle);
		}

		else if (!gameLost) {
			GUI.Label(new Rect((Screen.width / 4), (Screen.height / 8), 700, 200), "YOU WON!", myLabelStyle);
		}


		if (GUI.Button (new Rect ((Screen.width / 4), (Screen.height / 1.6f), 300, 200), "Menu", myButtonStyle)) {
			Application.LoadLevel("Menu");

		}

		if (GUI.Button (new Rect ((Screen.width / 4) + 400, (Screen.height / 1.6f), 300, 200), "Restart", myButtonStyle)) {
			Application.LoadLevel("LevelCreatorScene(CopyLater)");
		}
	}
}
