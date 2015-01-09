using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class CameraController : MonoBehaviour {
	public string deviceName;
	WebCamTexture wct;
	public Texture2D snap;
	int indexPhoto = 0;
	int photoNumber = 0;
	bool fotoGeschossen = false;
	bool beimFotoLaden = false;
	bool beimFotoSpeichern = false;
	bool fotoSpeicherNummerAusgewaehlt = false;
	public GUIStyle guiAnweisungen;

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
		wct = new WebCamTexture(deviceName, 400, 300, 12);
		cameraMode ();
		System.IO.Directory.CreateDirectory(speicherOrt);

	}

	void OnGUI() {  
		/**Kameramodus**/
		if (!fotoGeschossen && !beimFotoLaden && !beimFotoSpeichern){
			cameraMode ();
			GUILayout.Label("Wähle eine Aktion aus.", guiAnweisungen);
			Time.timeScale = 0;
			if (GUI.Button(new Rect(5, 45, 120, 45), "Foto machen")){
				Time.timeScale = 0;
				StartCoroutine(TakePhoto());
			}
			if (GUI.Button(new Rect(5, 95, 120, 45), "Foto laden")){
				Time.timeScale = 0;
				beimFotoLaden = true;
	        }
			if (GUI.Button(new Rect(5, 145, 120, 45), "Spielen")){
				Time.timeScale = 0;
				Application.LoadLevel("Menu");
			}
			if (GUI.Button (new Rect(5, 255, 120, 45), "Zurück")){
				Time.timeScale = 0;
				Application.LoadLevel("Menu");
			}
		}

		/**Soll das Foto gespeichert werden?**/
		if (fotoGeschossen){
			GUILayout.Label("Foto speichern?", guiAnweisungen);
			Time.timeScale = 0;
			if(GUI.Button (new Rect(5, 45, 120, 45), "Ja")){
				beimFotoSpeichern = true;
				fotoGeschossen = false;
				print ("Ja");
			}
			else if (GUI.Button (new Rect(5, 95, 120, 45), "Nein")){
				cameraMode();
			}
		}

		/**Aussuchen, welches Foto man lädt**/
		if(beimFotoLaden){
			GUILayout.Label("Foto auswählen.", guiAnweisungen);
			Time.timeScale = 0;
			for (int i = 0; i < 4; i++){
				if (GUI.Button (new Rect(5, 45 + 50*i, 120, 45), i.ToString())){
			    	photoNumber = i;
			     }
			}
			if (GUI.Button (new Rect(5, 255, 120, 45), "Zurück")){
				cameraMode ();
            }
			GUI.Button(new Rect(5 , 85 + 50*(photoNumber), 120, 5), "");
            StartCoroutine(LoadPhoto(photoNumber));
		}

		/**Speicherplatz für geschossenes Foto auswählen**/
		if(beimFotoSpeichern){
			GUILayout.Label("Speicherplatz für Foto auswählen.", guiAnweisungen);
			Time.timeScale = 0;
			for (int i = 0; i < 4; i++){
				if (GUI.Button (new Rect(5, 45 + 50*i, 120, 45), i.ToString())){
					photoNumber = i;
					fotoSpeicherNummerAusgewaehlt = true;
                }
            }
			if (GUI.Button (new Rect(5, 255, 120, 45), "Zurück")){
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