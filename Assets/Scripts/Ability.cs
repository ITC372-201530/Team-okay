using UnityEngine;
using System.Collections;

public class Ability {

	static public void levelOneAbility(MasterCubeGrid mcg, PlayerController player){
		if(player.chain < 5)
		{
			player.chain = 5;
			player.subtractPowers();

		}
	}

	static public void levelTwoAbility(PlayerController player){
		player.addFreeCombo(4);
		player.subtractPowers();
	}

	static public void levelThreeAbility(MasterCubeGrid mcg, PlayerController player) {
		mcg.scoreMultiplier *= 1.3f;
		player.subtractPowers();
	}
}
