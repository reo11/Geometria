using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManeger : MonoBehaviour {

    public GameObject pointPrefab;
    public Canvas canvas;
    const int pointNum = 5;

    // 一時的に位置を用意
     Vector3[] PosList = new Vector3[pointNum]{
        new Vector3(0f, 0f, 0f),
        new Vector3(0f, -500f, 0f),
        new Vector3(-300f, 0f, 0f),
        new Vector3(300f, 0f, 0f),
        new Vector3(0f, 500f, 0f),
        };

    // 点のつながりを保存(Falseで初期化される)
    bool[,] connectionMap = new bool[pointNum, pointNum];

    // 1つ前の押された点を保持
    int aheadPoint = -1;


    // Use this for initialization
    void Start() {
        for (int i = 0; i < pointNum; i++)
        {
            PointGen(PosList[i], i);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    void PointGen(Vector3 position, int id)
    {
        var prefab = Instantiate(pointPrefab, canvas.transform, false) as GameObject;
        RectTransform rectTransform = prefab.GetComponent<RectTransform>();
        rectTransform.localPosition = position;

        // 引数に何番目のボタンかを渡す
        //int test = prefab.GetComponent<ButtonColorChange>().Id;  // 何故かこれでid取ってこれなかった、要検証
        // ↑このタイミングではまだbuttoncolorchangeのstart関数が走ってないのでidに適切な数が代入されてない(すべて0)
        // onClickに追加する
        prefab.GetComponent<Button>().onClick.AddListener(() => { OnClickPoint(id); });
    }

    void OnClickPoint(int Id)
    {
        // 点のつながりを更新
        if(aheadPoint == -1)
        {
            aheadPoint = Id;
        }
        else
        {
            connectionMap[Id, aheadPoint] = true;
            connectionMap[aheadPoint, Id] = true;
        }
        /*
        //Debug.Log(connectionMap); // 型しか表示されねぇ！
        for(int i =0; i < pointNum; i++)
        {
            for(int j =0; j< pointNum; j++)
            {
                if (connectionMap[i, j])
                {
                    Debug.Log("[" + i.ToString() + "," + j.ToString() + "]");
                }
            }
        }
        */
    }
}
