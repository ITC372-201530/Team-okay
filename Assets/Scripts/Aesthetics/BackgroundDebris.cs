using UnityEngine;
using System.Collections;

public class BackgroundDebris : MonoBehaviour {

	private float spd;

	// Use this for initialization
	void Start () {
	
		initLoc();

	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition -= new Vector3(0,0,spd * Time.deltaTime * 10);
		//spd +=0.01f;

		if(transform.localPosition.z <= 0)
		{
			initLoc ();
		}
	}

	private void initLoc()
	{
		float x = UnityEngine.Random.value*2;

		if(x > 1)
		{
			x = 5 + x;
		}
		else
		{
			x = -6 - x;
		}

		float y = UnityEngine.Random.value * 8 - 4;

		float z = UnityEngine.Random.value * 100;

		transform.localPosition = new Vector3(x,y,100+z);
		spd = UnityEngine.Random.value+ 1;
	}

}
