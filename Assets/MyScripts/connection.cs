using UnityEngine;
using System.Collections;

public class connection : MonoBehaviour {

	public GameObject obj1 = null;
	public GameObject obj2 = null;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () 
	{
		float distance = Vector3.Distance(obj2.transform.position, obj1.transform.position);
		transform.localScale = new Vector3(distance, 1.0f, 1.0f);

		float angle = Vector3.Angle(obj2.transform.position, obj1.transform.position);
		var rotationVector = transform.rotation.eulerAngles;
		rotationVector.z = angle;
		transform.rotation = Quaternion.Euler(rotationVector);

		float newX = (obj1.transform.position.x + obj2.transform.position.x) / 2;
		float newY = (obj2.transform.position.y + obj1.transform.position.y) / 2;
		transform.position = new Vector3(newX, newY, 0.0f);

	}
}
