using UnityEngine;
using System.Collections;

public class liveBar : MonoBehaviour {
	private Sprite[]  liveBarSprites = new Sprite[4];
	private int spriteIndex;
	void Awake () {
		for (int i = 1; i < liveBarSprites.Length; i++){ 
			liveBarSprites[i] = Resources.Load<Sprite>("liveBar_" + i.ToString());
		}
	}

	void Update () {
		spriteIndex = GameObject.Find ("Player").GetComponent<PlayerController> ().playerHealth;
		gameObject.GetComponent<SpriteRenderer>().sprite = liveBarSprites[spriteIndex];

		/*
		if (GameObject.Find ("Player").GetComponent<PlayerController> ().playerHealth==3){
			gameObject.GetComponent<SpriteRenderer>().sprite = liveBarSprites[3];
		}
		*/
	}
	
}
