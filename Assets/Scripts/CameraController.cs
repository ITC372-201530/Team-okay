using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public PlayerController player;
	private float posY;
	private float posZ;

	// Use this for initialization
	void Start () {
		/*
		 * Store the initial y & z positions, so that the original
		 * transform position has relevance
		 */
		posY = transform.position.y;
		posZ = transform.position.z;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		/*
		 * At the end of each update, this camera will match the
		 * 	x (left-right) position of the attached player object
		 */
		if(transform.position.x!= player.transform.position.x)
		{
			transform.position = new Vector3(player.transform.position.x,posY,posZ);
		}
	}
}
