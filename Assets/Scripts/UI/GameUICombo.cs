using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
		
		if(player.chain != 0)
		{
			display+= player.chain.ToString(format);
		}
		txt.text = display;
	}
	
}
