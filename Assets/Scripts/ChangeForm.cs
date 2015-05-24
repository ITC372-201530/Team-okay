using UnityEngine;
using System.Collections;

public class ChangeForm : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadStage(string level)
	{
		Application.LoadLevel (level);
	}
}
