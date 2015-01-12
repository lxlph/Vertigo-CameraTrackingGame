using UnityEngine;
using System.Collections;
public class liveBarLC : MonoBehaviour {
	public Sprite[] liveBarSprites = new Sprite[4];
	public int spriteIndex;
	void Awake () {
		for (int i = 1; i < liveBarSprites.Length; i++){
			liveBarSprites[i] = Resources.Load<Sprite>("liveBar_" + i.ToString());
		}
	}
	void Update () {
		Vector2 PlayerPOS = GameObject.Find("Rotor_Prefab").transform.transform.position;
		transform.position = new Vector2(PlayerPOS.x + 75, PlayerPOS.y + 35);
		spriteIndex = GameObject.Find ("Rotor_Prefab").GetComponent<Kapselmoverscript> ().playerHP;
		gameObject.GetComponent<SpriteRenderer>().sprite = liveBarSprites[spriteIndex];
		/*
if (GameObject.Find ("Player").GetComponent<PlayerController> ().playerHealth==3){
gameObject.GetComponent<SpriteRenderer>().sprite = liveBarSprites[3];
}
*/
	}
}