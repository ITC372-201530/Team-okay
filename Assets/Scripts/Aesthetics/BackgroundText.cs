using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundText : MonoBehaviour {

	private List<string> lines;
	public int maxLength;
	public float active;
	public int avgLineLength;
	float updateTime;

	// Use this for initialization
	void Start () {
		lines = new List<string>();
		guiText.text = allStrings();
		updateTime = 0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//time to update
		if(updateTime<=0f)
		{
			//update again in some time
			updateTime += Random.value;
			//get the line count to its limit
			while(lines.Count < maxLength)
			{
				lines.Add(newString());
			}
			//then remove one, so next update adds one
			lines.RemoveAt(0);
			//display lines
			guiText.text = allStrings();
		}
		updateTime-=Time.deltaTime;
	}

	private string newString()
	{
		string newString = "";
		//add random characters to line
		int tabs = (int)(Mathf.Pow(Random.value,2)*3);
		int lineLength = (int)((1-Mathf.Pow(Random.value,2))*(80-(tabs*5)));
		for(int i = 0; i < tabs; i++)
		{
			newString+="\t";
		}
		for(int i = 0; i < lineLength; i++)
		{
			newString += (char)((int)(Random.value*95)+32);
		}
		newString += "\n";
		return newString;
	}

	private string allStrings()
	{
		string allStrings = "";
		foreach (string s in lines)
		{
			allStrings+=s;
		}
		return allStrings;
	}
}
