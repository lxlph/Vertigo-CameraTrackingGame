using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int playerHealth = 3;
	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	
	public float speed = 10.0F;
	void Update() {
		Vector3 dir = Vector3.zero;
		dir.y = Input.acceleration.y;
		dir.x = Input.acceleration.x;
		dir.z = 0;

		if (dir.sqrMagnitude > 1)
			dir.Normalize();

		
		dir *= Time.deltaTime;
		transform.Translate(dir * speed);
	}

	void OnCollisionEnter(Collision other) {
		playerHealth -=1;
		if (playerHealth <= 0){
			DestroyObject(gameObject);
			Application.LoadLevel("Menu");
		}
	}
}