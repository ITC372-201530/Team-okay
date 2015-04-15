using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float height;
	private int playerX, playerZ;

	/*
	 * to whoever does colour chains:
	 * 	if you want, this var can be an int,
	 * 	and you can make use of ColourCube.colourChars
	 * 	to perhaps make the arithmetic easier.
	 */
	public char lastColour;

	// Use this for initialization
	void Start () {
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
		if(lastColour != null)
		{
			//chain that shit
		}
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
