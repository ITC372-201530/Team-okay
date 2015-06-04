using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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

		
		display+= map.scoreMultiplier.ToString("0.00");
		display+="\n";
		display +=  (map.score/10).ToString("0.00");



		txt.text = display;
	}

}
