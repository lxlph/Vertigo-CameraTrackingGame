using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float angle;
	public float speed;
	public float mspeed;
	private bool changeEnabled = true;
	private float rotation = 1;
	public int playerHealth = 3;
	public bool vulnerable = true;
	public int fingerCount;
	
	void Start () {
		playerHealth = 3;
	}
	
	void FixedUpdate()
	{
		//float moveHorizontal = Input.GetAxis ("Horizontal");
		//float moveVertical = Input.GetAxis ("Vertical");

		Vector3 dir = Vector3.zero;
		dir.x = Input.acceleration.x;
		dir.z = Input.acceleration.y;

		if (dir.sqrMagnitude > 1) {
			dir.Normalize ();
		}

		//Vector3 movement = new Vector3 (dir.x, 0.0f, dir.z);
		Vector3 movement = new Vector3 (dir.x, 0.0f, dir.z);
		rigidbody.velocity = movement * mspeed;


		if (rotation == 1) 
		{
			angle += +60f * Time.deltaTime;
			transform.eulerAngles = new Vector3 (0, angle, 0);
		}
		else 
		{
			angle += -60f * Time.deltaTime;
			transform.eulerAngles = new Vector3 (0, angle, 0);
		}


		if (changeEnabled && Input.GetKeyDown(KeyCode.Space)) {
		
			transform.eulerAngles = new Vector3 (0, angle, 0);
			rotation *= -1;
			changeEnabled = false;
			StartCoroutine(ReEnable(3.0F));
		}

		//nicht geil gemacht, aber funktioniert
		foreach (Touch touch in Input.touches) {
			fingerCount++;
		}
		
		//drehrichtung ändern
		if (changeEnabled && fingerCount >= 1) {
			rotation *= -1;
			changeEnabled = false;
			StartCoroutine(ReEnable(5.0F));
		}

	}

	void OnCollisionEnter(Collision other) {
		
		if (vulnerable) {
			//playsound
			//	Debug.Log ("Autsch!");
			playerHealth -=1;
			vulnerable = false;
			StartCoroutine(Invulnerable(0.5F));
			
		}
		if (playerHealth <= 0){
			Application.LoadLevel("Menu");
		}
	}

	IEnumerator Invulnerable(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		vulnerable = true;
	}

	IEnumerator ReEnable(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		changeEnabled = true;
		fingerCount = 0;
	}

}
