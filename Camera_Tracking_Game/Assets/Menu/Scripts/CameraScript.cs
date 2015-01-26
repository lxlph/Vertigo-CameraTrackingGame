using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	void OnMouseDown(){
		Debug.Log ("Kamera wird geladen");
		Application.LoadLevel("Camera");
	}

}
/*
public class ExampleClass : MonoBehaviour {
	void Start() {
		WebCamDevice[] devices = WebCamTexture.devices;
		int i = 0;
		while (i < devices.length) {
			Debug.Log(devices[i].name);
			i++;
		}
	}
}*/