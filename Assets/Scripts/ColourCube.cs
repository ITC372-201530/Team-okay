using UnityEngine;
using System.Collections;
using System;

public class ColourCube : MonoBehaviour, IComparable<ColourCube> {

	public char colour = 'M';
	public static char[] colourChars = {'R','Y','G','C','B','M','K'};
	static int cubeCount = 0;
	private int zDepth;
	
	private bool player;
	private bool spawning;

	private GameObject childCube;
	public RuntimeAnimatorController RacRed, RacGreen, RacBlue, RacCyan, RacMagenta, RacYellow;

	public GameObject red;
	public GameObject green;
	public GameObject blue;
	public GameObject cyan;
	public GameObject magenta;
	public GameObject yellow;

	// Use this for initialization
	void Start () {
		//	spawning indicates whether or not the cube is rising
		spawning = true;
		//	this is the initial height that the cube rises from
		transform.position += new Vector3(0,-1,0);
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
//		if(colour == 'K')
//		{
//			if (UnityEngine.Random.value <= 0.001)
//			{
//				gameObject.layer = 0;
//			}
//			else
//			{
//				gameObject.layer = 15;
//			}
//		}
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

		if(childCube!=null)
		{
			Destroy (childCube.gameObject);
		}

		
		GameObject cubeType;
		RuntimeAnimatorController Rac;

		switch (colour)
		{
		case 'R':
			cubeType = red;
			Rac = RacRed;
			break;
		case 'G':
			cubeType = green;
			Rac = RacGreen;
			break;
		case 'B':
			cubeType = blue;
			Rac = RacBlue;
			break;
		case 'C':
			cubeType = cyan;
			Rac = RacCyan;
			break;
		case 'M':
			cubeType = magenta;
			Rac = RacMagenta;
			break;
		case 'Y':
			cubeType = yellow;
			Rac = RacYellow;
			break;
		default:
			return;
		}
		
		childCube = (GameObject) Instantiate(cubeType, transform.position, Quaternion.identity);
		childCube.transform.parent = transform;
		childCube.transform.localPosition = new Vector3(0, -0.5f, 0);
		//childCube.transform.localScale = Vector3.one * 4;
		//Animator animator = childCube.gameObject.GetComponent<Animator>();
		//animator.runtimeAnimatorController = Rac;
		//animator.speed = UnityEngine.Random.value / 4 + .75f;
		//childCube.animation.Play("Create");

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

	public char getColour()
	{
		return colour;
	}

}