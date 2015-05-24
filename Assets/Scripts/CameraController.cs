using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public PlayerController player;
	private float posX;
	private float posY;

	// Use this for initialization
	void Start () {
		/*
		 * Store the initial y & z positions, so that the original
		 * transform position has relevance
		 */
		posX = transform.position.x;
		posY = transform.position.y;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		/*
		 * At the end of each update, this camera will match the
		 * 	x (left-right) position of the attached player object
		 */
		if(transform.position.x!= player.transform.position.x)
		{
			transform.position = new Vector3(player.transform.position.x + posX, posY, transform.position.z);
		}
	}
}
