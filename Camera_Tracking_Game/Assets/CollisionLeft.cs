using UnityEngine;
using System.Collections;

public class CollisionLeft : MonoBehaviour {



	void OnCollisionEnter2D (Collision2D other) {
		Debug.Log ("Ich beruehre etwas");
		//transform.rotation = Quaternion.eulerAngles(new Vector3 (;

		if (GameObject.Find ("Rotor_Prefab").GetComponent<Kapselmoverscript> ().drehrichtung == 1) {
					
			GameObject.Find ("Rotor_Prefab").transform.Rotate (0, 0, 40 * (-1) * (GameObject.Find ("Rotor_Prefab").GetComponent<Kapselmoverscript> ().drehrichtung));
		
		} else if (GameObject.Find ("Rotor_Prefab").GetComponent<Kapselmoverscript> ().drehrichtung == -1) {
				
			GameObject.Find ("Rotor_Prefab").transform.Rotate (0, 0, 40 * (1) * (GameObject.Find ("Rotor_Prefab").GetComponent<Kapselmoverscript> ().drehrichtung));

		}
	}


}
