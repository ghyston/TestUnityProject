using UnityEngine;
using System.Collections;

public class DropingGiftsScript : MonoBehaviour {

	public float dropChance = 0.3f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GenerateGift(Vector3 pos)
	{
		if (Random.value > dropChance)
			return;
		

		float rnd = Random.value;
		GameObject gift;
		if (rnd < 0.33) {
			gift = Resources.Load ("DragableHealth") as GameObject;
		} else if (rnd > 0.66) {
			gift = Resources.Load ("DragableAmmoPistol") as GameObject;
		} else {
			gift = Resources.Load ("DragableAmmoShotgun") as GameObject;
		}
		Instantiate (gift, pos, Quaternion.identity);

	}
}
