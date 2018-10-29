using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour {

    private int hitPoint = 13000;
    string HpPrefix = "HP:";

    // Use this for initialization
    void Start () {
        this.GetComponent<Text>().text = HpPrefix + hitPoint.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SubHP(int score)
    {
        hitPoint -= score;
        this.GetComponent<Text>().text = HpPrefix + hitPoint.ToString();
    }

    public void AddHP(int score)
    {
        hitPoint += score;
        this.GetComponent<Text>().text = HpPrefix + hitPoint.ToString();
    }

    public bool CheckHP(int score)
    {
        return (score <= hitPoint);
    }
}
