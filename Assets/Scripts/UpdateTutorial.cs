using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateTutorial : MonoBehaviour {

	public MasterCubeGrid grid;
	public PlayerController player;

	//the current 'page' of the tutorial
	public int tutorialPart = 1;

	// Update is called once per frame
	void Update () {

		string display = null;
		Text displayText = null;

		//early tutorial pages are based on the state of the grid
		if (tutorialPart < 4) {
			tutorialPart = grid.tutorial;
		}
		//later is based on the player resources
		else {
			tutorialPart = player.tutorial;
			if (tutorialPart == 4)
				tutorialPart = 8;
		}

		//write appropriate tutorial string
		switch (tutorialPart) {
		case 1:
			display = "Move around with WASD";
			break;
		case 2:
			display = "Avoid the empty spaces";
			break;
		case 3:
			display = "Moving to a colour shown above gets you points and power";
			break;
		case 4:
			display = "Building power unlocks abilities, shown in the top right";
			break;
		case 5:
			display = "You have one full power bar, press space to set your combo chain to five or save it for a different ability";
			break;
		case 6:
			display = "You have two full power bars, press space to get four free moves or save it for a different ability";
			break;
		case 7:
			display = "You have three full power bars, press space to increase your score multiplier";
			break;
		case 8:
			display = "";
			break;
		default:
			break;
		}

		//display string
		displayText = GetComponent<Text>();
		displayText.text = ""+display;
	}
}
