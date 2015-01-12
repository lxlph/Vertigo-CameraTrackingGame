using UnityEngine;
using System.Collections;
public class liveBar : MonoBehaviour {
	public Sprite[] liveBarSprites = new Sprite[4];
	public int spriteIndex;
	private float posY;
	void Awake () {
		for (int i = 1; i < liveBarSprites.Length; i++){
			liveBarSprites[i] = Resources.Load<Sprite>("liveBar_" + i.ToString());
		}
	}
	void Update () {
		posY = transform.position.y;
		Vector3 PlayerPOS = GameObject.Find("Player").transform.transform.position;
		transform.position = new Vector3(PlayerPOS.x + 11, posY, PlayerPOS.z + 7);
		spriteIndex = GameObject.Find ("Player").GetComponent<PlayerMovement> ().playerHealth;
		gameObject.GetComponent<SpriteRenderer>().sprite = liveBarSprites[spriteIndex];
		/*
if (GameObject.Find ("Player").GetComponent<PlayerController> ().playerHealth==3){
gameObject.GetComponent<SpriteRenderer>().sprite = liveBarSprites[3];
}
*/
	}
}