using UnityEngine;
using System.Collections;

public class winGUI : MonoBehaviour {

	public bool gameLost;
	public Texture lose;
	public Texture win;
	public Texture retry;
	public Texture menu;
	// Use this for initialization
	void Start () {
		gameLost = GameObject.Find ("Player").GetComponent<PlayerMovement> ().gameLost;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		GUI.backgroundColor = Color.clear;

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

			GameObject.Find ("Player").GetComponent<PlayerMovement> ().gameLost = false;
			Application.LoadLevel(Application.loadedLevel);
		}



	}
}
