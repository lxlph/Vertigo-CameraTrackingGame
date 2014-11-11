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
			if (GUI.Button(new Rect(10, 5, 120, 52), "Foto machen")){
				StartCoroutine(TakePhoto());
			}
			if (GUI.Button(new Rect(130, 5, 120, 52), "Foto laden")){
				beimFotoLaden = true;
	        }
		}

		/**Soll das Foto gespeichert werden?**/
		if (fotoGeschossen){
			GUI.Button(new Rect(10, 5, 120, 52), "Foto speichern?");
			if(GUI.Button (new Rect(130, 5, 120, 52), "Ja")){
				beimFotoSpeichern = true;
				fotoGeschossen = false;
				print ("Ja");
			}
			else if (GUI.Button (new Rect(250, 5, 120, 52), "Nein")){
				cameraMode();
			}
		}

		/**Aussuchen, welches Foto man lädt**/
		if(beimFotoLaden){

			for (int i = 0; i < 4; i++){
				if (GUI.Button (new Rect(10 + 100*(i), 5, 100, 52), i.ToString())){
			    	photoNumber = i;
			     }
			}
			if (GUI.Button (new Rect(410, 5, 100, 52), "Zurueck")){
				cameraMode ();
            }
			GUI.Button(new Rect(10 + 100*(photoNumber), 57, 100, 5), "");
            StartCoroutine(LoadPhoto(photoNumber));
		}

		/**Speicherplatz für geschossenes Foto auswählen**/
		if(beimFotoSpeichern){
			for (int i = 0; i < 4; i++){
				if (GUI.Button (new Rect(10 + 100*(i), 5, 100, 52), i.ToString())){
					photoNumber = i;
					fotoSpeicherNummerAusgewaehlt = true;
                }
            }
			if (GUI.Button (new Rect(410, 5, 100, 52), "Zurueck")){
				cameraMode ();
			}
			GUI.Button(new Rect(10 + 100*(photoNumber), 57, 100, 5), "");
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
		Texture2D snap = new Texture2D(wct.width, wct.height);
		snap.SetPixels(wct.GetPixels());
		snap.Apply();
		System.IO.File.WriteAllBytes(
			speicherOrt + "photo" + photoNumber.ToString() + ".png",
			snap.EncodeToPNG()
            );
		print ("Gespeichert als photo" + photoNumber.ToString() + ".png");
		indexPhoto++; 
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