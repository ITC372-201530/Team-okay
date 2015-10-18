using UnityEngine;
using System.Collections;

public class Initials : FormChangeClick {

	public bool isUppercase = true;
	public int charId = 0;
	public int cubeId;

	void Start()
	{
		string lastName = PlayerPrefs.GetString("LastName");
		if(lastName.Length>=3)
		{
			char letter = lastName[cubeId];
			if(letter <= 32)
			{
				charId = -1;
			}
			else if(letter <= 90)
			{
				charId = (int)letter-65;
				isUppercase = true;
			}
			else if(letter <= 122)
			{
				charId = (int)letter-97;
				isUppercase = false;
			}
		}
		clamp ();
	}

	//where another cube in these menus would change scene,
	// the initial cubes change the case of their character
	public override void OnMouseDown () {
		isUppercase = !isUppercase;
	}

	//adjusts the cube's character based on input direction
	public override void changeChar(int direction)
	{
		charId += direction;
		clamp ();
	}

	//returns character that cube represents
	public char getChar()
	{
		if(charId == -1)
		{
			return ' ';
		}
		int cId = charId + 65 + (isUppercase ? 0 : 32);
		return (char)cId;
	}

	//loops character selected
	private void clamp()
	{
		if(charId < -1)
		{
			charId = 25;
		}
		if(charId > 25)
		{
			charId = -1;
		}
	}

}
