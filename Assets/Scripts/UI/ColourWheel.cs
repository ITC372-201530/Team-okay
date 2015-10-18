using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


//[ExecuteInEditMode]
public class ColourWheel : Graphic 
{
	//center colour
	public int colour;
	//player object to get colour from
	public PlayerController player;
	//how many full rotations would happen in a second
	// if the rotation difference was 360
	public float spd;
	//
	public float adj = 1f;
	public float zRot = 0f;
	
	void Start()
	{
		//hell yeah hard coding colours
		switch(colour)
		{
		case 1:	//Red
		color = new Color(.839f,.251f,0);
			break;
		case 3:	//Green
			color = new Color(.448f,.902f,.129f);
			break;
		case 5:	//Blue
			color = new Color(0f,.565f,1f);
			break;
		case 4:	//Cyan
			color = new Color(0f,1f,.776f);
			break;
		case 6:	//Magenta
			color = new Color(.741f,0f,.839f);
			break;
		case 2:	//Yellow
			color = new Color(.941f,.906f,.039f);
			break;
		default:
			color = new Color(.7f,.7f,.7f);
			break;
		}

		rectTransform.localScale = new Vector3(1f,1f,1f);
		rectTransform.offsetMax = Vector2.zero;
		rectTransform.offsetMin = Vector2.zero;
	}
	
	void Update()
	{
		//rotate towards intended position
		float toZRot = getIntendedPosition();
		float currZRot = rectTransform.localRotation.eulerAngles.z;
		float zDiff = toZRot - currZRot;
		if(zDiff < -180)
		{
			zDiff+=360;
		}
		if(zDiff > 180)
		{
			zDiff-=360;
		}

		float t = Time.deltaTime*spd;	
		if(Mathf.Abs (zDiff)>t)
		{
			rectTransform.localRotation = Quaternion.Euler(0,0,zDiff*t+currZRot);
		}

	}

	
	private float getIntendedPosition()
	{
		char c = player.lastColour;
		if(player.lastColour == null)
		{
			return 0f;
		}
		float rot = 0;
		char[] cs = ColourCube.colourChars;


		int place = -1;
		for(int i = 0; i < cs.Length-1; i++)
		{
			if(cs[(colour-1+i)%(cs.Length-1)] == c)
			{
				place = i;
			}
		}
		if(place==0)
		rectTransform.SetAsLastSibling();
		//rotation representing colour wheel position
		switch(place)
		{
		default:
		case 0:
			//Same Colour
			rot = 0;
			break;
		case 1:
			//Above Friendly
			rot = 60;
			break;
		case 5:
			//Below
			rot = 300;
			break;
		case 2:
			//Above Enemy
			rot = 120;
			break;
		case 3:
			//Opposite
			rot = 180;
			break;
		case 4:
			//Below Enemy
			rot = 240;
			break;
		}

		return rot;
	}

	protected override void OnFillVBO (List<UIVertex> vbo)
	{
		Vector2 corner1 = Vector2.zero;
		Vector2 corner2 = Vector2.zero;
		
		corner1.x = 0f;
		corner1.y = 0f-adj;
		corner2.x = .866f;
		corner2.y = 1f+adj;
		
		corner1.x -= rectTransform.pivot.x;
		corner1.y -= rectTransform.pivot.y;
		corner2.x -= rectTransform.pivot.x;
		corner2.y -= rectTransform.pivot.y;
		
		corner1.x *= rectTransform.rect.width;
		corner1.y *= rectTransform.rect.height;
		corner2.x *= rectTransform.rect.width;
		corner2.y *= rectTransform.rect.height;

		float centerY = (corner1.y+corner2.y)/2;
		float farX = corner2.x * 1.1443375673f;

		vbo.Clear();

		UIVertex vert = UIVertex.simpleVert;
		
		vert.position = new Vector2(corner1.x, centerY);
		vert.color = color;
		vbo.Add(vert);
		
		vert.position = new Vector2(corner2.x, corner2.y);
		vert.color = color;
		vbo.Add(vert);

		vert.position = new Vector2(farX, centerY);
		vert.color = color;
		vbo.Add(vert);

		vert.position = new Vector2(corner2.x, corner1.y);
		vert.color = color;
		vbo.Add(vert);
	}

	
}