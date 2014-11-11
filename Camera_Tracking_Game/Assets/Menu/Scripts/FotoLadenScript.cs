using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class FotoLadenScript : MonoBehaviour {
	IEnumerator Start() {
		WWW www = new WWW("file:///MobileGame/photos/" + "photo" + "0" + ".png");
		yield return www;
		renderer.material.mainTexture = www.texture;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
