using UnityEngine;
using System.Collections;

public class TextSymbolThing : MonoBehaviour {

	private string content;

	// Use this for initialization
	void Start () {
		content = guiText.text;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		char[] newString = content.ToCharArray();
		float rng = Random.value;
		if(rng>0.95f)
		{
			newString[Random.Range(0,newString.Length)]=(char)(Random.Range(95,127));

		}
		guiText.text = new string(newString);

	}
}
