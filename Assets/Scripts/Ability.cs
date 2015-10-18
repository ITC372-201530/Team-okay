using UnityEngine;
using System.Collections;

//Collection of static functions to isolate abilities that change properties
// of both player and grid
public class Ability {

	//Executed when the player has one full power bar
	// Sets the combo counter to a specific value
	static public void levelOneAbility(MasterCubeGrid mcg, PlayerController player){
		player.chain = 5;
		player.subtractPowers();
	}

	//Executed when the player has two full power bars
	// Grants a number of moves that won't break the combo bar
	static public void levelTwoAbility(PlayerController player){
		player.addFreeCombo(3);
		player.subtractPowers();
	}

	//Executed when the player has two full power bars
	// Multiplies the score multiplier for the rest of the run
	static public void levelThreeAbility(MasterCubeGrid mcg, PlayerController player) {
		//Exponential Growth Hell Yeah
		mcg.scoreMultiplier *= 1.3f;
		player.subtractPowers();
	}
}
