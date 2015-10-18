using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//displays the list of high scores
public class HighscoreTable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//retrieve all 10 saved high scores
		for (int i = 1; i < 11; i++) {
			string hs = "highscore"+i;
			string hsn = "highscorename"+i;
			gameObject.GetComponent<Text>().text += PlayerPrefs.GetString (hsn) + ":" + PlayerPrefs.GetFloat (hs).ToString ("0.00") + "\n";

		}
	}
}
