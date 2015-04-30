﻿using UnityEngine;
using System.Collections;

public class Ability {

	static public void levelOneAbility(MasterCubeGrid mcg, PlayerController player){
		mcg.darknessSpeed -= 10;
		player.subtractPowers();
	}

	static public void levelTwoAbility(PlayerController player){
		player.freeCombo = 4;
		player.subtractPowers();
	}

	static public void levelThreeAbility(MasterCubeGrid mcg, PlayerController player) {
		mcg.scoreMultiplier *= 1.3;
		player.subtractPowers();
	}
}