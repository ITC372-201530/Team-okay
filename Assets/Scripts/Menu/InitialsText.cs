using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InitialsText : MonoBehaviour {

	private Text txt;
	//the cube object that contains character to display
	public Initials source; 

	// Use this for initialization
	void Start () {
		txt = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		string display ="";
		//get cube's character
		display+= source.getChar();

		txt.text = display;
	}
}
