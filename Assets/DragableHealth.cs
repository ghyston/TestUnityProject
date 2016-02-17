using UnityEngine;
using System.Collections;

public class DragableHealth : MonoBehaviour {

	public int amount = 10;

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.name != "Player")
			return;

		Health hell = coll.GetComponent<Health> ();
		if (hell.isFull ())
			return;

		coll.GetComponent<Health> ().IncreaseHealth (amount);
		Destroy (gameObject);
	}
}
