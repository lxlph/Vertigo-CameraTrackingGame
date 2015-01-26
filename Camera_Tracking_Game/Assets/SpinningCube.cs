using UnityEngine;
using System.Collections;

public class SpinningCube : MonoBehaviour
{
	public float speed = 20f;
	
	
	void Update ()
	{
		transform.Rotate(Vector3.down, speed * Time.deltaTime);
	}
}