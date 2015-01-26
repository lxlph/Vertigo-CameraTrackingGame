using UnityEngine;
using System.Collections;

public class LoadCameraPlane : MonoBehaviour {
	public GameObject cameraPlane;
	// Use this for initialization
	void Start () {
		Instantiate (cameraPlane);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
