using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int playerHealth = 3;
	// Use this for initialization
	void Start () {
	
	}
	
	public float speed = 10.0F;
	void Update() {
		Vector2 dir = Vector2.zero;
		dir.y = Input.acceleration.y;
		dir.x = Input.acceleration.x;
		/*
		if (dir.sqrMagnitude > 1)
			dir.Normalize();
		*/
		
		dir *= Time.deltaTime;
		transform.Translate(dir * speed);
	}

	void OnCollisionEnter2D(Collision2D other) {
		playerHealth -=1;
		if (playerHealth <= 0){
			DestroyObject(gameObject);
			Application.LoadLevel("Menu");
		}
	}
}