using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PointManeger : MonoBehaviour {

    public GameObject pointPrefab;
    public Canvas canvas;
    public GameObject linePrefab;
    const int pointNum = 5;

    // 一時的に位置を用意
    Vector2[] PosList = new Vector2[pointNum]{
        new Vector2(0f, 0f),
        new Vector2(0f, -500f),
        new Vector2(-300f, 0f),
        new Vector2(300f, 0f),
        new Vector2(0f, 500f),
        };

    // poslistのworld座標版(poslistはui座標)
    Vector3[] WorldPosList = new Vector3[pointNum];

    // 点のつながりを保存(Falseで初期化される)
    bool[,] connectionMap = new bool[pointNum, pointNum];

    // 1つ前の押された点を保持
    int firstPoint = -1;
    // 2つ前の押された点を保持(三角形検出用)
    int secondPoint = -1;

    // 検出した三角形リスト
    List<int[]> detectedTriangles = new List<int[]>();


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
        //prefab.GetComponent<RectTransform>().localPosition = position;
        rectTransform.localPosition = position;
        WorldPosList[id] = GetWorldPositionFromRectPosition(rectTransform);

        // 引数に何番目のボタンかを渡す
        //int test = prefab.GetComponent<ButtonColorChange>().Id;  // 何故かこれでid取ってこれなかった、要検証
        // ↑このタイミングではまだbuttoncolorchangeのstart関数が走ってないのでidに適切な数が代入されてない(すべて0)
        // onClickに追加する
        prefab.GetComponent<Button>().onClick.AddListener(() => { OnClickPoint(id); });
    }

    void OnClickPoint(int Id)
    {
        // 点のつながりを更新
        if(firstPoint != -1)
        {
            if (connectionMap[Id, firstPoint] == false)
            {
                connectionMap[Id, firstPoint] = true;
                connectionMap[firstPoint, Id] = true;
                DrawLine(WorldPosList[firstPoint], WorldPosList[Id]);
            }

            secondPoint = firstPoint;
            firstPoint = Id;
            // 三角形のチェック処理を入れる
            CheckTriangle();
        }
        else
        {
            firstPoint = Id;
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

    void DrawLine(Vector2 start, Vector2 end)
    {
        var prefab = Instantiate(linePrefab, transform.position, transform.rotation) as GameObject;
        prefab.GetComponent<line>().SetLine(start, end);
    }

    // uiの座標をworld座標に変換する関数
    Vector3 GetWorldPositionFromRectPosition(RectTransform rect)
    {
        /*
        http://alien-program.hatenablog.com/entry/2017/08/06/164258
        // こっからパクった
        */
        //UI座標からスクリーン座標に変換
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, rect.position);

        //ワールド座標
        Vector3 result = Vector3.zero;

        //スクリーン座標→ワールド座標に変換
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, screenPos, canvas.worldCamera, out result);
        return result;
    }

    void CheckTriangle()
    {
        for(int i = 0; i < pointNum; i++)
        {
            if(connectionMap[firstPoint, i] == true && connectionMap[secondPoint, i] == true)
            {
                int[] triangle = new int[3] { firstPoint, secondPoint, i };
                // sortして重複して入らないように
                Array.Sort(triangle);
                /*
                // TODO:何故かcontainsが重複していてもfalseになってしまっているので要修正
                // 配列の比較が == で出来ないクソ仕様っぽい
                if (detectedTriangles.Contains(triangle) == true)
                {
                    detectedTriangles.Add(triangle);
                    
                    Debug.Log("Triangle!");
                    Debug.Log(triangle[0]);
                    Debug.Log(triangle[1]);
                    Debug.Log(triangle[2]);
                    Debug.Log(detectedTriangles.Count);
                }
                */
                bool duplication = false;
                for (int j = 0; j < detectedTriangles.Count; j++)
                {
                    if(triangle[0] == detectedTriangles[j][0] && triangle[1] == detectedTriangles[j][1] && triangle[2] == detectedTriangles[j][2])
                    {
                        duplication = true;
                    }
                }
                if(duplication == false)
                {
                    detectedTriangles.Add(triangle);

                    Debug.Log("Triangle!");
                    Debug.Log(triangle[0]);
                    Debug.Log(triangle[1]);
                    Debug.Log(triangle[2]);
                    Debug.Log(detectedTriangles.Count);
                }
            }
        }
    }
}
