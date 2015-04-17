using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{

	public float height;
	private int playerX, playerZ;
	public int powerLimit;
	public int freeCombo;

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
	}

	// Use this for initialization
	void Start () 
	{
		transform.position = new Vector3(playerX,height,playerZ);
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
		transform.position = new Vector3(playerX,height,playerZ);
	}

	public void colourChain( char newC )
	{
		if (freeCombo > 0) {
			freeCombo--;
			lastColour = newC;
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

			}

			//chain that shit
		}
		if (rPower > powerLimit) 
			rPower = powerLimit;
		if (gPower > powerLimit) 
			gPower = powerLimit;
		if (bPower > powerLimit) 
			bPower = powerLimit;


		lastColour = newC;
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
