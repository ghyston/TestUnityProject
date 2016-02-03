using UnityEngine;
using System.Collections;


public class BaseTowerConnector : MonoBehaviour {

	public GameObject obj1;
	public GameObject obj2;
	public float radius1;
	public float radius2;

	// Use this for initialization
	void Start () 
	{
		Debug.Log("BaseTowerConnector::Start");

		Mesh mesh = new Mesh ();
		GetComponent<MeshFilter> ().mesh = mesh;
		UpdateVerticles ();

		int[] triangles = {
			0, 2, 1,
			1, 2, 3,
			1, 3, 5,
			4, 1, 5
		};
		mesh.triangles = triangles;

		Vector2[] uv = 
		{
			new Vector2 (0, 0),
			new Vector2 (1, 0),
			new Vector2 (0, 1),
			new Vector2 (1, 1),
			new Vector2 (0, 0),
			new Vector2 (0, 1)
		};

		mesh.uv = uv;
	}

	protected void UpdateVerticles()
	{
		Debug.Log("BaseTowerConnector::UpdateVerticles!");

		// Upd verticles array
		float distance = Vector3.Distance(obj2.transform.position, obj1.transform.position);
		Debug.Log ("obj1 pos: " + obj1.transform.position);
		Debug.Log ("obj2 pos: " + obj2.transform.position);


		Vector3[] verticles = new Vector3[6];
		verticles [0] = new Vector3 (0, -radius1, 0);
		verticles [1] = new Vector3 (distance / 2, -radius2, 0);
		verticles [2] = new Vector3 (0, radius1, 0);
		verticles [3] = new Vector3 (distance / 2, radius2, 0);
		verticles [4] = new Vector3 (distance, -radius1, 0);
		verticles [5] = new Vector3 (distance, radius1, 0);

		Mesh mesh = GetComponent<MeshFilter> ().mesh;
		//mesh.Clear ();
		mesh.vertices = verticles;

		// Update rotation
		Vector3 diff = obj2.transform.position - obj1.transform.position;
		float angle = Vector3.Angle(obj2.transform.position - obj1.transform.position, new Vector3(1, 0, 0));
		//Debug.Log ("Angel: " + angle + " diff: " + diff);
		if (diff.y < 0)
			angle *= -1;


		var rotationVector = transform.rotation.eulerAngles;
		rotationVector.z = angle;
		transform.rotation = Quaternion.Euler(rotationVector);

		// Set position to first object (@todo: do we need to change it?)
		transform.position = obj1.transform.position;
	}
}
