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
	 *
	 */
	List<ColourCube>[] cubes;

	
	//In the editor, this value is what gameobject
	//	makes up the grid (it has to have a
	//	ColourCube component
	public ColourCube prefab;
	public PlayerController player;

	public int darknessCounter;
	public int darknessSpeed;

	bool inputted = false;

	public double score;
	public double scoreMultiplier;

	void Start()
	{
		/*
		 * An array of lists
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
		movePlayer (6,(((int)(UnityEngine.Random.value*10000))%(rows-2))+1, false);

		score = 0;
		scoreMultiplier = 1;

		darknessCounter = 1000;
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

		player.setH (player.getH ()-1);
		if(player.getH ()==0)
		{
			//Lose
		}

	}

	public void movePlayer(int newX, int newZ, bool trueMove)
	{
		darknessCounter -= darknessSpeed;
		if (darknessSpeed <= 800) 
			darknessSpeed+=4;

		/*
		 * newX and newZ are where the player's new location is
		 * trueMove is to designate whether or not to also
		 * 	increment the darkness and whatever else happens
		 * 	on a conventional move
		 */
		if(trueMove)
		{
			cubes[player.getV ()][player.getH ()].setColour('K');
		}

		cubes[player.getV()][player.getH ()].setPlayer(false);
		player.setH (newX);
		player.setV(newZ);
		cubes[player.getV ()][player.getH ()].setPlayer(true);

		if (trueMove) 
		{
			player.colourChain (cubes [player.getV ()] [player.getH ()].getColour ());


			//darknessSpeed increases the rate at which darknessCounter
			//	is decreased. When darknessCounter is below 0, then
			//	1000 is added to it and further movements continue to
			// 	decrease its value.
			while (darknessCounter < 0) 
			{
				darken ();
				darknessCounter += 1000;
			}
			score += 10 * scoreMultiplier;
		}
		while(player.getH ()+cols>=cubes[0].Count)
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
		 * Boolean represents whether or not black cubes are allowed
		 */
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

			//	cc[cc.Count-2].getColour() is the colour of the cube to the left
			//	cubes[i-1][cubes[i-1].Count-1].getColour() is the colour of the
			//		cube created before this one (obv error if i==0)
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
		bool space = Input.GetButton ("Jump");

		if (space) {
			switch (player.checkAbilityLevel()){
			case 0:
			default:
				break;
			case 1:
				Ability.levelOneAbility(this, player);
				break;
			case 2:
				Ability.levelTwoAbility(player);
				break;
			case 3:
				Ability.levelThreeAbility(this, player);
				break;
			}
		}
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
					if (player.getH() > 10)
					{
						darken();
					}
					movePlayer (player.getH ()+dir,player.getV(), true);
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
				if(player.getV ()+dir>=0 && player.getV ()+dir<rows)
				{
					if(dir != 0)
					{
						movePlayer (player.getH (),player.getV ()+dir,true);
						inputted = true;
					}
				}
			}
		}
	}

}
