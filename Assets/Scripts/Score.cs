using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Score : MonoBehaviour {


	private float score;
	private float highscore;
	private string scorename;
	private string highscorename;
	// Use this for initialization
	void Start () {
	
		score = PlayerPrefs.GetFloat("PScore");
		highscorename = PlayerPrefs.GetString ("HScoreN");
		highscore = PlayerPrefs.GetFloat ("HScore");
		if(score > highscore)
		{
			PlayerPrefs.SetFloat("HScore", score);
			//PlayerPrefs.SetFloat("HScoreN", highscorename);
		}
		guiText.text = score.ToString("0.00");



	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
