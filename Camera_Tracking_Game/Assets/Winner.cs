using UnityEngine;
using System.Collections;

public class Winner : MonoBehaviour {

	public AudioClip winSound;

	public GameObject guiElementPrefab;
	public bool instantiated;
	

	void OnTriggerEnter2D (Collider2D other) {

		if (other.gameObject.name == "Rotor_Prefab" && !instantiated) {
			audio.PlayOneShot(winSound);
			Instantiate(guiElementPrefab);
			instantiated = true;
		}

	}
}
