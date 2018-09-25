using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OfflineRankingManager : MonoBehaviour {

    public Text Score1st;
    public Text Score2nd;
    public Text Score3rd;
    public Text Score;

    const int ranknum = 3;

    int[] scores = new int[ranknum+1];

    // PlayerPrefで保存するために使うキー
    string[] RankingKeys = new string[ranknum] { "first", "second", "third" };

    // Use this for initialization
    void Start()
    {
        // スコア取得
        for(int i = 0; i < ranknum; i++)
        {
            scores[i] = PlayerPrefs.GetInt(RankingKeys[i], 0);
        }
        // scoresの最後に今回のスコアを入れる
        scores[ranknum] = InGameScore.getScore();
        Score.text = scores[ranknum].ToString();
        Debug.Log(InGameScore.getScore());

        // ソートしてスコアを表示
        Array.Sort(scores);
        Array.Reverse(scores);
        Score1st.text = scores[0].ToString();
        Score2nd.text = scores[1].ToString();
        Score3rd.text = scores[2].ToString();

        Debug.Log(scores[0]);
        Debug.Log(scores[1]);
        Debug.Log(scores[2]);
        Debug.Log(scores[3]);

        // スコア更新
        for (int i = 0; i < ranknum; i++)
        {
            PlayerPrefs.SetInt(RankingKeys[i], scores[i]);
        }
        // 保存
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
