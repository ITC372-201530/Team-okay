using UnityEngine;
using System.Collections;

public class FormChangeClick : MonoBehaviour {
	public string level;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnMouseDown () {
		if (level == "quit")
			Application.Quit ();
		else 
		Application.LoadLevel (level);
	}
}
