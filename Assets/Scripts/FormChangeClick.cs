using UnityEngine;
using System.Collections;

public class FormChangeClick : MonoBehaviour {
	public string level;

	// Update is called once per frame
	void Update () {
	
	}

	public virtual void OnMouseDown () {
		if (level == "quit")
			Application.Quit ();
		else 
		Application.LoadLevel (level);
	}

	public virtual void changeChar(int direction) {}
}
