﻿using UnityEngine;
using System.Collections;

public class Taster : MonoBehaviour {

	public bool activated = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.name != "Rotor_Prefab")
			activated = true;
	}

	void OnTriggerStay2D (Collider2D other) {
		if (other.gameObject.name != "Rotor_Prefab")
			activated = true;
	}

	void OnTriggerExit2D (Collider2D other) {
			activated = false;
	}
}
