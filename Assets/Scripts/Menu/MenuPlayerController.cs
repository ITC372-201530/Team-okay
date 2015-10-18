using UnityEngine;
using System.Collections;

public class MenuPlayerController : MonoBehaviour {

	//parent of clickable objects
	public GameObject menuOptionsParent;
	//list of clickable objects
	private FormChangeClick[] menuOptions;
	//object destination and interaction location
	public int toX, toY;
	//is there an input currently pressed
	bool inputted = false;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		//gathers list of clickable options
		menuOptions = menuOptionsParent.GetComponentsInChildren<FormChangeClick>();

		toX = (int)gameObject.transform.position.x;
		toY = (int)gameObject.transform.position.z;

		audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		checkInputs();

		//move object towards destination
		Transform t = gameObject.transform;
		Vector3 difference = t.position - new Vector3(toX,t.position.y,toY);
		if((difference.magnitude > .05f))
		{
			t.position -= difference.normalized * difference.magnitude * Time.deltaTime * 3;
		}
	}

	//checks for a clickable option in list with
	// given coordinates
	private FormChangeClick findOption(int x, int y)
	{
		foreach (FormChangeClick fcc in menuOptions)
		{
			Transform t = fcc.GetComponent<Transform>();
			if(t.position.x==x&&t.position.z==y)
			{
				return fcc;
			}
		}
		return null;
	}

	//processes inputs
	private void checkInputs()
	{
		//an axis is between -1.0f and 1.0f
		float inputV = Input.GetAxisRaw("Vertical");
		float inputH = Input.GetAxisRaw("Horizontal");
		bool space = Input.GetButton ("Ability");
		if (space && !inputted) {
			inputted = true;
			FormChangeClick option = findOption(toX,toY);
			if(option!=null)
			{
				//simulates clicking on the selected option
				option.OnMouseDown();
			}
		}
		else if (inputH!=0 && !inputted)
		{
			inputted = true;
			int dir = 0;
			if(inputH>0)
			{
				dir = 1;
			}
			if(inputH<0)
			{
				dir = -1;
			}
			if(dir != 0)
			{
				if (findOption(toX+dir,toY)!=null)
				{
					toX+=dir;
					audioSource.Play();
				}
			}
		}
		else if (inputV!=0 && !inputted)
		{
			inputted = true;
			int dir = 0;
			if(inputV>0)
			{
				dir = 1;
			}
			if(inputV<0)
			{
				dir = -1;
			}
			if(dir != 0)
			{
				if (findOption(toX,toY+dir)!=null)
				{
					toY+=dir;
					audioSource.Play();
				}

					

			}
		}

		if(inputted)
		{
			if(inputH==0&&inputV==0&&!space)
			{
				inputted = false;
			}
		}
	}
}
