using UnityEngine;
using System.Collections;
public class LivebarScript : MonoBehaviour {
	public Sprite[] liveBarSprites = new Sprite[4];
	public int spriteIndex;

	void Awake () {
		for (int i = 1; i < liveBarSprites.Length; i++){
			liveBarSprites[i] = Resources.Load<Sprite>("liveBar_" + i.ToString());
		}
	}
	void OnGUI () {
		//Vector2 PlayerPOS = GameObject.Find("Rotor_Prefab").transform.transform.position;
		//transform.position = new Vector2(PlayerPOS.x + 75, PlayerPOS.y + 35);
		spriteIndex = GameObject.Find ("Rotor_Prefab").GetComponent<Kapselmoverscript> ().playerHP;
		Texture2D texture = (Texture2D) Resources.Load ("liveBar_" + spriteIndex.ToString());
		GUI.Label(new Rect(1000, 80, 100, 100), texture);
	}
}
