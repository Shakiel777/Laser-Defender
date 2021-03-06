﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

    public static int score = 0;
    private Text myScore;

    void Start()
    {
        myScore = GetComponent<Text>();
        Reset();
    }

    public void Score(int points)
    {
        // Debug.Log("Scored Points");
        score += points;
        myScore.text = score.ToString();
    }
	
	// Update is called once per frame
	public static void Reset ()
    {
        score = 0;
        // myScore.text = score.ToString(); (not needed because now calling static)
    }
}
