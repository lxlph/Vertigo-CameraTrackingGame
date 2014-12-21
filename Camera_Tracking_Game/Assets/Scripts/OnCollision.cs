using UnityEngine;
using System.Collections;

public class OnCollision : MonoBehaviour {

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "Cube") {
						Destroy (col.gameObject);
		}
	}
}
