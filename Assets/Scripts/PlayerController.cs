using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{

	private float height;
	private int playerX, playerZ;
	public int powerLimit;
	public int freeCombo;
	public Animation playerModel, dogModel;
	private Renderer playerRend, dogRend;
	private int lastPowerSoundPlayed;

	/*
	 * to whoever does colour chains:
	 * 	if you want, this var can be an int,
	 * 	and you can make use of ColourCube.colourChars
	 * 	to perhaps make the arithmetic easier.
	 */
	public char lastColour;
	public int chain = 0;
	public int rPower;
	public int gPower;
	public int bPower;

	public int tutorial;

	private AudioSource audioSource;
	public AudioClip chainBreak, move1, move2, move3, move4, move5, abilityOneReady, abilityTwoReady, abilityThreeReady;
	public AudioSource abilityAudioSource;


	public void addFreeCombo(int fc)
	{
		freeCombo = fc;
		dogRend.enabled = true;
		playerRend.enabled = false;
	}

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

	// Use this for initialization
	void Start () 
	{
		height = transform.position.y;
		//transform.position = new Vector3(playerX,height,playerZ);
		audioSource = GetComponent<AudioSource>();

		playerRend = playerModel.GetComponentsInChildren<Renderer>()[0];
		dogRend = dogModel.GetComponentsInChildren<Renderer>()[0];

		dogRend.enabled = false;
		playerRend.enabled = true;

	}

	// Update is called once per frame
	void Update ()
	{
		/*
		 * Because the PlayerController class does not hold
		 * 	the player's location on the grid, this class is
		 * 	doing little more than displaying that position
		 * 	(right now, maybe ability resources are here too)
		 */
		Vector3 positionDiff = new Vector3(playerX,height,playerZ) - transform.position;
		float mag = positionDiff.magnitude;
		if(positionDiff.magnitude > .1f)
		{
			if(mag < 1)
			{
				positionDiff = positionDiff.normalized;
			}
			transform.position += (positionDiff) * Time.deltaTime * 2;
			transform.rotation = Quaternion.LookRotation (positionDiff);

			dogModel.Play("Moving");
			playerModel.Play("Moving");

		}
		else
		{
				dogModel.Play("Idle");
				playerModel.Play("Idle");
		}

		tutorial = checkAbilityLevel() + 4;


	}

	public void colourChain( char newC )
	{
		if (freeCombo > 0) {
			freeCombo--;
			lastColour = newC;
			if(freeCombo == 0)
			{
				dogRend.enabled = false;
				playerRend.enabled = true;
			}
		}
		if(lastColour != null)
		{
			switch(newC)
			{
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
			case 'K':
			{
				Application.LoadLevel ("GameOver");
			}
				break;

			}

			//chain that shit
		}
		if (rPower > powerLimit) 
			rPower = powerLimit;
		if (gPower > powerLimit) 
			gPower = powerLimit;
		if (bPower > powerLimit) 
			bPower = powerLimit;
		if(chain>10)
			chain = 10;


		lastColour = newC;

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

	public int getH()
	{
		return playerX;
	}

	public int getV()
	{
		return playerZ;
	}

	public void setH(int h)
	{
		playerX = h;
	}

	public void setV(int v)
	{
		playerZ = v;
	}
}
