using UnityEngine;
using System.Collections;

public class HUDText : MonoBehaviour {

	public PlayerController player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
	
		string display = null;

		display += "Red Power:\t";
		display += player.rPower;
		display += "\nGreen Power:\t";
		display += player.gPower;
		display += "\nBlue Power:\t";
		display += player.bPower;
		display += "\n\nCombo:\t\t";
		display += player.chain;
	
		guiText.text = display;

	}
}
