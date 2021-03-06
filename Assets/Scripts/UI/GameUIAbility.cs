﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

//displays the text according to what ability is ready
public class GameUIAbility : MonoBehaviour {
	
	public PlayerController player;
	Text txt;
	
	void Start()
	{
		txt = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		string display;
		//retrieve what ability is readied
		// (which ability would be used if
		// the player pressed space bar right now)
		switch(player.checkAbilityLevel())
		{
		default:
		case 0:
			display = "Ability Powers";
			break;
		case 1:
			display = "Chain Restore";
			break;
		case 2:
			display = "Free Combo";
			break;
		case 3:
			display = "Score Multiplier";
			break;
		}
		
		txt.text = display;
	}
	
}
