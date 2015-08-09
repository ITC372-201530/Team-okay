using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundText : MonoBehaviour {

	private List<string> words;
	public int maxLength;
	public float active;
	public int avgLineLength;
	float updateTime;

	// Use this for initialization
	void Start () {
		words = new List<string>();
		guiText.text = allStrings();
		updateTime = 0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
/*		int rng = (int)(Random.value*(maxLength*2/active));
		if(words.Length >= maxLength) {
			words = "";
		}
		if (!(rng > maxLength*2)) {
			if (rng < words.Length) {
				words = words.Substring (0, words.Length - 2);
			}
			else {
				if (rng%avgLineLength == 0) {
					words+='\n';
				}
				else {
					words += (char)(rng%95+32);
				}
			}

		}
		*/
		
		if(updateTime<=0f)
		{
			updateTime += Random.value;
			while(words.Count < maxLength)
			{
				words.Add(newString());
			}
			words.RemoveAt(0);
			guiText.text = allStrings();
		}
		updateTime-=Time.deltaTime;
	}

	private string newString()
	{
		string newString = "";

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
		foreach (string s in words)
		{
			allStrings+=s;
		}
		return allStrings;
	}
}
