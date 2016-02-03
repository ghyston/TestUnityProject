using UnityEngine;
using System.Collections;

public class MeshBehaviourScript : MonoBehaviour {

	public Vector3[] newVerticles;
	public Vector3[] newNormals;
	public Vector2[] newUV;
	public int[] newTriangles;
	//public Color[] newColors;

	// Use this for initialization
	void Start () {
		Mesh mesh = new Mesh ();
		GetComponent<MeshFilter> ().mesh = mesh;
		mesh.vertices = newVerticles;
		mesh.uv = newUV;
		mesh.triangles = newTriangles;
	//	mesh.normals = newNormals;
	//	mesh.colors = newColors;
	}
	
	// Update is called once per frame
	void Update () {
		Mesh mesh = GetComponent<MeshFilter> ().mesh;
		Vector3[] verticles = mesh.vertices;
	//	Vector3[] normals = mesh.normals;
		int i = 0;
		while (i < verticles.Length) {
			verticles[i].x = newVerticles[i].x + Mathf.Sin(Time.time + verticles[i].y);
			verticles[i].y = newVerticles[i].y - Mathf.Cos(Time.time + verticles[i].x);
			i++;
		}

		//mesh.Clear ();
		mesh.vertices = verticles;
	}
}
