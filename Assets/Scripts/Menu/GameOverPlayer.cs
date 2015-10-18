using UnityEngine;
using System.Collections;

public class GameOverPlayer : MonoBehaviour
{
	
	public GameObject menuOptionsParent;
	//public Animation animation;
	private FormChangeClick[] menuOptions;
	public int toX, toY;
	bool inputted = false;
	private AudioSource audioSource;
	
	// Use this for initialization
	void Start ()
	{
		Search();
		toX = (int)gameObject.transform.position.x;
		toY = (int)gameObject.transform.position.z;
		
		audioSource = gameObject.GetComponent<AudioSource> ();
	}

	public void Search()
	{
		menuOptions = menuOptionsParent.GetComponentsInChildren<FormChangeClick> ();
	}

	// Update is called once per frame
	void Update ()
	{
		checkInputs ();
		Transform t = gameObject.transform;
		Vector3 difference = t.position - new Vector3 (toX, t.position.y, toY);
		if ((difference.magnitude > .05f))
		{
			t.position -= difference.normalized * difference.magnitude * Time.deltaTime * 3;
			//animation.gameObject.transform.rotation = Quaternion.LookRotation (-difference);
			//animation.Play ("Moving");
		} else
		{
			//animation.Play ("Idle");
		}
	}
	
	private FormChangeClick findOption (int x, int y)
	{
		foreach (FormChangeClick fcc in menuOptions)
		{
			Transform t = fcc.GetComponent<Transform> ();
			if (t.position.x == x && t.position.z == y)
			{
				return fcc;
			}
		}
		return null;
	}
	
	private void checkInputs ()
	{
		//an axis is between -1.0f and 1.0f
		float inputV = Input.GetAxisRaw("Vertical");
		float inputH = Input.GetAxisRaw("Horizontal");
		bool space = Input.GetButton ("Ability");
		if (space && !inputted)
		{
			inputted = true;
			FormChangeClick option = findOption (toX, toY);
			if (option != null)
			{
				option.OnMouseDown ();
				//option.position += new Vector3(0,1,0);
			}
		} else if (inputH != 0 && !inputted)
		{
			inputted = true;
			int dir = 0;
			if (inputH > 0)
			{
				dir = 1;
			}
			if (inputH < 0)
			{
				dir = -1;
			}
			if (dir != 0)
			{
				if (findOption (toX + dir, toY) != null)
				{
					toX += dir;
					audioSource.Play ();
				}
			}
		} else if (inputV != 0 && !inputted)
		{
			inputted = true;
			int dir = 0;
			if (inputV > 0)
			{
				dir = 1;
			}
			if (inputV < 0)
			{
				dir = -1;
			}
			if (dir != 0)
			{

				FormChangeClick option = findOption (toX, toY);
				if (option != null)
				{

					option.changeChar (dir);
				}
			}
		}
		
		if (inputted)
		{
			if (inputH == 0 && inputV == 0 && !space)
			{
				inputted = false;
			}
		}
	}
}
