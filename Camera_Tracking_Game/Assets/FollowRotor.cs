using UnityEngine;
using System.Collections;

public class FollowRotor : MonoBehaviour {


	private Transform rotor;		// Reference to the player.
	public Vector3 offset;			// The offset at which the Health Bar follows the player.
	public int testZ;
	private float offsetX;
	private float offsetY;
	private float lastX;
	private float lastY;
	private float diffX;
	private float diffY;
	public bool playModeActive;


	// Use this for initialization
	void Start () {
		playModeActive = false;

		rotor = GameObject.FindGameObjectWithTag("Rotor").transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {

		/*
		if ((Input.acceleration.x - lastX) < 0.08f && (Input.acceleration.x - lastX) > -0.08f) {
				
			diffX = (Input.acceleration.x - lastX);

		
			} else {

			if ((Input.acceleration.x - lastX) < -0.08f) {
				diffX = -0.08f;

			}

			else if ((Input.acceleration.x - lastX) > 0.08f) {
				diffX = 0.08f;
			}
		}

		if (((Input.acceleration.y - lastY) < 0.08f && (Input.acceleration.y - lastY) > -0.08f)) {

			diffY = (Input.acceleration.y - lastY);

			} else {

			if ((Input.acceleration.y - lastY) < -0.08f) {
				diffY = -0.08f;
				
			}
			
			else if ((Input.acceleration.y - lastY) > 0.08f) {
				diffY = 0.08f;
			}

		}

		offsetX = 10.0f * (Mathf.Lerp(lastX + diffX, lastX, Time.deltaTime));
		offsetY = 10.0f * (Mathf.Lerp(lastY + diffY, lastY, Time.deltaTime));
		*/

		offset = new Vector3 (offsetX, offsetY, -100.0f + testZ);

		if (playModeActive) {
				
						transform.position = (rotor.position + offset);
		}

		else if (!playModeActive) {
			transform.position = new Vector3 (0, 0, (-(GameObject.Find("Level").GetComponent<LevelCreator>().levelbild.width))/3.0f);
			Time.timeScale = 0; //funktioniert nicht?!


		}
	//	lastX = Input.acceleration.x;
	//	lastY = Input.acceleration.y;

	}

	void OnGUI () {
		if (!playModeActive) {
			if (GUI.Button (new Rect ((Screen.width / 2) - 180, (Screen.height / 1.1f), 150, 50), "Spawns ok!")) {
								Time.timeScale = 1;
								playModeActive = true;
						}
			if (GUI.Button (new Rect (new Rect ((Screen.width / 2) + 30, (Screen.height / 1.1f), 150, 50)), "Spawns incorrect!")) {
								Time.timeScale = 1;
								Application.LoadLevel ("Menu");

						}
				}
		}
}
