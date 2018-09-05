using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		TouchInfo info = AppUtil.GetTouch();
		if (info == TouchInfo.Began)
		{
			// タッチ開始
			SceneManager.LoadScene ("Title");
		}
	}
}
