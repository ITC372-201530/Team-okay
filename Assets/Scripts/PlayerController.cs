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
	void Update () {
		transform.position = new Vector3(MCG.playerX,height,MCG.playerZ);
	}
}
