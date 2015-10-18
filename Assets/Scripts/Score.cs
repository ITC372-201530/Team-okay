using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Score : MonoBehaviour {

	//like an array but without any of the sophistication
	private float highscore0, highscore1, highscore2, highscore3, highscore4, highscore5, highscore6, highscore7, highscore8, highscore9, highscore10;
	private string highscorename0, highscorename1, highscorename2, highscorename3, highscorename4, highscorename5, highscorename6, highscorename7, highscorename8, highscorename9, highscorename10;

	// Use this for initialization
	void Start () {
		//PScore is the most recent score achieved by the player,
		// and is saved to the playerPrefs file every 
		highscore0 = PlayerPrefs.GetFloat("PScore");
		guiText.text = highscore0.ToString("0.00");
	}

	public void submit (string name) {
		
		highscore0 = PlayerPrefs.GetFloat("PScore");

		//load high score list
		highscore1 = PlayerPrefs.GetFloat ("highscore1");
		highscorename1 = PlayerPrefs.GetString ("highscorename1");
		highscore2 = PlayerPrefs.GetFloat ("highscore2");
		highscorename2 = PlayerPrefs.GetString ("highscorename2");
		highscore3 = PlayerPrefs.GetFloat ("highscore3");
		highscorename3 = PlayerPrefs.GetString ("highscorename3");
		highscore4 = PlayerPrefs.GetFloat ("highscore4");
		highscorename4 = PlayerPrefs.GetString ("highscorename4");
		highscore5 = PlayerPrefs.GetFloat ("highscore5");
		highscorename5 = PlayerPrefs.GetString ("highscorename5");
		highscore6 = PlayerPrefs.GetFloat ("highscore6");
		highscorename6 = PlayerPrefs.GetString ("highscorename6");
		highscore7 = PlayerPrefs.GetFloat ("highscore7");
		highscorename7 = PlayerPrefs.GetString ("highscorename7");
		highscore8 = PlayerPrefs.GetFloat ("highscore8");
		highscorename8 = PlayerPrefs.GetString ("highscorename8");
		highscore9 = PlayerPrefs.GetFloat ("highscore9");
		highscorename9 = PlayerPrefs.GetString ("highscorename9");
		highscore10 = PlayerPrefs.GetFloat ("highscore10");
		highscorename10 = PlayerPrefs.GetString ("highscorename10");
		
		//add new entries (highscore0 and name) to existing list
		float[] a = new float[11]{highscore1, highscore2, highscore3, highscore4, highscore5, highscore6, highscore7, highscore8, highscore9, highscore10, highscore0 };
		string[] b = new string[11]{ highscorename1, highscorename2, highscorename3, highscorename4, highscorename5, highscorename6, highscorename7, highscorename8, highscorename9, highscorename10, name };
		//then sort list
		selectsort (a, b);

		for (int i=0; i<11; i++)
		{	
			print (i + " =" + b [i]);
		}

		//save the highest 10 scores and names
		PlayerPrefs.SetFloat ("highscore1", a[0]);
		PlayerPrefs.SetString ("highscorename1", b[0]);
		PlayerPrefs.SetFloat ("highscore2", a[1]);
		PlayerPrefs.SetString ("highscorename2", b[1]);
		PlayerPrefs.SetFloat ("highscore3", a[2]);
		PlayerPrefs.SetString ("highscorename3", b[2]);
		PlayerPrefs.SetFloat ("highscore4", a[3]);
		PlayerPrefs.SetString ("highscorename4", b[3]);
		PlayerPrefs.SetFloat ("highscore5", a[4]);
		PlayerPrefs.SetString ("highscorename5", b[4]);
		PlayerPrefs.SetFloat ("highscore6", a[5]);
		PlayerPrefs.SetString ("highscorename6", b[5]);
		PlayerPrefs.SetFloat ("highscore7", a[6]);
		PlayerPrefs.SetString ("highscorename7", b[6]);
		PlayerPrefs.SetFloat ("highscore8", a[7]);
		PlayerPrefs.SetString ("highscorename8", b[7]);
		PlayerPrefs.SetFloat ("highscore9", a[8]);
		PlayerPrefs.SetString ("highscorename9", b[8]);
		PlayerPrefs.SetFloat ("highscore10", a[9]);
		PlayerPrefs.SetString ("highscorename10", b[9]);

	}

	//sorts a float and string array based on the values
	// of the float array
	static void selectsort(float[] a,string[] b  ){
		int i, j, min;
		float temp;
		string tempH;
		for (i=0; i<11; i++) {
			min = i;
			Console.WriteLine("New Loop");
			for(j=i+1;j<11;j++)
				if(a[j]>a[min]) min = j;//find minimum
			temp = a[i];
			tempH = b[i];
			a[i]=a[min];
			b[i] = b[min];
			a[min]=temp;
			b[min]=tempH;
			
		}
	}

}
