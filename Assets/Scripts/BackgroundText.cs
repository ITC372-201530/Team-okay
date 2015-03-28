using UnityEngine;
using System.Collections;

public class BackgroundText : MonoBehaviour {

	private string words;
	public int maxLength;
	public float active;
	public int avgLineLength;

	// Use this for initialization
	void Start () {
		words = "";
		guiText.text = words;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		int rng = (int)(Random.value*(maxLength*2/active));
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
		guiText.text = words;
	}
}
