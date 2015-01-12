using UnityEngine;
using System.Collections;
using System.IO;
using System;
public class CameraController : MonoBehaviour {
	public string deviceName;
	WebCamTexture wct;
	public Texture2D snap;
	public GUISkin myGuiBlank;
	int indexPhoto = 0;
	public static int photoNumber = 0;
	bool fotoGeschossen = false;
	bool beimFotoLaden = false;
	bool beimFotoSpeichern = false;
	bool fotoSpeicherNummerAusgewaehlt = false;
	public GUIStyle guiAnweisungen;
	public Texture TakeP = null;
	public Texture LoadP = null;
	public Texture Play = null;
	public Texture Back = null;
	public Texture Ja = null;
	public Texture Nein = null;
	public Texture[] phases;
	public Texture ph0 = null;
	public Texture ph1 = null;
	public Texture ph2 = null;
	public Texture ph3 = null;
	
	bool actionPerformed = false;
	//bool front_facing = false;
	//int t = 0;
	private String speicherOrt = "/storage/sdcard0/MobileGame/photos/";
	void Start(){
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		WebCamDevice[] devices = WebCamTexture.devices;
		deviceName = devices[0].name;
		/* //wenn man die Frontkamera benutzen will
while(front_facing && t < devices.Length){
deviceName = devices[t].name;
front_facing = devices[t].isFrontFacing;
t++;
}
*/
		wct = new WebCamTexture(deviceName);
		System.IO.Directory.CreateDirectory(speicherOrt);
		cameraMode ();
}
	void OnGUI(){
	GUI.backgroundColor = Color.clear;
	/**Kameramodus**/
	GUI.skin = myGuiBlank;
	if (!fotoGeschossen && !beimFotoLaden && !beimFotoSpeichern){
		cameraMode ();
		//Time.timeScale = 0;
		if (GUI.Button(new Rect(Screen.width / 150, Screen.height / 13, TakeP.width / 2, TakeP.height / 2), TakeP)){
			//Time.timeScale = 0;
			StartCoroutine(TakePhoto());
		}
		if (GUI.Button(new Rect(Screen.width / 6, Screen.height / 13, LoadP.width / 2, LoadP.height / 2), LoadP)){
			//Time.timeScale = 0;
			beimFotoLaden = true;
		}
			/*
		if (GUI.Button(new Rect(Screen.width / 3, Screen.height / 13, Play.width / 2, Play.height / 2), Play)){
			//Time.timeScale = 1;
			Destroy(wct);
			Application.LoadLevel("LevelCreatorScene(CopyLater)");
		}
		*/
		if (GUI.Button (new Rect(Screen.width / 2, Screen.height / 13, Back.width / 2, Back.height / 2), Back)){
			//Time.timeScale = 0;
			//wct.Stop();
			//Destroy(wct);
			Application.LoadLevel("Menu");
		}
	}
	/**Soll das Foto gespeichert werden?**/
	if (fotoGeschossen){
		GUILayout.Label("Foto speichern?", guiAnweisungen);
		Time.timeScale = 0;
		if (GUI.Button (new Rect(Screen.width / 150, Screen.height / 13, Ja.width / 2, Ja.height / 2), Ja)){
			beimFotoSpeichern = true;
			fotoGeschossen = false;
			print ("Ja");
		}
		else if (GUI.Button (new Rect(Screen.width / 6, Screen.height / 13, Nein.width / 2, Nein.height / 2), Nein)){
			cameraMode();
		}
		Time.timeScale = 1;
	}
	/**Aussuchen, welches Foto man lädt**/
	if(beimFotoLaden){
		GUILayout.Label("Foto auswählen.", guiAnweisungen);
		//Time.timeScale = 0;
		wct.Stop ();
		if (GUI.Button (new Rect(10, 45, 200, 66), ph0)){
			photoNumber = 0;
			LoadPhoto(photoNumber);
			//Application.LoadLevel("LevelCreatorScene(CopyLater)");
		}
		if (GUI.Button (new Rect(215, 45, 200, 66), ph1)){
			photoNumber = 1;
			LoadPhoto(photoNumber);
			//Destroy(wct);
			//Application.LoadLevel("LevelCreatorScene(CopyLater)");
		}
		if (GUI.Button (new Rect(420, 45, 200, 66), ph2)){	
			photoNumber = 2;
			LoadPhoto(photoNumber);
			//Destroy(wct);
			//Application.LoadLevel("LevelCreatorScene(CopyLater)");
		}
		if (GUI.Button (new Rect(625, 45, 200, 66), ph3)){
			photoNumber = 3;
			LoadPhoto(photoNumber);
			//Destroy(wct);
			//Application.LoadLevel("LevelCreatorScene(CopyLater)");
		}

		if (GUI.Button(new Rect(830, 45, Play.width / 2, Play.height / 2), Play)){
			//Time.timeScale = 1;
			Application.LoadLevel("LevelCreatorScene(CopyLater)");
		}


		if (GUI.Button (new Rect(1035, 45, 200, 66), Back)){
			cameraMode ();
		}
		GUI.Button(new Rect(15 , 85 + 50*(photoNumber), 120, 5), "");
		StartCoroutine(LoadPhoto(photoNumber));

	}
	/**Speicherplatz für geschossenes Foto auswählen**/
	if(beimFotoSpeichern){
		GUILayout.Label("Speicherplatz für Foto auswählen.", guiAnweisungen);
		//Time.timeScale = 0;
			if (GUI.Button (new Rect(10, 45, 200, 66), ph0)){
				photoNumber = 0;
				fotoSpeicherNummerAusgewaehlt = true;
			}
			if (GUI.Button (new Rect(215, 45, 200, 66), ph1)){
				photoNumber = 1;
				fotoSpeicherNummerAusgewaehlt = true;
			}
			if (GUI.Button (new Rect(420, 45, 200, 66), ph2)){	
				photoNumber = 2;
				fotoSpeicherNummerAusgewaehlt = true;
			}
			if (GUI.Button (new Rect(625, 45, 200, 66), ph3)){
				photoNumber = 3;
				fotoSpeicherNummerAusgewaehlt = true;
			}



		if (GUI.Button (new Rect(490, 45, 200, 66), Back)){
            cameraMode ();
        }
        GUI.Button(new Rect(5 , 85 + 50*(photoNumber), 120, 5), "");
        if (fotoSpeicherNummerAusgewaehlt){
            savePhoto(photoNumber);
        }
    }
}



IEnumerator TakePhoto(){
    Texture2D snap = new Texture2D(wct.width, wct.height);
		snap.SetPixels(wct.GetPixels());
		snap.Apply();
		System.IO.File.WriteAllBytes(
			speicherOrt + "temp"+ ".png",
			snap.EncodeToPNG()
			);
		WWW www = new WWW("file://" + speicherOrt + "temp" + ".png");
		yield return www;
		if (renderer.material.mainTexture != null){
			renderer.material.mainTexture = null;
		}
		renderer.material.mainTexture = www.texture;
		fotoGeschossen = true;
	}
	IEnumerator LoadPhoto(int photoNumber){
		WWW www = new WWW("file://" + speicherOrt + "photo" + photoNumber.ToString() + ".png");
		yield return www;
		if (renderer.material.mainTexture != null){
			renderer.material.mainTexture = null;
		}
		renderer.material.mainTexture = www.texture;
		print ("Laedt" + photoNumber.ToString() + ".png");
		//wct.Stop();

	}
	void savePhoto(int photoNumber){
		WWW www = new WWW("file://" + speicherOrt + "temp" + ".png");
		Texture2D snap = www.texture;
		System.IO.File.WriteAllBytes(
			speicherOrt + "photo" + photoNumber.ToString() + ".png",
            snap.EncodeToPNG()
            );
        print ("Gespeichert als photo" + photoNumber.ToString() + ".png");
        cameraMode ();
    }

	void cameraMode(){

		fotoGeschossen = false;
		beimFotoLaden = false;
		beimFotoSpeichern = false;
		fotoSpeicherNummerAusgewaehlt = false;
		if (renderer.material.mainTexture != null){
			renderer.material.mainTexture = null;
		}

		renderer.material.mainTexture = wct;
        wct.Play();

}
}