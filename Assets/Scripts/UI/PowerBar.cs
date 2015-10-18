using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


//[ExecuteInEditMode]
public class PowerBar : Graphic 
{

	public PowerBarPanel pbp;
	public int bar;
	public float length;

	void LateUpdate()
	{
		rectTransform.localScale = new Vector3(pbp.getLength(bar),rectTransform.localScale.y,1f);
		//get the intended colour based on player resources
		Color c = pbp.getColour(bar);

		//if there is no glow (power bar is not full)
		if(!pbp.isGlow(bar))
		{
			color = c;
		}
		else
		{
			//apply amount of glow
			float glow = pbp.getGlow();
			float[] v3 = new float[3];
			v3[0] = (1-c.r)*glow + c.r;
			v3[1] = (1-c.g)*glow + c.g;
			v3[2] = (1-c.b)*glow + c.b;
			color = new Color( v3[0],v3[1],v3[2] );
			glow = (glow+0.05f)%0.5f;
		}
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