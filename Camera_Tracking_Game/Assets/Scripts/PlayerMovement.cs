using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float angle;
	public float speed;
	public float mspeed;
	
	void Start () {
		
	}
	
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement * mspeed;

		angle += 50f * Time.deltaTime;
		transform.eulerAngles = new Vector3 (0, angle, 0);

	 }
}


