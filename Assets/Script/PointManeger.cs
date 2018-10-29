using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PointManeger : MonoBehaviour
{

    public GameObject pointPrefab;
    public Canvas canvas;
    public Canvas buttonCanvas;
    public GameObject linePrefab;
    const int pointNum = 11;
    public GameObject trianglePrefab;
    public GameObject ScoreText;
    public GameObject HPText;
    public GameObject SceneController;

    // 一時的に位置を用意
    Vector2[] PosList = new Vector2[pointNum]{
        new Vector2(-400f, 108f),
        new Vector2(-362f, -187f),
        new Vector2(-255f, 56f),
        new Vector2(-52f, 123f),
        new Vector2(-127f, -147f),
        new Vector2(47f, -32f),
        new Vector2(59f, -187f),
        new Vector2(386f, 84f),
        new Vector2(191f, 146f),
        new Vector2(258f, -60f),
        new Vector2(362f, -191f),
    };

    // poslistのworld座標版(poslistはui座標)
    Vector2[] WorldPosList = new Vector2[pointNum];

    // 点のつながりを保存(Falseで初期化される)
    bool[,] connectionMap = new bool[pointNum, pointNum];

    // 1つ前の押された点を保持
    int firstPoint = -1;
    // 2つ前の押された点を保持(三角形検出用)
    int secondPoint = -1;

    // スコア
    int score;

    // 検出した三角形リスト
    List<int[]> detectedTriangles = new List<int[]>();

    // 生成したpoint objectのリスト
    GameObject[] PointObjects = new GameObject[pointNum];

    // 削除された点の保存リスト(Falseに初期化されるっぽい)
    bool[] DeletedPoint = new bool[pointNum];

    // Use this for initialization
    void Start()
    {
        score = 0;
        for (int i = 0; i < pointNum; i++)
        {
            PointGen(PosList[i], i);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PointGen(Vector3 position, int id)
    {
        var prefab = Instantiate(pointPrefab, canvas.transform, false) as GameObject;
        // 親を設定する
        prefab.transform.SetParent(buttonCanvas.transform, false);
        RectTransform rectTransform = prefab.GetComponent<RectTransform>();
        //prefab.GetComponent<RectTransform>().localPosition = position;
        rectTransform.localPosition = position;
        WorldPosList[id] = GetWorldPositionFromRectPosition(rectTransform);

        // 引数に何番目のボタンかを渡す
        //int test = prefab.GetComponent<ButtonColorChange>().Id;  // 何故かこれでid取ってこれなかった、要検証
        // ↑このタイミングではまだbuttoncolorchangeのstart関数が走ってないのでidに適切な数が代入されてない(すべて0)
        // onClickに追加する
        prefab.GetComponent<Button>().onClick.AddListener(() => { OnClickPoint(id); });
        // 後でdestroyしやすいようにここで取っておいてみる
        PointObjects[id] = prefab;
    }

    void OnClickPoint(int Id)
    {
        // 連続して点を押された場合は無視
        if (Id != firstPoint)
        {
            // 点のつながりを更新
            if (firstPoint != -1)
            {
                // HPが十分にあるか検証
                bool enough = HPText.GetComponent<HP>().CheckHP(CalcHP(Id, firstPoint));
                if (enough == true)
                {
                    if (connectionMap[Id, firstPoint] == false)
                    {
                        connectionMap[Id, firstPoint] = true;
                        connectionMap[firstPoint, Id] = true;
                        DrawLine(WorldPosList[firstPoint], WorldPosList[Id]);
                    }

                    secondPoint = firstPoint;
                    firstPoint = Id;
                    // コスト消費の処理を追加
                    HPText.GetComponent<HP>().SubHP(CalcHP(secondPoint, firstPoint));
                    // 三角形のチェック処理を入れる
                    var triangles = CheckTriangle();
                    foreach (var triangle in triangles)
                    {
                        Debug.Log(CalcArea(triangle));
                        Debug.Log(detectedTriangles.Count);
                        Scoring(triangle);
                        DrawTriangle(triangle);
                    }
                }
                // 終了判定を入れたい
                bool finish = false;
                for (int i = 0; i < pointNum; i++)
                {
                    if (i != firstPoint && i != secondPoint && DeletedPoint[i] != true) {
                         finish = finish || HPText.GetComponent<HP>().CheckHP(CalcHP(firstPoint, i));
                    }
                }
                if(finish == false)
                {
                    // シーン遷移画面へ
                    Debug.Log("finish");
                    SceneController.GetComponent<SceneController>().OnClickOfflineScore();
                }
            }
            else
            {
                firstPoint = Id;
            }
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
        // 親を設定
        prefab.transform.SetParent(canvas.transform, false);
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

    List<int[]> CheckTriangle()
    {
        List<int[]> diffTriangles = new List<int[]>();

        for (int i = 0; i < pointNum; i++)
        {
            if (connectionMap[firstPoint, i] == true && connectionMap[secondPoint, i] == true)
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
                    if (triangle[0] == detectedTriangles[j][0] && triangle[1] == detectedTriangles[j][1] && triangle[2] == detectedTriangles[j][2])
                    {
                        duplication = true;
                    }
                }
                var inside = CheckInside(triangle);
                if (duplication == false && inside == true)
                {
                    detectedTriangles.Add(triangle);
                    diffTriangles.Add(triangle);
                }
                // 三角形の内部の点を消す?
                for (int num = 0; num < pointNum; num++)
                {
                    if (num != triangle[0] && num != triangle[1] && num != triangle[2])
                    {
                        Vector3[] pos = new Vector3[3]
                        {
                        WorldPosList[triangle[0]],
                        WorldPosList[triangle[1]],
                        WorldPosList[triangle[2]],
                        };
                        var insidepoint = CheckClockwise(pos, WorldPosList[num]);
                        if (insidepoint == 0)
                        {
                            DeletedPoint[num] = true;
                            Destroy(PointObjects[num]);
                        }
                    }
                }
            }
        }

        return (diffTriangles);
    }

    float CalcArea(int[] triangle)
    {
        float x1 = PosList[triangle[1]][0] - PosList[triangle[0]][0];
        float y1 = PosList[triangle[1]][1] - PosList[triangle[0]][1];
        float x2 = PosList[triangle[2]][0] - PosList[triangle[0]][0];
        float y2 = PosList[triangle[2]][1] - PosList[triangle[0]][1];

        float area = 0.5f * Mathf.Abs(x2 * y1 - y2 * x1);
        return (area);
    }

    // 三角形を描画する
    /*
     * こっからパクった
     * http://www.shibuya24.info/entry/2015/11/29/180748
    */
    void DrawTriangle(int[] triangle)
    {
        Vector3[] position = new Vector3[]
        {
                WorldPosList[ triangle[0] ],
                WorldPosList[ triangle[1] ],
                WorldPosList[ triangle[2] ],
        };

        var prefab = Instantiate(trianglePrefab, transform.position, transform.rotation) as GameObject;
        // 親を設定
        prefab.transform.SetParent(canvas.transform, true);
        prefab.GetComponent<Triangle>().SetTriangle(position);
    }

    void Scoring(int[] triangle)
    {
        float area = CalcArea(triangle);
        // 今はintに変換して1/10にしとく(スコアリング)
        score += (int)area / 10;
        ScoreText.GetComponent<InGameScore>().PrintScore(score);
    }

    int CalcHP(int first, int second)
    {
        Vector3 line = WorldPosList[second] - WorldPosList[first];
        return ((int)line.sqrMagnitude / 20);
        
    }

    // ここで内外判定をする
    bool CheckInside(int[] triangle)
    {
        foreach (var detectedTriangle in detectedTriangles)
        {
            Vector3[] pos = new Vector3[3]
            {
                WorldPosList[detectedTriangle[0]],
                WorldPosList[detectedTriangle[1]],
                WorldPosList[detectedTriangle[2]],
            };
            var x = CheckClockwise(pos, WorldPosList[triangle[0]]);
            var y = CheckClockwise(pos, WorldPosList[triangle[1]]);
            var z = CheckClockwise(pos, WorldPosList[triangle[2]]);
            //Debug.Log(x);
            //Debug.Log(y);
            //Debug.Log(z);
            if (x + y + z < 2)
            {
                return (false);
            }
        }
        return (true);
    }

    // 内外判定用の外積(Triangleスクリプトにもある)
    int CheckClockwise(Vector3[] triangle, Vector3 point)
    {
        Vector3 AB = triangle[1] - triangle[0];
        Vector3 BC = triangle[2] - triangle[1];
        Vector3 CA = triangle[0] - triangle[2];
        Vector3 AP = point - triangle[0];
        Vector3 BP = point - triangle[1];
        Vector3 CP = point - triangle[2];

        Vector3 crossA = Vector3.Cross(AB, BP).normalized;
        Vector3 crossB = Vector3.Cross(BC, CP).normalized;
        Vector3 crossC = Vector3.Cross(CA, AP).normalized;

        Debug.Log(crossA);
        Debug.Log(crossB);
        Debug.Log(crossC);

        if ((crossA[2] > 0 && crossB[2] > 0 && crossC[2] > 0) || (crossA[2] < 0 && crossB[2] < 0 && crossC[2] < 0) || (Math.Abs(crossA[2] + crossB[2]+crossC[2]) > 1))
        {
            //三角形の内側に点がある
            return 0;
        }
        else
        {
            return 1;
        }
    }
}
