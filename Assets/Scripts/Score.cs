using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {


	private string score;
	private string highscore;
	// Use this for initialization
	void Start () {
	
		score = PlayerPrefs.GetString("PScore");
		guiText.text = score;


	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
