using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManeger : MonoBehaviour {

    public GameObject pointPrefab;
    public int pointNum;
    public Canvas canvas;

    // 一時的に位置を用意
    List<Vector3> PosList = new List<Vector3>(){
        new Vector3(0f, 0f, 0f),
        new Vector3(0f, -500f, 0f),
        new Vector3(-300f, 0f, 0f),
        new Vector3(300f, 0f, 0f),
        new Vector3(0f, 500f, 0f),
        };

    // Use this for initialization
    void Start () {
        for (int i = 0; i < pointNum; i++)
        {
            PointGen(PosList[i]);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void PointGen(Vector3 position)
    {
        var prefab = Instantiate(pointPrefab, canvas.transform, false) as GameObject;
        RectTransform rectTransform = prefab.GetComponent<RectTransform>();
        rectTransform.localPosition = position;
    }
}
