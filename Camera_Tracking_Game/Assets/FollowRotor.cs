using UnityEngine;
using System.Collections;

public class FollowRotor : MonoBehaviour {


	private Transform rotor;		// Reference to the player.
	public Vector3 offset;			// The offset at which the Health Bar follows the player.
	public int testZ;

	// Use this for initialization
	void Start () {


		rotor = GameObject.FindGameObjectWithTag("Rotor").transform;
		offset = new Vector3 (0, 0, -100.0f + testZ);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = (rotor.position + offset);

	}
}
