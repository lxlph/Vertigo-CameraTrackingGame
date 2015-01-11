using UnityEngine;
using System.Collections;

public class LoadMaster : MonoBehaviour {
	public int photoIndex;
	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(this);
	}
}
