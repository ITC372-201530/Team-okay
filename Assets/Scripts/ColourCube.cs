using UnityEngine;
using System.Collections;
using System;

public class ColourCube : MonoBehaviour, IComparable<ColourCube> {

	public char colour = 'M';
	public bool player;
	public int zDepth;
	private bool spawning;
	private char[] colourChars = {'R','G','B','C','M','Y','K'};
	static int cubeCount = 0;

	// Use this for initialization
	void Start () {

		spawning = true;
		transform.position += new Vector3(0,-3,0);
		changeColour ();
		zDepth = cubeCount;
		cubeCount++;
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

		if(colour == 'K')
		{
			if (UnityEngine.Random.value <= 0.001)
			{
				gameObject.layer = 0;
			}
			else
			{
				gameObject.layer = 15;
			}
		}
	}

	public void setVar (int c)
	{
		colour = colourChars [c];
		changeColour ();
	}

	public void setVar (char c)
	{
		colour = c;
		changeColour ();
	}


	void changeColour()
	{
		switch (colour)
		{
		case 'R':
			gameObject.layer = 9;
			break;
		case 'G':
			gameObject.layer = 10;
			break;
		case 'B':
			gameObject.layer = 11;
			break;
		case 'C':
			gameObject.layer = 12;
			break;
		case 'M':
			gameObject.layer = 13;
			break;
		case 'Y':
			gameObject.layer = 14;
			break;
		case 'K':
		default:
			gameObject.layer = 15;
			break;
		}
	}

	public int CompareTo(ColourCube other)
	{
		if(other == null)
		{
			return 1;
		}

		return (zDepth - other.zDepth);
	}

}