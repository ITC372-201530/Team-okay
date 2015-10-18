using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MasterCubeGrid : MonoBehaviour
{
	//how wide is the game area
	public int cols;
	//how far ahead of the player do new cubes spawn
	public int rows;

	//Due to the grid's orientation, referencing squares
	// is done as such
	// cubes[column#][row#] OR cubes[left&right][up&down]
	List<ColourCube>[] cubes;
	
	//In the editor, this value is what gameobject
	//	makes up the grid (it has to have a
	//	ColourCube component)
	public ColourCube prefab;

	//the player character
	public PlayerController player;

	//which 'page' of the tutorial should be displayed
	public int tutorial;

	//these values control the cubes being removed at the bottom of the grid
	// how long until more cubes are removed
	public int darknessCounter;
	// how quickly the counter approaches a removal
	public int darknessSpeed;

	//only one input can be handled at a time
	// if there is no current input, this is false
	bool inputted = false;

	//player score and score multiplier
	public float score;
	public float scoreMultiplier;

	//sound player and files
	private AudioSource audioSource;
	public AudioClip darkenSound, abilityOneUse, abilityOneDeny,
	abilityTwoUse, abilityThreeUse;
	
	void Start()
	{
		//An array of lists
		cubes = new List<ColourCube>[cols];
		for (int i = 0; i < cols; i++)
		{
			cubes[i] = new List<ColourCube>();
		}

		for(int i = 0; i < rows*2; i++)
		{
			//initialise the list with several rows
			// 'false' because the list is initialised without black spaces
			addRow (false);
		}

		//the player begins at row 6, in the center
		movePlayer (6,cols/2, false);
		player.lastColour = cubes[player.getH()][player.getV()].colour;

		score = 0;
		scoreMultiplier = 1;

		darknessCounter = 1000;

		tutorial = 1;

		audioSource = GetComponent<AudioSource>();
	}

	//removes the bottom row of cubes
	public void darken()
	{
		//When the dark wall moves
		// Remove bottom-most cubes
		// New bottom-most cubes become black
		// If player is on one of these cubes, lose state
		// All cubes move one unit down

		foreach (List<ColourCube> cc in cubes)
		{
			//cubes are sorted by the order in which they were created
			cc.Sort();

			//destroy the first cube in the list (the oldest)
			var des = cc[0];
			cc.RemoveAt(0);
			Destroy (des.gameObject);

			//there is an invisible last row
			// play its destruction animation
			cc[0].beginDestructionAnimation();

			foreach(ColourCube ccc in cc)
			{
				//move each cube towards the base of the game
				ccc.transform.position -= new Vector3(1,0,0);
			}

		}
		//move the player towards the base of the game
		player.setV (player.getV ()-1);
		player.transform.position += new Vector3(-1,0,0);

		if(player.getV ()==0)
		{
			//if the player is on the first row, they have lost
			Application.LoadLevel("GameOver");
		}

		//play the darkness sound
		audioSource.clip = darkenSound;
		audioSource.Play();

	}

	//used to change the player character's destination,
	// not to change their current position
	public void movePlayer(int newX, int newZ, bool trueMove)
	{
		//tutorial page changes on a move
		if (tutorial < 4)
			tutorial++;

		//newX and newZ are where the player's new location is
		// trueMove is to designate whether or not to also
		// increment the darkness and whatever else happens
		// on a conventional move
		if(trueMove)
		{
			//play the destruction animation of the cube we are moving from
			cubes[player.getH ()][player.getV ()].beginDestructionAnimation ();
		}

		//the cube being left no longer holds the player
		cubes[player.getH()][player.getV ()].setPlayer(false);

		//the player destination is moved in the new direction
		player.setV(newX);
		player.setH(newZ);

		//the cube of the player destination now holds the player
		cubes[player.getH ()][player.getV ()].setPlayer(true);

		if (trueMove) 
		{
			//check if the player gets to COMBO
			player.colourChain (cubes [player.getH ()] [player.getV ()].getColour ());

			//if they broke their combo, the bottom row of cubes is removed
			if (player.chain == 0)
			{
				//1000 is the value required to trigger this
				darknessCounter -= 1000;
			}

			//decrement the timer until the bottom row is removed 'naturally'
			darknessCounter -= darknessSpeed;

			if (darknessSpeed <= 800) 
			{
				//increase the speed of darkness occurence
				// capped at 4 in 5 moves
				darknessSpeed += 3;
			}

			//darknessSpeed increases the rate at which darknessCounter
			//	is decreased. When darknessCounter is below 0, then
			//	1000 is added to it and further movements continue to
			// 	decrease its value.
			while (darknessCounter < 0) 
			{
				darken ();
				darknessCounter += 1000;
			}

			//every successful move (one where the player doesn't lose)
			// increase the score
            PlayerPrefs.SetFloat("PScore", (score / 10));
			score += 10 * scoreMultiplier;
		}


		while(player.getV ()+rows>=cubes[0].Count)
		{
			//create rows ahead of the player if there aren't enough
			addRow (true);
		}
	
	}

	void addRow(bool apartheid)
	{
		//adds a vertical line of cubes to the top-end
		// of the grid
		// if there is to be manipulation of the colour
		// placement, it should be here
		// Boolean represents whether or not black cubes are allowed

		int i = 0;
		foreach (List<ColourCube> cc in cubes)
		{
			// The 3d position for the new cube
			Vector3 pos = new Vector3(cc.Count,0,i);

			// This creates a new cube and
			//		1) adds it to the grid
			//		2) stores it as newCube;
			cc.Add((ColourCube) Instantiate(prefab, pos, Quaternion.identity));
			ColourCube newCube = (ColourCube) cc[cc.Count-1];
			newCube.transform.parent = transform;
			//	cc[cc.Count-2].getColour() is the colour of the cube to the left
			//	cubes[i-1][cubes[i-1].Count-1].getColour() is the colour of the
			//		cube created before this one (obv error if i==0)
		
			char lastColour;
			int cCount = 6;
			if(apartheid)
			{
				//logic for manipulating black cubes
				cCount++;
				if (i != 0)
				{ 
					lastColour = cubes[i-1][cubes[i-1].Count-1].getColour();

					if (lastColour.Equals('K'))
					{

						cCount--;
					}
				}
			}
			//set the colour of the new cube to be a random colour
			newCube.setColour(((int)(UnityEngine.Random.value*10000))%cCount);

			i++;
		}
	}

	//interprets input
	void Update()
	{
		//an axis is between -1.0f and 1.0f
		float inputV = Input.GetAxisRaw("Vertical");
		float inputH = Input.GetAxisRaw("Horizontal");
		bool space = Input.GetButton ("Ability");

		//space activates abilities
		if (space && !inputted) {
			inputted = true;
			switch (player.checkAbilityLevel()){
				//this decides which ability is used,
				// determined by the player's power bars
				// then that ability is activated and
				// the appropriate sound plays
			case 0:
			default:
				break;
			case 1:
				if(player.chain < 5)
				{
					Ability.levelOneAbility(this, player);
					audioSource.clip = abilityOneUse;
				}
				else
				{
					audioSource.clip = abilityOneDeny;
				}
				audioSource.Play ();
				break;
			case 2:
				Ability.levelTwoAbility(player);
				audioSource.clip = abilityTwoUse;
				audioSource.Play ();
				break;
			case 3:
				Ability.levelThreeAbility(this, player);
				audioSource.clip = abilityThreeUse;
				audioSource.Play ();
				break;
			}
		}

		//inputted is true if an input is currently being handled
		// therefore if it is false, a new input can be registered
		if(inputted)
		{
			if(inputV==0&&inputH==0&&!space)
			{
				inputted = false;
			}
		}
		else
		{
			if(inputV!=0)
			{
				//the input is to move up or down
				int dir = 0;
				if(inputV>0)
				{
					//the input is to move up
					dir = 1;
				}
				if(inputV<0)
				{
					//the input is to move down
					dir = -1;
				}
				if(dir != 0)
				{
					if (player.getV() > 10)
					{
						//the bottom of the grid can only fall so far
						// behind the player
						// we're cheap rubberbanding assholes
						darken();
					}
					movePlayer (player.getV ()+dir,player.getH(), true);
					inputted = true;
				}
			}
			else if(inputH!=0)
			{
				//the input is to move right or left
				int dir = 0;
				if(inputH>0)
				{
					//the input is to move left
					dir = 1;
				}
				if(inputH<0)
				{
					//the input is to move right
					dir = -1;
				}
				if(player.getH ()+dir>=0 && player.getH ()+dir<cols)
				{
					if(dir != 0)
					{
						movePlayer (player.getV (),player.getH ()+dir,true);
						inputted = true;
					}
				}
			}
		}
	}

}
