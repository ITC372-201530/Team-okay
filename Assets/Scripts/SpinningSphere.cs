using UnityEngine;
using System.Collections;

public class SpinningSphere : MonoBehaviour {

	public float angle;

	// Use this for initialization
	void Start () {
		applyRotation ();
	}
	
	// Update is called once per frame
	void Update () {
		
		angle += Time.deltaTime*20;
		if (angle >= 360) {
			angle-=360;
		}
		applyRotation();
	}

	void applyRotation()
	{
		float theta = Mathf.Deg2Rad * angle;
		
		float cs = Mathf.Cos(theta);
		float sn = Mathf.Sin(theta);

		float xN = cs - sn;
		float yN = sn + cs;

		transform.position = new Vector3 (xN*3, yN*3, 30);
	}
}
