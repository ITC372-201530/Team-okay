using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameUICombo : MonoBehaviour {
	
	public PlayerController player;
	Text txt;
	
	void Start()
	{
		txt = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		string display ="COMBO\n";
		
		
		display+= player.chain.ToString("0.00");
		display+='\n';

		txt.text = display;
	}
	
}
