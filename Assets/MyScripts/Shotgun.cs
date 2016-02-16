using UnityEngine;
using System.Collections;

public class Shotgun : BaseWeapon {

	public int bulletsPerShot = 5;
	public float angleRange = 20;

	public override bool Shot(GameObject shooter)
	{
		if (!checkTiming ()) {
			return false;
		}

		for (int i = 0; i < bulletsPerShot; i++) 
		{
			Vector3 angle = shooter.transform.rotation.eulerAngles;
			Vector3 rndVec = new Vector3 (0, 0, Random.Range (-angleRange, angleRange));

			GameObject bullet = MonoBehaviour.Instantiate (bulletType, shooter.transform.position, Quaternion.Euler(angle + rndVec)) as GameObject;
			Physics2D.IgnoreCollision (shooter.GetComponent<BoxCollider2D> (), bullet.GetComponent<BoxCollider2D> ());
			bullet.GetComponent<bulletScript> ().speed *= (Random.Range (8, 12) / 10.0f);
		}
		lastShot = System.DateTime.Now;
		return true;
	}

	public override string GetName()
	{
		return "Shotgun";
	}
}
