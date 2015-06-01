using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


//[ExecuteInEditMode]
public class ColourWheel : Graphic 
{
	public int colour;
	public PlayerController player;
	private Vector2 centerPivot;
	public float padding;
	public float spd;

	
	
	void Start()
	{
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
		//centerPivot = rectTransform.pivot;

		centerPivot = new Vector2(0.5f,0.5f);
		padding = 0.01f;
		rectTransform.localScale = new Vector3(0.9f,0.4f,1);
		rectTransform.offsetMax = Vector2.zero;
		rectTransform.offsetMin = Vector2.zero;
	}
	
	void Update()
	{
		Vector3 intendedPosition = getIntendedPosition();

		Vector2 intendedPivot = new Vector2(intendedPosition.x,intendedPosition.y);
		Vector2 pivotDiff = intendedPivot - rectTransform.pivot;

		float t = Time.deltaTime*spd;

		if(pivotDiff.magnitude>t)
		{
			rectTransform.pivot += (pivotDiff*t);
		}

		float intendedZ  = intendedPosition.z;
//		float zDiff = intendedZ - rectTransform.anchoredPosition3D.z;
//		if(zDiff > Time.deltaTime)
//		{
//			rectTransform.anchoredPosition3D += new Vector3(0,0,zDiff*Time.deltaTime);
//		}
		rectTransform.anchoredPosition3D = new Vector3(0,0,intendedZ);
	}

	
	private Vector3 getIntendedPosition()
	{
		char c = player.lastColour;
		if(player.lastColour == null)
		{
			return Vector3.one;
		}
		Vector2 intPos = Vector2.one;
		float intZ = 0;
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
		switch(place)
		{
		default:
		case 0:
			intPos = centerPivot;
			intZ = -25;
			break;
		case 1:
			intPos = centerPivot - new Vector2(0,rectTransform.localScale.y+padding);
			intZ = -15;
			break;
		case 5:
			intPos = centerPivot + new Vector2(0,rectTransform.localScale.y+padding);
			intZ = -5;
			break;

		case 2:
			intPos = centerPivot + new Vector2(-10,-(rectTransform.localScale.y+padding)*4);
			intZ = 200;
			break;
		case 3:
			intPos = centerPivot + new Vector2(-15,-(rectTransform.localScale.y+padding)*2);
			intZ = 400;
			break;
		case 4:
			intPos = centerPivot + new Vector2(-10,0);
			intZ = 300;
			break;
		}

		return new Vector3(intPos.x,intPos.y,intZ);
	}

	protected override void OnFillVBO (List<UIVertex> vbo)
	{
		Vector2 corner1 = Vector2.zero;
		Vector2 corner2 = Vector2.zero;
		
		corner1.x = 0f;
		corner1.y = 0f;
		corner2.x = 1f;
		corner2.y = 1f;
		
		corner1.x -= rectTransform.pivot.x;
		corner1.y -= rectTransform.pivot.y;
		corner2.x -= rectTransform.pivot.x;
		corner2.y -= rectTransform.pivot.y;
		
		corner1.x *= rectTransform.rect.width;
		corner1.y *= rectTransform.rect.height;
		corner2.x *= rectTransform.rect.width;
		corner2.y *= rectTransform.rect.height;
		
		vbo.Clear();
		
		UIVertex vert = UIVertex.simpleVert;
		
		vert.position = new Vector2(corner1.x, corner1.y);
		vert.color = color;
		vbo.Add(vert);
		
		vert.position = new Vector2(corner1.x, corner2.y);
		vert.color = color;
		vbo.Add(vert);
		
		vert.position = new Vector2(corner2.x, corner2.y);
		vert.color = color;
		vbo.Add(vert);
		
		vert.position = new Vector2(corner2.x, corner1.y);
		vert.color = color;
		vbo.Add(vert);
	}

	
}