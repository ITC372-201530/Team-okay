using UnityEngine;
using System.Collections;
using System;

public class ColourCube : MonoBehaviour, IComparable<ColourCube> {

	public char colour = 'M';
	private static char[] colourChars = {'R','G','B','C','M','Y','K'};
	static int cubeCount = 0;
	private int zDepth;
	
	private bool player;
	private bool spawning;

	// Use this for initialization
	void Start () {
		//	spawning indicates whether or not the cube is rising
		spawning = true;
		//	this is the initial height that the cube rises from
		transform.position += new Vector3(0,-3,0);
		//	changes the cube to display its colour
		//		(probably always magenta initially)
		changeColour ();
		//	each cube stores in what order it was created,
		//		so they may be sorted
		zDepth = cubeCount;
		cubeCount++;
	}
	
	// Update is called once per frame
	void Update () {


		float sizeY = -transform.localScale.y;
		float moveMag = 1f * Time.deltaTime;

		//	the cube rises!
		if (transform.position.y < sizeY && spawning)
		{
			transform.position += new Vector3 (0, (moveMag), 0);
		}
		else
		{
			//	if the resting height has been reached,
			//		there will be no more rising
			spawning = false;
		}
		//	correct Y based on presence or absence of player
		if (transform.position.y <= (sizeY/2) && player) {
			transform.position+= new Vector3(0,(moveMag/2),0);
		}
		else if (transform.position.y >= sizeY && !player) {
			transform.position+= new Vector3(0,-(moveMag/4),0);
		}

		//	shitty flicker code for black cubes
		//	uses the "default" layer, same as player sphere
		//		therefore has the same lighting and cameras
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

	public void setColour (int c)
	{
		/*
		 * Pass in a value between 0-6 to set the
		 * 	cube to the corresponding colour
		 * 	(see private static char[] colourChars
		 * 	at top of file)
		 */
		colour = colourChars [c];
		changeColour ();
	}

	public void setColour (char c)
	{
		/*
		 * Set the cube to become the colour
		 * 	of the char c
		 */
		colour = c;
		changeColour ();
	}

	public void setPlayer (bool presence)
	{
		player = presence;
	}

	void changeColour()
	{
		/*
		 * translates the stored char for colour into
		 * 	a layer, so that the correct lights are
		 * 	applied to this cube
		 */
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
		/*
		 * CompareTo enables sorting
		 * 	cubes are sorted based on how recently
		 * 	they were created
		 */
		if(other == null)
		{
			return 1;
		}

		return (zDepth - other.zDepth);
	}

}