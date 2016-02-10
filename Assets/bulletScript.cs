using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {

	public int speed = 6;

	// Use this for initialization
	void Start () {
		Vector2 rotationEuler = transform.rotation * (new Vector2(1, 0));
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (rotationEuler.x, rotationEuler.y) * speed;
		//Debug.Log ("rotation: " + rotationEuler.x + " " + rotationEuler.y);

		Destroy(gameObject, 5);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.name == "zombie") 
		{
			Destroy (coll.gameObject);
			Destroy (gameObject);

			GameObject corpse = coll.gameObject.GetComponent<zombiescript> ().corpseObject;
			Instantiate (corpse, coll.gameObject.transform.position, coll.gameObject.transform.rotation); 
		}
	}

}
