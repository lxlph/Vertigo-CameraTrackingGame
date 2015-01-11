using UnityEngine;
using System.Collections;

public class LoadMenu : MonoBehaviour {
	private bool mainMenu = true;
	void OnGUI(){
		if (mainMenu){
			if (GUI.Button(new Rect(280, 130, 200, 200), "Kameramodus")){
				Debug.Log ("Kamera geladen");
				mainMenu = false;
				Application.LoadLevel("Camera");
			}
			if (GUI.Button(new Rect(280, 385, 200, 200), "Normales Spiel")){
				Debug.Log ("Spiel geladen");
				mainMenu = false;
			}
		}
		else{
			if (GUI.Button(new Rect(280, 130, 200, 200), "Level 1")){
				Application.LoadLevel("Level1");
			}
			if (GUI.Button(new Rect(280, 330, 200, 200), "Level 2")){
				Application.LoadLevel("Level2");
			}
			if (GUI.Button(new Rect(280, 530, 200, 200), "Level 3")){
				Application.LoadLevel("Level3");
			}
			if (GUI.Button(new Rect(280, 730, 200, 200), "Zurück")){
				Application.LoadLevel("EmptyScene");
			}
		}
	}
}
