using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour
{
	AndroidJavaClass metaioClass;
	AndroidJavaClass jc;
	AndroidJavaObject jo;
	
	void Start ()
	{   
		jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
		jo = jc.GetStatic<AndroidJavaObject>("currentActivity");    
		metaioClass = new AndroidJavaClass("com.metaio.sdk.jni.IMetaioSDKAndroid");

	}
	
	void OnGUI ()
	{   
		if(GUI.Button(new Rect (600, 100, 100, 100), "ON"))
		{
		
			metaioClass.CallStatic("startTorch",jo);
		}
		
		if(GUI.Button(new Rect (700, 100, 100, 100), "OFF"))
		{

			metaioClass.CallStatic("stopTorch",jo);
		}   
	}
}