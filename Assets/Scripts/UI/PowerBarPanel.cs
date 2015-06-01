using UnityEngine;
using System.Collections;

public class PowerBarPanel : MonoBehaviour {

	public float glow;
	private Color[] colours;
	public PlayerController player;

	// Use this for initialization
	void Start () {
		glow = 0.2f;

		colours = new Color[7];
		colours[0] = new Color(.839f,.251f,0);
		colours[1] = new Color(.448f,.902f,.129f);
		colours[2] = new Color(0f,.565f,1f);
		colours[3] = new Color(0f,1f,.776f);
		colours[4] = new Color(.741f,0f,.839f);
		colours[5] = new Color(.941f,.906f,.039f);
		colours[6] = new Color(.7f,.7f,.7f);

	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		glow = (glow+0.05f)%0.5f;
	}

	public float getGlow ()
	{
		return glow;
	}

	public float getLength(int bar)
	{
		float l;
		switch(bar)
		{
		default:
			l = -1;
			break;
		case 1:
			l = player.rPower;
			break;
		case 2:
			l = player.gPower;
			break;
		case 3:
			l = player.bPower;
			break;
		}
		return l / (float)player.powerLimit;
	}

	public Color getColour(int bar)
	{
		if(colours==null || colours.Length < 1)
		{
			return Color.white;
		}
		switch(bar)
		{
		case 1:
			if(!getRed () || (!getGreen() && !getBlue ()))
			{
				return colours[0];
			}
			if(getGreen() && getBlue())
			{
				return colours[6];
			}
			if(getGreen())
			{
				return colours[5];
			}
			if(getBlue())
			{
				return colours[4];
			}
			break;
		case 2:
			if(!getGreen () || (!getRed() && !getBlue ()))
			{
				return colours[1];
			}
			if(getRed() && getBlue())
			{
				return colours[6];
			}
			if(getRed())
			{
				return colours[5];
			}
			if(getBlue())
			{
				return colours[3];
			}
			break;
		case 3:
			if(!getBlue () || (!getRed() && !getGreen ()))
			{
				return colours[2];
			}
			if(getRed() && getGreen())
			{
				return colours[6];
			}
			if(getRed())
			{
				return colours[4];
			}
			if(getGreen())
			{
				return colours[3];
			}
			break;
		}
		return colours[6];
	}

	public bool isGlow(int bar)
	{
		switch(bar)
		{
		default:
			return false;
		case 1:
			return getRed ();
		case 2:
			return getGreen ();
		case 3:
			return getBlue ();
		}
	}

	private bool getRed()
	{
		return player.rPower >= player.powerLimit;
	}

	private bool getGreen()
	{
		return player.gPower >= player.powerLimit;
	}

	private bool getBlue()
	{
		return player.bPower >= player.powerLimit;
	}
}
