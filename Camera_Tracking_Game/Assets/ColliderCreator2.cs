using UnityEngine;
using System.Collections;

public class ColliderCreator2 : MonoBehaviour {


	public GameObject colliderDummy;
	public Texture2D levelbild = null;
	public int instantiatecounter;
	public int instantiatecounter2;
	public int photoNumber;

	
	// Use this for initialization
	void Start () {
		Debug.Log ("collider");
		photoNumber = CameraController.photoNumber;
		LoadPhoto(photoNumber);
		createCollider (levelbild);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LoadPhoto(int photoNumber){
		WWW www = new WWW("file://" + "/storage/sdcard0/MobileGame/photos/" + "photo" + photoNumber.ToString() + ".png");
		//yield return new WWW;

		levelbild = www.texture;
	}

	void createCollider(Texture2D levelbild) {
				float faktor = (0.4f * (500.0f/levelbild.height)); //faktor durch probieren -.-'
				int randCounter = 0;
				bool[,] levelarray = GameObject.Find ("Level").GetComponent<LevelCreator> ().findEdges (levelbild); //enthält ein boolean-array, das für die collidererstellung verwendet wird
	

		for (int b = 0; b < levelbild.height; b++) {

			randCounter++;

			
			for (int a = 0; a < levelbild.width; a++) {

				if ((b == 0 || b == levelbild.height - 1 || a == 0 || a == levelbild.width - 1) && randCounter % 8 == 0) { 
					Instantiate (colliderDummy, new Vector3 ((a - (levelbild.width / 2)) * (faktor), ((b - (levelbild.height / 2)) * faktor ), 0), Quaternion.identity);
				
					randCounter++;

				} else {

					randCounter++;

				}

				if (levelarray[a, b] == true) {

					if (instantiatecounter % 4 == 0) {

						Instantiate (colliderDummy, new Vector3 ((a - (levelbild.width / 2)) * (faktor), ((b - (levelbild.height / 2)) * faktor ), 0), Quaternion.identity);

					}

					instantiatecounter++;

				}


			}
		}
	}
}
	