using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterCubeGrid : MonoBehaviour
{

	public int rows;
	public int cols;
	List<ColourCube>[] cubes;

	public int playerX, playerZ;

	public ColourCube prefab;

	bool inputted = false;
	
	void Start()
	{
		cubes = new List<ColourCube>[rows];

		for (int i = 0; i < rows; i++)
		{
			cubes[i] = new List<ColourCube>();
		}
		for(int i = 0; i < cols; i++)
		{
			addRow ();
		}
		movePlayer (5,0);
	}

	public void darken()
	{
		foreach (List<ColourCube> cc in cubes)
		{
			cc.Sort();
			cc[0].gameObject.SetActive(false);
			cc.RemoveAt(0);
			cc[0].setVar('K');
			foreach(ColourCube ccc in cc)
			{
				ccc.transform.position -= new Vector3(1,0,0);
			}
			playerX--;
			if(playerX==0)
			{
				//Lose
			}
		}

	}

	public void movePlayer(int newX, int newZ)
	{
		cubes[playerZ][playerX].player = false;
		playerX = newX;
		playerZ = newZ;
		cubes[playerZ][playerX].player = true;
		while(playerX+cols>=cubes[0].Count)
		{
			addRow ();
		}
	}

	void addRow()
	{
		int i = 0;
		foreach (List<ColourCube> cc in cubes)
		{
			Vector3 pos = new Vector3(cc.Count,0,i);
			cubes[i].Add((ColourCube) Instantiate(prefab, pos, Quaternion.identity));
			ColourCube newCube = (ColourCube) cubes[i][cc.Count-1];
			newCube.setVar(((int)(UnityEngine.Random.value*10000))%7);

			i++;
		}
	}

	void Update()
	{
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
				if(cubes[playerZ][playerX+dir].colour != 'K' && dir != 0)
				{
					cubes[playerZ][playerX].setVar('K');
					movePlayer (playerX+dir,playerZ);
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
					if(cubes[playerZ+dir][playerX].colour != 'K' && dir != 0)
					{
						cubes[playerZ][playerX].setVar('K');
						movePlayer (playerX,playerZ+dir);
						inputted = true;
					}
				}
			}
		}
	}

}
