using UnityEngine;
using System.Collections;

public class LoadMenu : MonoBehaviour {
	public GUISkin myGuiBlank;
	private bool mainMenu = true;
	public Texture CameraMode = null;
	public Texture NormalMode = null;
	public Texture Level1 = null;
	public Texture Level2 = null;
	public Texture Level3 = null;
	public Texture Back = null;
	public bool notPlayedYet;

	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;

		notPlayedYet = true;
	}

	IEnumerator Wait(float waitTime) {
		yield return new WaitForSeconds(waitTime);
	}

	void OnGUI(){
		if (notPlayedYet) {
			GameObject.Find("Sound").GetComponent<buttonsoundsMenu> ().vertigoSound();
			notPlayedYet = false;
		}


		GUI.backgroundColor = Color.clear;
		if (mainMenu){
				
				GUI.skin = myGuiBlank;
			if (GUI.Button(new Rect(Screen.width / 2 -200, Screen.height / 2 +160 , NormalMode.width, NormalMode.height), CameraMode)){
				Application.LoadLevel("Camera");
			}
			if (GUI.Button(new Rect(Screen.width / 2 -200, Screen.height / 2 -10, CameraMode.width, CameraMode.height), NormalMode)){
				GameObject.Find("Sound").GetComponent<buttonsoundsMenu> ().playNormalMode();
				mainMenu = false;
			}
		}
		else{
			GUI.skin = myGuiBlank;

			if (GUI.Button(new Rect(Screen.width / 2 -200, Screen.height / 2 -75, Level1.width, Level1.height), Level1)){
			//	GameObject.Find("Sound").GetComponent<buttonsoundsMenu> ().playButtonSound();
				Wait (2);
				Application.LoadLevel("Level1");
			}
			if (GUI.Button(new Rect(Screen.width / 2 -200, Screen.height / 2 + 50, Level2.width, Level2.height), Level2)){
			//	GameObject.Find("Sound").GetComponent<buttonsoundsMenu> ().playButtonSound();
				Wait (2);
				Application.LoadLevel("Level2");
			}
			if (GUI.Button(new Rect(Screen.width / 2 -200, Screen.height / 2 +175, Level3.width, Level3.height), Level3)){
			//	GameObject.Find("Sound").GetComponent<buttonsoundsMenu> ().playButtonSound();
				Wait (2);
				Application.LoadLevel("Level3");
			}
			if (GUI.Button(new Rect((Screen.width / 2 -200) - 1.02f * Back.width, Screen.height / 2 +175, Back.width, Back.height), Back)){
				mainMenu = true;
			}
		}
	}
}
