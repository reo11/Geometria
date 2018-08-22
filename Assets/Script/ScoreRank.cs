using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRank {
	private int mapNumber;
	private int rank;
	private string name;
    private double score;

	public void setScore(int mapNum,int rank, string name, string score){
		this.mapNumber = mapNum;
		this.rank = rank;
		this.name = name;
		this.score = double.Parse(score);
	}
}
