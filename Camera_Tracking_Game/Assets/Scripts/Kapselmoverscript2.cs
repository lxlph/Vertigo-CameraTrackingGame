using UnityEngine;
using System.Collections;

public class Kapselmoverscript2 : MonoBehaviour {

	private int drehrichtung = 1;
	public float drehSpeed = 0.6f;
	public float moveSpeed = 10.0f;
	private bool changeEnabled = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate (0, 0, drehSpeed * drehrichtung);

		//if-abfrage, ob sich y geändert hat, sonst wieder auf wert setzen



		//bewegungskram

		/*
		Vector2 dir = Vector2.zero;
		dir.y = Input.acceleration.y;
		dir.y = Input.acceleration.x; ?!?!?!?!?!?!
		*/

		Vector3 dir = Vector3.zero;
		dir.x = Input.GetAxis ("Horizontal");
		dir.y = Input.GetAxis ("Vertical");


		/*
		if (dir.sqrMagnitude > 1)
			dir.Normalize();
		*/
		
		dir *= Time.deltaTime;
		transform.Translate(dir * moveSpeed, Space.World);

		//drehrichtung ändern
		if (changeEnabled && Input.GetKeyDown(KeyCode.Space)) {
			drehrichtung *= -1;
			changeEnabled = false;
			StartCoroutine(ReEnable(5.0F));
		}

	}

	IEnumerator ReEnable(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		changeEnabled = true;
	}
}
