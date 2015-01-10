using UnityEngine;
using System.Collections;

public class Winner : MonoBehaviour {

	public GameObject guiElementPrefab;
	public bool instantiated;

	void OnTriggerEnter2D (Collider2D other) {

		if (other.gameObject.name == "Rotor_Prefab" && !instantiated) {
			Instantiate(guiElementPrefab);
			instantiated = true;
		}

	}
}
