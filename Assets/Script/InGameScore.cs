using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameScore : MonoBehaviour {

    public static int offlineScore;

	// Use this for initialization
	void Start () {
        offlineScore = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PrintScore(int score)
    {
        offlineScore = score;
        this.GetComponent<Text>().text = "Score:" + score.ToString();
    }

    public static int getScore()
    {
        return (offlineScore);
    }
}
