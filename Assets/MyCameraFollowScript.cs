using UnityEngine;
using System.Collections;

public class MyCameraFollowScript : MonoBehaviour {

	public GameObject followToObj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position.Set (followToObj.transform.position.x, followToObj.transform.position.y, -10);
		transform.position = new Vector3(followToObj.transform.position.x, followToObj.transform.position.y, -10);
	}
}
