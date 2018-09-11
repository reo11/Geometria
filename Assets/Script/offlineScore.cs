using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class offlineScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var score = InGameScore.getScore();
        this.GetComponent<Text>().text = "Score:" + score;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
