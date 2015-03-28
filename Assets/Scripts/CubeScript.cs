using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour {

	public GameObject cubeLeft;
	public GameObject cubeRight;
	public GameObject colouring;
	public bool player;
	private Light lightComp;

	private bool spawning;

	// Use this for initialization
	void Start () {

		spawning = true;
		lightComp = colouring.GetComponent<Light> ();

		float sizeX = transform.localScale.x;
		lightComp.color = new Color (0, 0, 1);
		lightComp.range = sizeX*2;

		transform.position += new Vector3(0,-10,0);
		
	}
	
	// Update is called once per frame
	void Update () {
		float sizeY = -transform.localScale.y;
		float moveMag = 1f * Time.deltaTime;

		if (transform.position.y < sizeY && spawning) {
			transform.position += new Vector3 (0, (moveMag), 0);
		} else {
			spawning = false;
		}
		if (transform.position.y <= (sizeY/2) && player) {
			transform.position+= new Vector3(0,(moveMag/2),0);
		}
		else if (transform.position.y >= sizeY && !player) {
			transform.position+= new Vector3(0,-(moveMag/4),0);
		}
	}
}