using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelCreator : MonoBehaviour {


	public Texture2D levelbild;
	private Texture2D appliedPicture;
	public List<Vector3> newVertices = new List<Vector3> ();
	private Mesh mesh;
	public float verhaeltnis;
	public int photoNumber = 2;
	public WWW www;


	void Start () {

		//photoNumber = GameObject.Find ("LoadMaster").GetComponent<LoadMaster> ().photoIndex;
		photoNumber = CameraController.photoNumber;
		LoadPhoto(photoNumber);
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		mesh = GetComponent<MeshFilter> ().mesh;


		verhaeltnis = ((float)(levelbild.width) / (float)(levelbild.height));




		for (int i = 0; i < mesh.vertices.Length; i++) {
			newVertices.Add(new Vector3 (mesh.vertices[i].x * verhaeltnis, mesh.vertices[i].y, mesh.vertices[i].z)); 
		}

		mesh.vertices = newVertices.ToArray();
		mesh.Optimize ();
		mesh.RecalculateBounds (); 

		mesh.RecalculateNormals ();



		//& Visualisierung des Colliders
		appliedPicture = new Texture2D(levelbild.width, levelbild.height);
		drawLines(Color.black);


		renderer.material.mainTexture = levelbild;
		setPlayerAndTargetPosition ();
	}


	

		void drawLines(Color c)
		{
			bool[,] pictureValues = new bool[levelbild.width, levelbild.height];

			pictureValues = findEdges (levelbild);

			for (int x = 0; x < levelbild.width; x++) {
				for (int y = 0; y < levelbild.height; y++) {
					if(pictureValues[x,y]) {
						appliedPicture.SetPixel (x, y, c);
					}

				}
			}
			appliedPicture.Apply ();

		}


	void LoadPhoto(int photoNumber){
		WWW www = new WWW("file://" + "/storage/sdcard0/MobileGame/photos/" + "photo" + photoNumber.ToString() + ".png");
		//yield return new WaitForSeconds(0);

		if (renderer.material.mainTexture != null){
			renderer.material.mainTexture = null;
		}
		levelbild = www.texture; 
	}


	//boolean array erstellen
	public bool[,] findEdges(Texture2D levelbild) {

		bool[,] truePixels = new bool[levelbild.width, levelbild.height];

		//zweites array wichtig, da man sonst ein array schon verändert, während man es prüft.
		bool[,] truePixelsProcessed = new bool[levelbild.width, levelbild.height];

		//ARRAY MIT WERTEN FUELLEN
		for (int i = 0; i < levelbild.height; i++) {

			for (int j = 0; j < levelbild.width; j++) {

				//am anfang wird der wert auf true gesetzt wird, um später die if-abfrage in der schleife zu vereinfachen
				truePixels[j, i] = true;

			
				//wenn pixel selbst zu hell ist, wird es false gesetzt
				if (levelbild.GetPixel (j, i).r > 0.33f || levelbild.GetPixel (j, i).b > 0.33f || levelbild.GetPixel (j, i).g > 0.33f){
					truePixels[j, i] = false;
				}

				//umgebende pixel checken (im moment die äußeren 1 pixel oben und unten
				if(truePixels[j, i]) {
					int umgebendePixelWeiss = 0;
					int umgebendePixelRot = 0;
					int umgebendePixelGruen = 0;

					for (int k = -3; k <= 3; k++) {

						for (int l = -3; l <= 3; l++) {

							// weil das pixel selbst ja sowieso immer true ist dann
							if (l != 0 && k != 0) {

								//damit für die randpixel nicht alle umgebenden pixel auch noch gecheckt werden (out of bounds)
								if ((j + k) >= 0 && (j + k) < levelbild.width && (i + l) >= 0 && (i + l) < levelbild.height) {


									if (levelbild.GetPixel (j + k, i + l).r > 0.5f && levelbild.GetPixel (j + k, i + l).b > 0.5f && levelbild.GetPixel (j + k, i + l).g > 0.5f) {
										umgebendePixelWeiss++;
									}
									//HIER EVENTUELL EINSTELLEN
									if (levelbild.GetPixel (j + k, i + l).r < 0.6f && levelbild.GetPixel (j + k, i + l).b < 0.6f && levelbild.GetPixel (j + k, i + l).g > 0.6f) {
										umgebendePixelGruen++;
									}

									if (levelbild.GetPixel (j + k, i + l).r > 0.5f && levelbild.GetPixel (j + k, i + l).b < 0.4f && levelbild.GetPixel (j + k, i + l).g < 0.4f) {
										umgebendePixelRot++;
									}


									if (umgebendePixelWeiss >= 2) {
										truePixels[j, i] = false;
										break;
									}

									if (umgebendePixelRot >= 5) {
										truePixels[j, i] = false;
										break;
									}

									if (umgebendePixelGruen >= 3) {
										truePixels[j, i] = false;
										break;
									}

								}
							}
							
						}
						
					}
				}



		
			}
		
		}

	
		//Alle Werte auf False setzen, die nicht mindestens einen False-nachbarn haben:
		for (int i = 0; i < levelbild.height; i++) {
			
			for (int j = 0; j < levelbild.width; j++) {

				if(truePixels[j, i]) {

					for (int k = -2; k <= 2; k++) {
						
						for (int l = -2; l <= 2; l++) {

							if (l != 0 && k != 0) {
								
								//damit für die randpixel nicht alle umgebenden pixel auch noch gecheckt werden (out of bounds)
								if ((j + k) >= 0 && (j + k) < levelbild.width && (i + l) >= 0 && (i + l) < levelbild.height) {

									if (truePixels[(j + k), (i + l)] == false) {
										truePixelsProcessed[j, i] = true;
										break;
									}
								}
							}
						}
					}
				}
			}
		}

		truePixels = truePixelsProcessed;

		for (int i = 0; i < levelbild.height; i++) {
			
			for (int j = 0; j < levelbild.width; j++) {
				
				if(truePixels[j, i]) {

					truePixelsProcessed[j, i] = false;

					for (int k = -2; k <= 2; k++) {
						
						for (int l = -2; l <= 2; l++) {
							
							if (l != 0 && k != 0) {
								
								//damit für die randpixel nicht alle umgebenden pixel auch noch gecheckt werden (out of bounds)
								if ((j + k) >= 0 && (j + k) < levelbild.width && (i + l) >= 0 && (i + l) < levelbild.height) {
									
									if (truePixels[(j + k), (i + l)] == true) {
										truePixelsProcessed[j, i] = true;
										break;
									}
								}
							}
						}
					}
				}
			}
		}


		return truePixelsProcessed;
	}

	public void setPlayerAndTargetPosition() {
		//Debug.Log ("test1");
		bool instantiatedPlayer = false;
		bool instantiatedTarget = false;
		int instantiateCountPlayer = 0;
		int instantiateCountTarget = 0;

		//speichert die addierten werte für die farbe, falls mehrmals instanziiert werden soll, wird im höheren wert instanziiert
		float instantiateRed = 0.0f;
		float instantiateGreen = 0.0f;

		//vergleichswert
		float instantiateRedCurrent = 0.0f;
		float instantiateGreenCurrent = 0.0f;


		float faktor = (0.4f * (500.0f/levelbild.height)); //faktor durch probieren -.-'

		for (int i = 0; i < levelbild.height; i++) {

				for (int j = 0; j < levelbild.width; j++) {
						//fängt unten links an! x = x y = y
								
								if ((!instantiatedPlayer) && levelbild.GetPixel (j, i).r > 0.54f && levelbild.GetPixel (j, i).b < 0.5f && levelbild.GetPixel (j, i).g < 0.5f) {
									
						
									for (int k = -3; k <= 3; k++) {
										
										for (int l = -3; l <= 3; l++) {
											
											// weil das pixel selbst ja sowieso immer true ist dann
											if (l != 0 && k != 0) {
												
												//damit für die randpixel nicht alle umgebenden pixel auch noch gecheckt werden (out of bounds)
												if ((j + k) >= 0 && (j + k) < levelbild.width && (i + l) >= 0 && (i + l) < levelbild.height) {
													
													if (levelbild.GetPixel (j + k, i + l).r > 0.4f && levelbild.GetPixel (j + k, i + l).b < 0.4f && levelbild.GetPixel (j + k, i + l).g < 0.4f) {
										if((levelbild.GetPixel (j + k, i + l).r > (1.5f*((levelbild.GetPixel (j + k, i + l).g) + (levelbild.GetPixel (j + k, i + l).b))/2.0f))) {
															instantiateCountPlayer++;
											instantiateRedCurrent = instantiateRedCurrent + (levelbild.GetPixel (j + k, i + l).r);
										}

														if (instantiateCountPlayer == 6 && (instantiateRedCurrent > instantiateRed)) {

											GameObject.FindGameObjectWithTag("Rotor").transform.position = (new Vector3 (((j + k) - (levelbild.width / 2)) * (faktor), (((i + l) - (levelbild.height / 2)) * faktor )));
											instantiatedPlayer = true;
											instantiateCountPlayer = 0;
											instantiateRed = instantiateRedCurrent;

										}
									}
												}
											}
											
										}
						//Debug.Log("Player: " + instantiateCountPlayer);

						instantiateCountPlayer = 0;

					}
					
					
									
								
				
								}

				if ((!instantiatedTarget) && levelbild.GetPixel (j, i).r < 0.6f && levelbild.GetPixel (j, i).b < 0.6f && levelbild.GetPixel (j, i).g > 0.4f) {
					for (int k = -3; k <= 3; k++) {
										
										for (int l = -3; l <= 3; l++) {
											
											// weil das pixel selbst ja sowieso immer true ist dann
											if (l != 0 && k != 0) {
												
												//damit für die randpixel nicht alle umgebenden pixel auch noch gecheckt werden (out of bounds)
												if ((j + k) >= 0 && (j + k) < levelbild.width && (i + l) >= 0 && (i + l) < levelbild.height) {
													
									if (levelbild.GetPixel (j + k, i + l).r < 0.6f && levelbild.GetPixel (j + k, i + l).b < 0.6f && levelbild.GetPixel (j + k, i + l).g > 0.4f) {
										//sonst probleme wegen farbabstand
										if((levelbild.GetPixel (j + k, i + l).g - levelbild.GetPixel (j + k, i + l).b) > 0.18f) {
														instantiateCountTarget++;
														instantiateGreenCurrent = instantiateGreenCurrent + (levelbild.GetPixel (j + k, i + l).g);

										}
										if (instantiateCountTarget == 6 && (instantiateGreenCurrent > instantiateGreen)) {
															GameObject.FindGameObjectWithTag("Target").transform.position = (new Vector3 (((j + k) - (levelbild.width / 2)) * (faktor), (((i + l) - (levelbild.height / 2)) * faktor )));
															instantiatedTarget = true;
															instantiateCountTarget = 0;
															instantiateGreen = instantiateGreenCurrent;

														}
													

													}
												}
											}
											
										}
						instantiateCountTarget = 0;

										
									}


					
					
					
								}


								if (instantiatedPlayer && instantiatedTarget) {
									break;
								}
				instantiateRedCurrent = 0;
				instantiateGreenCurrent = 0;

						}
		}
	}


}
