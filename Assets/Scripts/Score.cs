using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Score : MonoBehaviour {


	private float highscore0, highscore1, highscore2, highscore3, highscore4, highscore5, highscore6, highscore7, highscore8, highscore9, highscore10;
	private string highscorename0, highscorename1, highscorename2, highscorename3, highscorename4, highscorename5, highscorename6, highscorename7, highscorename8, highscorename9, highscorename10;
	// Use this for initialization
	void Start () {
	
		submit ("Samel");

		guiText.text = highscore0.ToString("0.00");



	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void submit (string name) {

		highscore0 = PlayerPrefs.GetFloat("PScore");
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
		

		float[] a = new float[11]{highscore1, highscore2, highscore3, highscore4, highscore5, highscore6, highscore7, highscore8, highscore9, highscore10, highscore0 };
		string[] b = new string[11]{ highscorename1, highscorename2, highscorename3, highscorename4, highscorename5, highscorename6, highscorename7, highscorename8, highscorename9, highscorename10, name };
		
		/**for (int i=1; i<11; i++) {
			string hs = "highscore"+i;
			string hsn = "highscorename"+i;
			a[i] = PlayerPrefs.GetFloat(hs);
			b[i] = PlayerPrefs.GetString(hsn);
		} **/
		
		//a [10] = highscore0;
		//b [10] = highscorename0;
		
		//for (int i=0; i<11; i++) print (b [i]);
		
		selectsort (a, b);

		for (int i=0; i<11; i++) print (i + " =" + b [i]);

		PlayerPrefs.SetFloat ("highscore1", a[0]);
		//print ("setting highscore1 to " + a[0] + "/n");
		//print ("setting highscorename1 to " + b[0] + "/n");
		PlayerPrefs.SetString ("highscorename1", b[0]);
		PlayerPrefs.SetFloat ("highscore2", a[1]);
		//print ("setting highscore2 to " + a[1] + "/n");
		//print ("setting highscorename2 to " + b[1] + "/n");
		PlayerPrefs.SetString ("highscorename2", b[1]);
		PlayerPrefs.SetFloat ("highscore3", a[2]);
		//print ("setting highscore3 to " + a[2] + "/n");
		//print ("setting highscorename3 to " + b[2] + "/n");
		PlayerPrefs.SetString ("highscorename3", b[2]);
		PlayerPrefs.SetFloat ("highscore4", a[3]);
		//print ("setting highscore4 to " + a[3] + "/n");
		//print ("setting highscorename4 to " + b[3] + "/n");
		PlayerPrefs.SetString ("highscorename4", b[3]);
		PlayerPrefs.SetFloat ("highscore5", a[4]);
		//print ("setting highscore5 to " + a[4] + "/n");
		//print ("setting highscorename5 to " + b[4] + "/n");
		PlayerPrefs.SetString ("highscorename5", b[4]);
		PlayerPrefs.SetFloat ("highscore6", a[5]);
		//print ("setting highscore6 to " + a[5] + "/n");
		//print ("setting highscorename6 to " + b[5] + "/n");
		PlayerPrefs.SetString ("highscorename6", b[5]);
		PlayerPrefs.SetFloat ("highscore7", a[6]);
		//print ("setting highscore7 to " + a[6] + "/n");
		//print ("setting highscorename7 to " + b[6] + "/n");
		PlayerPrefs.SetString ("highscorename7", b[6]);
		PlayerPrefs.SetFloat ("highscore8", a[7]);
		//print ("setting highscore8 to " + a[7] + "/n");
		//print ("setting highscorename8 to " + b[7] + "/n");
		PlayerPrefs.SetString ("highscorename8", b[7]);
		PlayerPrefs.SetFloat ("highscore9", a[8]);
		//print ("setting highscore9 to " + a[8] + "/n");
		//print ("setting highscorename9 to " + b[8] + "/n");
		PlayerPrefs.SetString ("highscorename9", b[8]);
		PlayerPrefs.SetFloat ("highscore10", a[9]);
		//print ("setting highscore10 to " + a[9] + "/n");
		//print ("setting highscorename10 to " + b[9] + "/n");
		PlayerPrefs.SetString ("highscorename10", b[9]);

	}

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
