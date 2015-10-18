using UnityEngine;
using System.Collections;

//A class to attach to a Camera GameObject so it follows
// the player in their progression
public class CameraController : MonoBehaviour {

	public PlayerController player;
	private float posX;
	private float posY;

	//Use this for initialization
	void Start () {
		//Store the initial y & z positions, so that the original
		// transform position has relevance
		posX = transform.position.x;
		posY = transform.position.y;
	}
	
	//LateUpdate () is called after Update (), so can be relied on
	// to represent the current position of the player
	// (this function is between the player updating and drawing)
	void LateUpdate () {
		//At the end of each update, this camera will match the
		// x (up-down) position of the attached player object
		if(transform.position.x!= player.transform.position.x)
		{
			transform.position = new Vector3(player.transform.position.x + posX, posY, transform.position.z);
		}
	}
}
