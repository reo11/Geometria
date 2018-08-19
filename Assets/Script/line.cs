using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 無くてもいい気がするがサンプルプログラムにあったので書いとく
[RequireComponent(typeof(LineRenderer))]
public class line : MonoBehaviour {

    //private LineRenderer lineRenderer;

    void Start()
    {

    }

    void Update()
    {
        // サンプルプログラム
        /*
        if (Input.GetMouseButtonDown(0))
        {
            // クリック位置した座標の取得。 
            Vector2 goPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 頂点を追加。 
            ++this.lineRenderer.numPositions;

            // 追加した頂点の座標を設定。 
            this.lineRenderer.SetPosition(this.lineRenderer.numPositions - 1, goPosition);
        }
        */
    }

    public void SetLine(Vector2 start, Vector2 end)
    {
        // 幅とかの設定はいったんインスペクターでやることにする
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}
