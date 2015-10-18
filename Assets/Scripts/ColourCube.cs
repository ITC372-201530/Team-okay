using UnityEngine;
using System.Collections;
using System;

//This class represents one cube of a colour in the active game world
public class ColourCube : MonoBehaviour, IComparable<ColourCube> {
	//This array is used to convert between ints and chars for colour representation
	public static char[] colourChars = {'R','Y','G','C','B','M','K'};

	//These values make the array of cubes sortable, so their location in game
	// becomes predictable (they are stored in an ordered list, but this guarantees
	// newer cubes being at the end of the list)
	static int cubeCount = 0;
	private int zDepth;

	//This indicates which model to display and is used by the player
	// when they move to it to determine combo chain and power resources
	public char colour = 'M';

	//Indicates whether the player is on top of this cube
	private bool player;
	//Set to true if the cube is despawning
	private Boolean despawning = false;

	//The model of a cube, based on which colour this class is
	private GameObject childCube;

	//Models of appropriate colour, to be created when the colour is set
	public GameObject red;
	public GameObject green;
	public GameObject blue;
	public GameObject cyan;
	public GameObject magenta;
	public GameObject yellow;

	// Use this for initialization
	void Start () {

		//this is the initial height (y-axis) of the cube
		// height is the only position value that should be controlled in this class
		transform.position += new Vector3(0,-transform.localScale.y,0);

		//changes the cube to display its colour
		// (probably always magenta initially)
		changeColour ();

		//each cube stores in what order it was created,
		// so they may be sorted
		zDepth = cubeCount;
		cubeCount++;
	}
	
	// Update is called once per frame
	void Update () {

		float sizeY = -transform.localScale.y;
		//how much the cube moves is dependant on how long the
		// most recent update took
		float moveMag = 1f * Time.deltaTime;

		//correct Y based on presence or absence of player
		if (transform.position.y <= (sizeY/2) && player)
		{
			transform.position+= new Vector3(0,(moveMag/2),0);
		}
		else if (transform.position.y >= sizeY && !player)
		{
			transform.position+= new Vector3(0,-(moveMag/4),0);
		}

		//if the cube is despawning, and has finished that animation, remove the cube model
		if(childCube!=null)
		{
			if (!childCube.animation.IsPlaying("Take 0010") && despawning == true)
			{
				//'K' results in no new cube model replacing the old
				setColour('K');
			}
		}
	}

	public void setColour (int c)
	{
		//Pass in a value between 0-6 to set the
		// cube to the corresponding colour
		// (see private static char[] colourChars
		// at top of file)
		colour = colourChars [c];
		changeColour ();
	}

	public void beginDestructionAnimation()
	{
		//Play destruction animation, and indicate that the state is despawning
		if(childCube!=null)
		{
			childCube.animation.Play ("Take 0010");
		}
		despawning = true;
	}


	public void setColour (char c)
	{

		//Set the cube to become the colour
		// of the char c
		colour = c;
		changeColour ();
	}

	public void setPlayer (bool presence)
	{
		//presence represents whether the player is above
		// this cube or not
		player = presence;
	}

	void changeColour()
	{
		//translates the stored char for colour into
		// a model, which is attached to this cube

		if(childCube!=null)
		{
			//begin by removing the previous model
			Destroy (childCube.gameObject);
		}

		GameObject cubeType;

		switch (colour)
		{
		case 'R':
			cubeType = red;
			break;
		case 'G':
			cubeType = green;
			break;
		case 'B':
			cubeType = blue;
			break;
		case 'C':
			cubeType = cyan;
			break;
		case 'M':
			cubeType = magenta;
			break;
		case 'Y':
			cubeType = yellow;
			break;
		default:
			return;
		}

		//create an instance of the cube model
		childCube = (GameObject) Instantiate(cubeType, transform.position, Quaternion.identity);
		childCube.transform.parent = transform;
		
		//each cube has a random animation speed, so they don't animate in unison
		Animation anim = childCube.gameObject.GetComponent<Animation>();
		foreach (AnimationState state in anim) {
			state.speed = UnityEngine.Random.value / 3 + .75f;
		}

	}

	public int CompareTo(ColourCube other)
	{
		//CompareTo enables sorting
		// cubes are sorted based on how recently
		// they were created
		if(other == null)
		{
			return 1;
		}
		return (zDepth - other.zDepth);
	}

	public char getColour()
	{
		if(!despawning)
			return colour;
		else
			return 'K';
	}

}