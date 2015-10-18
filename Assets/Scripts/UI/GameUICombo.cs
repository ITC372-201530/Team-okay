using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

//displays current chain value
public class GameUICombo : MonoBehaviour {
	
	public PlayerController player;
	Text txt;
	string format;

	void Start()
	{
		txt = GetComponent<Text>();
		format = txt.text;
	}
	
	// Update is called once per frame
	void Update () {
		string display ="";

		//only displays if the value is at least one
		// (the combo exists)
		if(player.chain != 0)
		{
			display+= player.chain.ToString(format);
		}
		txt.text = display;
	}
	
}
