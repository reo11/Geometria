using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour {
	public Text scoreText; //Text用変数
	private int score = 0; //スコア計算用変数
	// Use this for initialization
	void Start () {
		scoreText.text = "1:TestUser 000"; //初期スコアを代入して画面に表示
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = GetComponent<FireBaseService>().getScoreRanking();
	}
}
