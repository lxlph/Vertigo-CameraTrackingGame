using UnityEngine;
using System.Collections;

public class Kapselmoverscript : MonoBehaviour {

	public int drehrichtung = 1;
	public float drehSpeed = 0.6f;
	public float moveSpeed = 80.0f;
	private bool changeEnabled = true;
	private int fingerCount = 0;
	private bool vulnerable = true;
	public int playerHP = 3;


	public GameObject guiElementPrefab;

	public bool gameLost;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (playerHP <= 0 && !gameLost && !(GameObject.Find("WinCube").GetComponent<Winner>().instantiated)) {

			Instantiate(guiElementPrefab);
			gameLost = true;

		}

		if (!gameLost && !(GameObject.Find("WinCube").GetComponent<Winner>().instantiated)) {
			transform.Rotate (0, 0, drehSpeed * drehrichtung);
		}
		//if-abfrage, ob sich y geändert hat, sonst wieder auf wert setzen



		//bewegungskram

		//enable für mobile


		Vector2 dir = Vector2.zero;
		dir.y = Input.acceleration.y;
		dir.x = Input.acceleration.x;



		/*
	 	Vector3 dir = Vector3.zero;
		dir.x = Input.GetAxis ("Horizontal");
		dir.y = Input.GetAxis ("Vertical");
		*/


		if (dir.sqrMagnitude > 1) {
			dir.Normalize ();
		}


		//dir *= Time.deltaTime;
		//transform.Translate(dir * moveSpeed, Space.World);

		if (!gameLost && !(GameObject.Find("WinCube").GetComponent<Winner>().instantiated)) {
			rigidbody2D.velocity = (dir * moveSpeed);
		}

		if (gameLost || (GameObject.Find("WinCube").GetComponent<Winner>().instantiated)) {
			rigidbody2D.velocity = new Vector2 (0, 0);
		}


		//nicht geil gemacht, aber funktioniert
		foreach (Touch touch in Input.touches) {
				fingerCount++;
		}

		//drehrichtung ändern
		if (changeEnabled && fingerCount >= 1) {
			drehrichtung *= -1;
			changeEnabled = false;
			StartCoroutine(ReEnable(5.0F));
		}


	
	}

	void OnCollisionEnter2D (Collision2D other) {
	

		if (vulnerable) {
			//playsound

			playerHP--;
		//	Debug.Log ("Autsch!");
			vulnerable = false;
			StartCoroutine(Invulnerable(0.5F));

		}

		if (GameObject.Find ("BumperA1").GetComponent<Taster> ().activated || GameObject.Find ("BumperB1").GetComponent<Taster> ().activated) {

			if (drehrichtung == 1) {
				transform.Rotate (0, 0, drehSpeed * 50.0f * drehrichtung);
			}

			else if (drehrichtung == -1) {
				transform.Rotate (0, 0, (-1)* drehSpeed * 50.0f * drehrichtung);
			}

		} else if (GameObject.Find ("BumperA2").GetComponent<Taster> ().activated || GameObject.Find ("BumperB2").GetComponent<Taster> ().activated) {

			if (drehrichtung == 1) {
				transform.Rotate (0, 0, drehSpeed * 50.0f * (-1) * drehrichtung);
			}
			
			else if (drehrichtung == -1) {
				transform.Rotate (0, 0, drehSpeed * 50.0f * drehrichtung);
			}
		}
	}

	IEnumerator ReEnable(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		changeEnabled = true;
		fingerCount = 0;
	}

	IEnumerator Invulnerable(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		vulnerable = true;
	}
}
