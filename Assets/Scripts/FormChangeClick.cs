using UnityEngine;
using System.Collections;

public class FormChangeClick : MonoBehaviour {
	//The scene to change to when this object is interacted with
	public string level;

	public virtual void OnMouseDown () {
		//OnMouseDown is called when this object's collider is clicked on
		if (level == "")
		{
			//some cubes have no attached level
			return;
		}
		else if (level == "quit")
		{
			//Quit the game
			Application.Quit ();
		}
		else 
		{
			//Change the game to be the scene of the same name as the level string
			Application.LoadLevel (level);
		}
	}

	//Used in subclass Initials
	public virtual void changeChar(int direction) {}
}
