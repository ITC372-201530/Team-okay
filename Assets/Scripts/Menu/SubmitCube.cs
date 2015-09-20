using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SubmitCube : FormChangeClick {
	
	public Canvas initialsCanvas;
	public Initials charCube1, charCube2, charCube3;
	public Text submitText;
	public GameOverPlayer player;

	public Score score;
	
	public override void OnMouseDown () {

		string name = "";
		name+=charCube1.getChar();
		name+=charCube2.getChar();
		name+=charCube3.getChar();

		score.submit(name);

		PlayerPrefs.SetString("LastName",name);

		submitText.gameObject.SetActive(false);
		initialsCanvas.gameObject.SetActive(false);
		charCube1.gameObject.SetActive(false);
		charCube2.gameObject.SetActive(false);
		charCube3.gameObject.SetActive(false);
		gameObject.SetActive(false);

		player.Search();
		player.toX = 0;

	}
	
}