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
	public AudioClip getHit;
	public bool gameLost;
	public GameObject normalModeWinGUI;
	public AudioClip sadTrombone;


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
		if (!gameLost && !GameObject.Find("WinnerCubeNormalMode").GetComponent<WinnerScriptNormalMode>().instantiated) {
						Vector3 movement = new Vector3 (dir.x, 0.0f, dir.z);
						rigidbody.velocity = movement * mspeed;
		} else {
			rigidbody.velocity = new Vector3 (0, 0, 0);

		}

		if (rotation == 1) {
			if (!gameLost && !GameObject.Find("WinnerCubeNormalMode").GetComponent<WinnerScriptNormalMode>().instantiated) {

						angle += +40f * Time.deltaTime;
						transform.eulerAngles = new Vector3 (0, angle, 0);
			}
				} else {
			if (!gameLost && !GameObject.Find("WinnerCubeNormalMode").GetComponent<WinnerScriptNormalMode>().instantiated) {
								angle += -40f * Time.deltaTime;
								transform.eulerAngles = new Vector3 (0, angle, 0);
		
						}
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

	void OnCollisionStay(Collision other) {
		
		if (vulnerable && other.gameObject.name != "WinnerCubeNormalMode") {
			//playsound
			//	Debug.Log ("Autsch!");


			playerHealth -=1;
			vulnerable = false;
			audio.PlayOneShot(getHit);
			angle = angle - (30 * rotation);

			StartCoroutine(Invulnerable(0.5F));
			
		}
		if (playerHealth <= 0 && gameLost == false){

			gameLost = true;
			audio.PlayOneShot(sadTrombone);
			Instantiate(normalModeWinGUI);

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
