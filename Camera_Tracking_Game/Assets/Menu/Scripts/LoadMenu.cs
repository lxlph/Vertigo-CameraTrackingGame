using UnityEngine;
using System.Collections;

public class LoadMenu : MonoBehaviour {
	private bool mainMenu = true;
	void OnGUI(){
		if (mainMenu){
			if (GUI.Button(new Rect(280, 130, 120, 45), "Kameramodus")){
				Time.timeScale = 0;
				Debug.Log ("Kamera geladen");
				mainMenu = false;
				Application.LoadLevel("Camera");
			}
			if (GUI.Button(new Rect(280, 180, 120, 45), "Normales Spiel")){
				Time.timeScale = 0;
				Debug.Log ("Spiel geladen");
				mainMenu = false;
			}
		}
		else{
			if (GUI.Button(new Rect(280, 130, 120, 45), "Level 1")){
				Time.timeScale = 0;
				Application.LoadLevel("Level1");
			}
			if (GUI.Button(new Rect(280, 180, 120, 45), "Level 2")){
				Time.timeScale = 0;
				Application.LoadLevel("Level2");
			}
		}
	}
}
