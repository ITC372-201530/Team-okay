using UnityEngine;
using System.Collections;
using System;

public class TextureCubes : MonoBehaviour {
	private Texture red;
	private Texture cyan;
	private Texture blue;
	private Texture green;
	private Texture yellow;
	private Texture magenta;

	// Use this for initialization
	void Start () {
		red = (Texture)Resources.Load("Prefabs/Materials/RedUnityMap");
		cyan = (Texture)Resources.Load("Prefabs/Materials/CyanUnityMap");
		blue = (Texture)Resources.Load("Prefabs/Materials/BlueUnityMap");
		green = (Texture)Resources.Load("Prefabs/Materials/GreenUnityMap");
		yellow = (Texture)Resources.Load("Prefabs/Materials/YellowUnityMap");
		magenta = (Texture)Resources.Load("Prefabs/Materials/MagentaUnityMap");

		Material Diffuse = Resources.Load("Prefabs/Materials/cubediffuse", typeof(Material)) as Material;
		Material Diffuse2 = Resources.Load("Prefabs/Materials/RedDiffuse", typeof(Material)) as Material;
		Diffuse.mainTexture = yellow;

		GameObject cube01 = GameObject.Find ("Box001");
		//Debug.Log (cube01.renderer.material);
		//cube01.renderer.material = Diffuse2;


	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
