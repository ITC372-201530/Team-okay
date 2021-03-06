﻿using UnityEngine;
using System.Collections;

//displays a list of game state
// used for debugging
public class HUDText : MonoBehaviour {

	public PlayerController player;
	public MasterCubeGrid grid;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
	
		string display = null;

		if(player.freeCombo!=0)
		{
			display += "Free Combo Left:\t";
			display += player.freeCombo;
		}
		display += "\n\nRed Power:\t";
		display += player.rPower;
		display += "\nGreen Power:\t";
		display += player.gPower;
		display += "\nBlue Power:\t";
		display += player.bPower;
		display += "\n\nCombo:\t\t";
		display += player.chain;
		display += "\n\nDarkness Speed:\t\t";
		display += grid.darknessCounter;
		display += "\n\nScore:\t\t";
		display += grid.score;
		display += "\nScore Muliplier:\t";
		display += grid.scoreMultiplier;

		guiText.text = display;

	}
}
