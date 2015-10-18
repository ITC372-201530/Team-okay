using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

//displays the player's current score
public class GameUIScore : MonoBehaviour {

	public MasterCubeGrid map;
	Text txt;

	void Start()
	{
		txt = GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {
		string display ="SCORE\n+";

		//format to two decimal places cause it looks cool
		display+= map.scoreMultiplier.ToString("0.00");
		display+="\n";
		display +=  (map.score/10).ToString("0.00");



		txt.text = display;
	}

}
