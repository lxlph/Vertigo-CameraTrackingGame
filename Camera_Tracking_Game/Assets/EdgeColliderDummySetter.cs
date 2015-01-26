using UnityEngine;
using System.Collections;

public class EdgeColliderDummySetter : MonoBehaviour {


	// Use this for initialization
	void Start () {
		gameObject.name = GameObject.Find("LevelCollider").GetComponent<ColliderCreator>().colliderCounter.ToString ();
	}
	
	public void setCollider(Vector2[] uebergebenePunkte) {
		gameObject.GetComponent<EdgeCollider2D>().points = uebergebenePunkte;

	}


}
