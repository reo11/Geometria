using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour {

    int hitPoint = 10000;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SubHP(int score)
    {
        hitPoint -= score;
        this.GetComponent<Text>().text = "HP:" + hitPoint.ToString();
    }

    public void AddHP(int score)
    {
        hitPoint += score;
        this.GetComponent<Text>().text = "HP:" + hitPoint.ToString();
    }
}
