using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InitialsText : MonoBehaviour {

	private Text txt;
	public Initials source; 

	// Use this for initialization
	void Start () {
		txt = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		string display ="";
		display+= source.getChar();

		txt.text = display;
	}
}
