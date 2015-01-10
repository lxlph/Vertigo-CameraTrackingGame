using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ColliderCreator : MonoBehaviour {

	public Texture2D levelbild;
	public float verhaeltnis;
	public GameObject EdgeColliderDummy;
	public int colliderCounter;


	// Use this for initialization
	void Start () {
		verhaeltnis = ((float)(levelbild.width) / (float)(levelbild.height));

				createCollider (levelbild);

	}
	
	// Update is called once per frame
	void createCollider(Texture2D levelbild) {
		bool[,] levelarray = GameObject.Find("Level").GetComponent<LevelCreator>().findEdges(levelbild); //enthält ein boolean-array, das für die collidererstellung verwendet wird
		Vector2 activePixel = new Vector2 (0,0);
		List<Vector2> globalVerticesList = new List<Vector2>();
		List<Vector2> localVerticesList = new List<Vector2>();

		/* Edge Collider verwenden!
		 * 1. Anfangen mit einem beliebigen Element (von unten links durchgehen)
		 * 2. Dieses Element als "active Pixel" deklarieren und in die pixellist packen, auch in eine lokale Pixellist packen
		 * 3. Ein Nachbarpixel suchen, das auch true ist und nicht in der globalen PIxellist, und dieses als aktives pixel benutzen
		 * 4. Pixel so lange durchsuchen, bis irgendwann kein nächstes Pixel mehr zu finden ist, das nicht in der Pixellist steht
		 * 5. Alles richtig stretchen in lokaler liste. Den Faktor für die Breite nicht vergessen (theoretisch sollte Formel von 
		 * 6. Pixellist in das Array für den Edgecollider packen und den Edgecollider constructen aus lokaler liste, dann lokale liste löschen
		 * 7. Unsystematisch weitersuchen, bis nächstes true-Pixel gefunden wurde, das nicht in Pixellist ist
		 */
		
		
		//finding the starting-pixel
		for (int b = 0; b < levelbild.height; b++) {
			
			for (int a = 0; a < levelbild.width; a++) {
				
				if (levelarray[a, b] == true && !(globalVerticesList.Contains(new Vector2(a, b)))) {


						activePixel = new Vector2 (a, b);


					while (activePixel != new Vector2(-1, -1)) {
						globalVerticesList.Add (activePixel);
						// ADJUST HERE!!!!!
						localVerticesList.Add (new Vector2 (0.35f * (activePixel.x) * verhaeltnis - ((float) levelbild.width / ((1.0f/0.35f) * verhaeltnis)), (0.35f * activePixel.y) - ((float) levelbild.height / (1.0f/0.35f))));
						/*

						if (((b - globalVerticesList[globalVerticesList.Count - 1].y) >= (-1)) && ((b - globalVerticesList[globalVerticesList.Count - 1].y) <= (1)) && ((a - globalVerticesList[globalVerticesList.Count - 1].x) <= (1)) && ((a - globalVerticesList[globalVerticesList.Count - 1].x) >= (-1))) {
							globalVerticesList.RemoveAt(globalVerticesList.Count - 1);
							localVerticesList.RemoveAt(localVerticesList.Count - 1);

						}
						*/

						//	Debug.Log (activePixel);
						
						for (int k = -1; k <= 1; k++) {
							
							for (int l = -1; l <= 1; l++) {
								
								// weil das pixel selbst ja sowieso immer true ist dann
								if (l != 0 && k != 0) {
									
									//damit für die randpixel nicht alle umgebenden pixel auch noch gecheckt werden (out of bounds)
									if (((int)activePixel.x + k) >= 0 && ((int)activePixel.x + k) < levelbild.width && ((int)activePixel.y + l) >= 0 && ((int)activePixel.y + l) < levelbild.height) {
										
										if (levelarray[((int)activePixel.x + k), ((int)activePixel.y + l)] && (!(globalVerticesList.Contains(new Vector2(((int)activePixel.x + k), ((int)activePixel.y + l)))))) {
											
											activePixel = new Vector2(((int)activePixel.x + k), ((int)activePixel.y + l));
											
											
										} else {
											activePixel = new Vector2 (-1, -1);
										}
									}
								}
								
							}
							
						}
						
						
						
					}
					//	localVerticesList.Add (firstPixel);
					
					//collider erstellen
					if (localVerticesList.Count > 10) {

						colliderCounter++;

						GameObject currentCollider = (GameObject) Instantiate(Resources.Load("EdgeColliderDummy"));

						Vector2[] uebergebenePunkte = localVerticesList.ToArray();
					

						currentCollider.GetComponent<EdgeColliderDummySetter>().setCollider(uebergebenePunkte);	
						localVerticesList.Clear ();
					}
				}
				
			}
		}
		
	}

}
