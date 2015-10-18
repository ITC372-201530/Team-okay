using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	//height is inhereted from initial game state y axis
	private float height;
	//playerX and playerZ representing the coordinates that
	// the player model is moving toward
	private int playerX, playerZ;
	//the maximum of each power bar
	public int powerLimit;
	//the current number of moves that can be made
	// with a guaranteed combo
	public int freeCombo;
	//the two models that are used to display the player
	public GameObject playerStandardModel, playerFreeComboModel;
	//used to check if a new power bar is filled each move
	private int lastPowerSoundPlayed;
	//the colour that was last moved to
	public char lastColour;
	//COMBO counter
	public int chain = 0;
	//power resource bars
	public int rPower;
	public int gPower;
	public int bPower;

	//the second part of the tutorial
	public int tutorial;

	//audio parts
	private AudioSource audioSource;
	public AudioClip chainBreak, move1, move2, move3, move4, move5, abilityOneReady, abilityTwoReady, abilityThreeReady;
	public AudioSource abilityAudioSource;

	// Use this for initialization
	void Start () 
	{
		height = transform.position.y;
		audioSource = GetComponent<AudioSource>();
		playerFreeComboModel.SetActive( false);
		playerStandardModel.SetActive( true);
	}

	public void addFreeCombo(int fc)
	{
		freeCombo = fc;
		//display the free combo model
		playerFreeComboModel.SetActive( true);
		playerStandardModel.SetActive( false);
	}

	//returns an int of how many power bars are full
	public int checkAbilityLevel(){
		int level = 0;
		if (rPower == powerLimit)
			level++;
		if (gPower == powerLimit)
			level++;
		if (bPower == powerLimit)
			level++;
		return level;
	}

	//sets all full power bars to 0
	public void subtractPowers()
	{
		if (rPower == powerLimit)
			rPower = 0;
		if (gPower == powerLimit)
			gPower = 0;
		if (bPower == powerLimit)
			bPower = 0;
		lastPowerSoundPlayed = 0;
	}

	// Update is called once per frame
	void Update ()
	{
		//the difference between the current position and the destination
		Vector3 positionDiff = new Vector3(playerX,height,playerZ) - transform.position;
		//how big is the difference
		float mag = positionDiff.magnitude;

		if(positionDiff.magnitude > .1f)
		{
			if(mag < 1)
			{
				//cap the speed of the player model
				// at 1 unit/second
				positionDiff = positionDiff.normalized;
			}
			//move the player an amount based on the distance from
			// the destination
			transform.position += (positionDiff) * Time.deltaTime * 2;
		}

		tutorial = checkAbilityLevel() + 4;
	}

	//check for combos, and increase ability powers
	public void colourChain( char newC )
	{
		//freeCombo gives free combos
		if (freeCombo > 0) {
			freeCombo--;
			//pretend the previous colour was the same
			// as the new colour
			lastColour = newC;
			//if we ran out of free combo, switch models
			if(freeCombo == 0)
			{
				playerFreeComboModel.SetActive( false);
				playerStandardModel.SetActive( true);
			}
		}

		if(lastColour != null)
		{
			switch(newC)
			{
				//Red combos with Red, Magenta and Yellow
			case 'R':
				if(lastColour == 'R' || lastColour == 'M' || lastColour == 'Y')
				{
					chain++;
					rPower+=chain*2;
				}
				else
				{
					chain=0;
				}
				break;
				//Green combos with Green, Cyan and Yellow
			case 'G':
				if(lastColour == 'G' || lastColour == 'C' || lastColour == 'Y')
				{
					chain++;
					gPower+=chain*2;
				}
				else
				{
					chain=0;
				}
				break;
				//Blue combos with Blue, Cyan and Magenta
			case 'B':
				if(lastColour == 'B' || lastColour == 'C' || lastColour == 'M')
				{
					chain++;
					bPower+=chain*2;
				}
				else
				{
					chain=0;
				}
				break;
				//Cyan combos with Cyan, Blue and Green
			case 'C':
				if(lastColour == 'C' || lastColour == 'B' || lastColour == 'G')
				{
					chain++;
					gPower+=chain;
					bPower+=chain;
				}
				else
				{
					chain=0;
				}
				break;
				//Magenta combos with Magenta, Blue and Red
			case 'M':
				if(lastColour == 'M' || lastColour == 'B' || lastColour == 'R')
				{
					chain++;
					rPower+=chain;
					bPower+=chain;
				}
				else
				{
					chain=0;
				}
				break;
				//Yellow combos with Yellow, Red and Green
			case 'Y':
				if(lastColour == 'Y' || lastColour == 'R' || lastColour == 'G')
				{
					chain++;
					rPower+=chain;
					gPower+=chain;
				}
				else
				{
					chain=0;
				}
				break;
				//Black don' combo wit' nuffin
			case 'K':
			{
				Application.LoadLevel ("GameOver");
			}
				break;

			}
		}
		//cap power amounts
		if (rPower > powerLimit) 
			rPower = powerLimit;
		if (gPower > powerLimit) 
			gPower = powerLimit;
		if (bPower > powerLimit) 
			bPower = powerLimit;
		//cap combo counter
		if(chain>10)
			chain = 10;
		//record new colour
		lastColour = newC;

		//play appropriate sound
		if(chain == 0)
		{
			audioSource.clip = chainBreak;
		}
		else if(chain <= 3)
		{
			audioSource.clip = move1;
		}
		else if(chain <= 5)
		{
			audioSource.clip = move2;
		}
		else if(chain <= 7)
		{
			audioSource.clip = move3;
		}
		else if(chain <= 9)
		{
			audioSource.clip = move4;
		}
		else
		{
			audioSource.clip = move5;
		}
		audioSource.Play();

		//if there is an ability resource full
		// that hadn't been before, play a sound for it
		if(lastPowerSoundPlayed < checkAbilityLevel())
		{
			switch(checkAbilityLevel())
			{
			case 1:
				abilityAudioSource.clip = abilityOneReady;
				break;
			case 2:
				abilityAudioSource.clip = abilityTwoReady;
				break;
			case 3:
				abilityAudioSource.clip = abilityThreeReady;
				break;
			}
			abilityAudioSource.Play ();
			lastPowerSoundPlayed = checkAbilityLevel();
		}

	}

	//V and H here are used instead of the axis names,
	// to separate mechanical location from visual
	public int getV()
	{
		return playerX;
	}

	public int getH()
	{
		return playerZ;
	}

	public void setV(int v)
	{
		playerX = v;
	}

	public void setH(int h)
	{
		playerZ = h;
	}
}
