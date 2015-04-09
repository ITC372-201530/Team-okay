using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public PlayerController player;
	private float posY;
	private float posZ;

	// Use this for initialization
	void Start () {
		posY = transform.position.y;
		posZ = transform.position.z;
	}
	
	// Update is called once per frame
	void LateUpdate () {

		if(transform.position.x!= player.transform.position.x)
		{
			transform.position = new Vector3(player.transform.position.x,posY,posZ);
		}
	}
}
