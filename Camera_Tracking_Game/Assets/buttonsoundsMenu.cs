using UnityEngine;
using System.Collections;

public class buttonsoundsMenu : MonoBehaviour {

	public AudioClip vertigo;
	public AudioClip normalMode;
	public AudioClip cameraMode;
	public AudioClip buttonSound;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void playNormalMode () {
		audio.PlayOneShot (normalMode);

	}

	public void playCameraMode () {
		audio.PlayOneShot (cameraMode);
		
	}

	public void vertigoSound () {
		audio.PlayOneShot (vertigo);
		
	}

	public void playButtonSound () {
		audio.PlayOneShot (buttonSound);
		
	}
}
