using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SubmitCube : FormChangeClick {

	//the canvas displaying the text of the initials
	public Canvas initialsCanvas;
	//the cubes displaying inditials
	public Initials charCube1, charCube2, charCube3;
	//the text for this cube
	public Text submitText;
	public GameOverPlayer player;

	public Score score;
	
	public override void OnMouseDown () {
		//gathers the characters from the initials cubes
		string name = "";
		name+=charCube1.getChar();
		name+=charCube2.getChar();
		name+=charCube3.getChar();

		//submits the initials to the score object
		score.submit(name);

		//saves the last-used initials to be loaded later
		PlayerPrefs.SetString("LastName",name);

		//disables initials cubes and this cube,
		// score cannot be submitted twice per run
		submitText.gameObject.SetActive(false);
		initialsCanvas.gameObject.SetActive(false);
		charCube1.gameObject.SetActive(false);
		charCube2.gameObject.SetActive(false);
		charCube3.gameObject.SetActive(false);
		gameObject.SetActive(false);

		//moves the player to a still-existing cube
		player.Search();
		player.toX = 0;

	}
	
}