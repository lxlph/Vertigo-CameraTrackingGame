using UnityEngine;
using System.Collections;

public class LoadGame : MonoBehaviour {
	void OnMouseDown(){
		Debug.Log ("Spiel wird geladen");
		Application.LoadLevel("InGame");
	}
}
