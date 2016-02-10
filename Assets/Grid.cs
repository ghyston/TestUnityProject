using UnityEngine;
using System.Collections;

// Copypasted from here: http://answers.unity3d.com/questions/482128/draw-grid-lines-in-game-view.html

public class Grid : MonoBehaviour {

	public GameObject plane;

	public bool showMain = true;

	public int gridSizeX;
	public int gridSizeY;

	public float step;

	public float startX;
	public float startY;

	public Material lineMaterial;

	private Color mainColor = new Color(0.0f,0.9f, 0.9f,1f);

	void Start () 
	{
		//@todo: check gridSize should be positive!

	}

	void Update () 
	{
		
	}

	void CreateLineMaterial() 
	{

		if( !lineMaterial ) 
		{
			lineMaterial = new Material (Shader.Find("Diffuse"));
			lineMaterial.color = mainColor;
		}

	}

	void OnPostRender() 
	{   	

		CreateLineMaterial();
		// set the current material
		lineMaterial.SetPass( 0 );

		GL.Begin( GL.LINES );

		if(showMain)
		{
			GL.Color(mainColor);

			float xMax = startX + gridSizeX;
			float yMax = startY + gridSizeY;

			// Vertical
			for (float dx = startX; dx <= xMax; dx += step) 
			{
				GL.Vertex3( dx, startY, 0);
				GL.Vertex3( dx, yMax, 0);				
			}

			// Horizontal
			for (float dy = startY; dy <= yMax; dy += step) 
			{
				GL.Vertex3( startX, dy, 0);
				GL.Vertex3( xMax, dy, 0);				
			}
		}

		GL.End();
	}
}
