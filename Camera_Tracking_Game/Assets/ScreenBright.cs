using UnityEngine;
using System.Collections;

public class ScreenBright : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame

}
