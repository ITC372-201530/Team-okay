using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterCubeGrid : MonoBehaviour
{

	public int rows;
	public int cols;
	/*
	 * Due to the grid's orientation, referencing squares
	 * 	is done as such
	 *	
	 *	cubes[z][x] OR cubes[up&down][left&right]
	 *	sorry ~_~'
	 */
	List<ColourCube>[] cubes;

	public int playerX, playerZ;
	
	//In the editor, this value is what gameobject
	//	makes up the grid (it has to have a
	//	ColourCube component
	public ColourCube prefab;

	bool inputted = false;
	
	void Start()
	{
		/*
		 * An array of lists! What joy to attempt to maintain
		 */
		cubes = new List<ColourCube>[rows];

		for (int i = 0; i < rows; i++)
		{
			cubes[i] = new List<ColourCube>();
		}
		for(int i = 0; i < cols; i++)
		{
			addRow (false);
		}
		movePlayer (5,((int)(UnityEngine.Random.value*10000))%rows, false);
	}

	public void darken()
	{
		/*
		 * When the dark wall moves
		 * 		Remove left-most cubes
		 * 		New left-most cubes become black
		 * 		If player is on one of these cubes, lose state
		 * 		All cubes move one unit left
		 */
		foreach (List<ColourCube> cc in cubes)
		{
			cc.Sort();

			//cc[0].gameObject.SetActive(false);
			var des = cc[0];
			cc.RemoveAt(0);
			Destroy (des.gameObject);

			cc[0].setColour('K');


			foreach(ColourCube ccc in cc)
			{
				ccc.transform.position -= new Vector3(1,0,0);
			}

		}

		playerX--;
		if(playerX==0)
		{
			//Lose
		}

	}

	public void movePlayer(int newX, int newZ, bool trueMove)
	{
		/*
		 * newX and newZ are where the player's new location is
		 * trueMove is to designate whether or not to also
		 * 	increment the darkness and whatever else happens
		 * 	on a conventional move
		 */
		if(trueMove)
		{
			cubes[playerZ][playerX].setColour('K');
		}

		cubes[playerZ][playerX].setPlayer(false);
		playerX = newX;
		playerZ = newZ;
		cubes[playerZ][playerX].setPlayer(true);

		if(trueMove)
		{
			//check dark value first or whatever as well I guess
			darken ();
		}

		while(playerX+cols>=cubes[0].Count)
		{
			addRow (true);
		}
	}

	void addRow(bool apartheid)
	{
		/*
		 * adds a vertical line of cubes to the right-end
		 * 	of the grid
		 * if there is to be manipulation of the colour
		 * 	placement, it should be here
		 */
		int i = 0;
		foreach (List<ColourCube> cc in cubes)
		{
			Vector3 pos = new Vector3(cc.Count,0,i);
			cubes[i].Add((ColourCube) Instantiate(prefab, pos, Quaternion.identity));
			ColourCube newCube = (ColourCube) cubes[i][cc.Count-1];
			int cCount = 6;
			if(apartheid)
			{
				cCount++;
			}
			newCube.setColour(((int)(UnityEngine.Random.value*10000))%cCount);

			i++;
		}
	}

	void Update()
	{
		/*
		 * This just interprets input
		 * 	Can be improved
		 */
		float inputH = Input.GetAxisRaw("Horizontal");
		float inputV = Input.GetAxisRaw("Vertical");
		if(inputted)
		{
			if(inputH==0&&inputV==0)
			{
				inputted = false;
			}
		}
		else
		{
			if(inputH!=0)
			{
				int dir = 0;
				if(inputH>0)
				{
					dir = 1;
				}
				if(inputH<0)
				{
					dir = -1;
				}
				if(dir != 0)
				{
					movePlayer (playerX+dir,playerZ, true);
					inputted = true;
				}
			}
			else if(inputV!=0)
			{
				int dir = 0;
				if(inputV>0)
				{
					dir = 1;
				}
				if(inputV<0)
				{
					dir = -1;
				}
				if(playerZ+dir>=0 && playerZ+dir<rows)
				{
					if(dir != 0)
					{
						movePlayer (playerX,playerZ+dir,true);
						inputted = true;
					}
				}
			}
		}
	}

}
