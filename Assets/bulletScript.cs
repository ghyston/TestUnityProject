using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {

	public float speed = 6;

	// Use this for initialization
	void Start () {
		Vector2 rotationEuler = transform.rotation * (new Vector2(1, 0));
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (rotationEuler.x, rotationEuler.y) * speed;
		//Debug.Log ("rotation: " + rotationEuler.x + " " + rotationEuler.y);

		Destroy(gameObject, 2);

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

			GameObject.Find ("DropingGiftsFactory").GetComponent<DropingGiftsScript> ().GenerateGift (coll.gameObject.transform.position);

			/*if (Random.value < 0.2) {
				float rnd = Random.value;
				GameObject gift;
				if (rnd < 0.33) 
				{
					gift = Resources.Load ("DragableHealth") as GameObject;
				} 
				else if (rnd > 0.66) 
				{
					gift = Resources.Load ("DragableAmmoPistol") as GameObject;
				}
				else 
				{
					gift = Resources.Load ("DragableAmmoShotgun") as GameObject;
				}
				Instantiate(gift, coll.gameObject.transform.position, Quaternion.identity);
			}*/

			GameObject corpse = coll.gameObject.GetComponent<zombiescript> ().corpseObject;
			Instantiate (corpse, coll.gameObject.transform.position, coll.gameObject.transform.rotation); 
			increaSeZombiesCounter ();
		}
	}

	void increaSeZombiesCounter()
	{
		((ZombieFabric)GameObject.Find ("ZombieFabric").GetComponent<ZombieFabric> ()).zombiesKilled++;
	}

}
