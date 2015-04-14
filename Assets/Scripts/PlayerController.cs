using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float height;
	public MasterCubeGrid MCG;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(MCG.playerX,height,MCG.playerZ);
	}
	
	// Update is called once per frame
	void Update ()
	{
		/*
		 * Because the PlayerController class does not hold
		 * 	the player's location on the grid, this class is
		 * 	doing little more than displaying that position
		 * 	(right now, maybe ability resources are here too)
		 */
		transform.position = new Vector3(MCG.playerX,height,MCG.playerZ);
	}
}
