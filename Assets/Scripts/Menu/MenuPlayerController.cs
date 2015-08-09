using UnityEngine;
using System.Collections;

public class MenuPlayerController : MonoBehaviour {

	public GameObject menuOptionsParent;
	public Animation animation;
	private FormChangeClick[] menuOptions;
	private int toX, toY;
	bool inputted = false;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		menuOptions = menuOptionsParent.GetComponentsInChildren<FormChangeClick>();
		toX = (int)gameObject.transform.position.x;
		toY = (int)gameObject.transform.position.z;

		audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		checkInputs();
		Transform t = gameObject.transform;
		Vector3 difference = t.position - new Vector3(toX,t.position.y,toY);
		if((difference.magnitude > .05f))
		{
			t.position -= difference.normalized * difference.magnitude * Time.deltaTime * 3;
			animation.gameObject.transform.rotation = Quaternion.LookRotation (-difference);
			animation.Play("Moving");
		}
		else
		{
			animation.Play("Idle");
		}
	}

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

	private void checkInputs()
	{
		float inputV = Input.GetAxisRaw("Horizontal");
		float inputH = -Input.GetAxisRaw("Vertical");
		bool space = Input.GetButton ("Jump");
		if (space && !inputted) {
			inputted = true;
			FormChangeClick option = findOption(toX,toY);
			if(option!=null)
			{
				option.OnMouseDown();
				//option.position += new Vector3(0,1,0);
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
