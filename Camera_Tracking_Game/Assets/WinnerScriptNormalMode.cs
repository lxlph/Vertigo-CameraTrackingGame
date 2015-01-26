using UnityEngine;
using System.Collections;

public class WinnerScriptNormalMode : MonoBehaviour {

	// Use this for initialization
	public GameObject normalModeWinGUI;
	public bool instantiated;
	public AudioClip winSound;


	void OnTriggerEnter (Collider other) {
		Debug.Log ("asdf");
		if (other.gameObject.name == "Player" && !instantiated) {
			audio.PlayOneShot(winSound);
			Instantiate(normalModeWinGUI);
			instantiated = true;
		}
	}
}
