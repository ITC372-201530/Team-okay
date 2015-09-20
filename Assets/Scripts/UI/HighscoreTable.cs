using UnityEngine;
using System.Collections;

public class HighscoreTable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		for (int i = 1; i < 11; i++) {
			string hs = "highscore"+i;
			string hsn = "highscorename"+i;
			this.guiText.text += PlayerPrefs.GetString (hsn) + ":" + PlayerPrefs.GetFloat (hs).ToString ("0.00") + "\n";

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
