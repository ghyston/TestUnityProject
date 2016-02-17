using UnityEngine;
using System.Collections;

public class DragableAmmoShotgun : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.name != "Player")
			return;

		coll.GetComponent<Inventory> ().increasePocketsAmount (1);

		Destroy (gameObject);
	}
}
