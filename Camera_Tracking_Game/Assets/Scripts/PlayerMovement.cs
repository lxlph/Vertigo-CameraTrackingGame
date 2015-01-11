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

	
	void Start () {
		
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
		Vector3 movement = new Vector3 (dir.x, dir.z, 0.0f);
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
	}
		IEnumerator ReEnable(float waitTime) {
			yield return new WaitForSeconds(waitTime);
			changeEnabled = true;
	
			
		}

	void OnCollisionEnter(Collision other) {
		playerHealth -=1;
		if (playerHealth <= 0){
			DestroyObject(gameObject);
			Application.LoadLevel("Menu");
		}
	}

}
