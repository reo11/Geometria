using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameScore : MonoBehaviour {

    public static string offlineScore;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PrintScore(string score)
    {
        offlineScore = score;
        this.GetComponent<Text>().text = "Score:" + score;
    }

    public static string getScore()
    {
        return (offlineScore);
    }
}
