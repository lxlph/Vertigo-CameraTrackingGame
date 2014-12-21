using UnityEngine;
using System.Collections;

public class BackRotation : MonoBehaviour {

	public float angle;
	
	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.name == "Wuerfel")
			angle += -180f * Time.deltaTime;
		transform.eulerAngles = new Vector3 (0, angle, 0);
	}


}
