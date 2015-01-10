using UnityEngine;
using System.Collections;

public class LoadMenu : MonoBehaviour {
	private bool mainMenu = true;
	void OnGUI(){
		if (mainMenu){
			if (GUI.Button(new Rect(280, 130, 120, 45), "Kameramodus")){
				Debug.Log ("Kamera geladen");
				mainMenu = false;
				Application.LoadLevel("Camera");
			}
			if (GUI.Button(new Rect(280, 180, 120, 45), "Normales Spiel")){
				Debug.Log ("Spiel geladen");
				mainMenu = false;
			}
		}
		else{
			if (GUI.Button(new Rect(280, 130, 120, 45), "Level 1")){
				Application.LoadLevel("Level1");
			}
			if (GUI.Button(new Rect(280, 180, 120, 45), "Level 2")){
				Application.LoadLevel("Level2");
			}
			if (GUI.Button(new Rect(280, 230, 120, 45), "Level 3")){
				Application.LoadLevel("Level3");
			}
		}
	}
}
