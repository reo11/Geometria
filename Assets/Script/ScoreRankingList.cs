using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRankingList : MonoBehaviour {
	public ScoreText scoreText;
	public Text screText; //Text用変数
	private int score = 0; //スコア計算用変数
	// Use this for initialization
	void Start () {
		scoreText.GetComponent<ScoreText>().score++;
	}
	
}
