using UnityEngine;
using System.Collections;

public class Shotgun : BaseWeapon {

	public int bulletsPerShot = 5;
	public float angleRange = 20;

	public override void Shot(GameObject shooter)
	{
		Debug.Log ("Shotgun::Shot()");

		if (!checkTiming ()) {
			return;
		}

		for (int i = 0; i < bulletsPerShot; i++) 
		{
			Debug.Log ("create bullet #" + i);

			Vector3 angle = shooter.transform.rotation.eulerAngles;
			Vector3 rndVec = new Vector3 (0, 0, Random.Range (-angleRange, angleRange));

			Debug.Log ("angle: " + angle);
			Debug.Log ("rnd: " + rndVec);

			GameObject bullet = Instantiate (bulletType, shooter.transform.position, Quaternion.Euler(angle + rndVec)) as GameObject;
			Physics2D.IgnoreCollision (shooter.GetComponent<BoxCollider2D> (), bullet.GetComponent<BoxCollider2D> ());
			bullet.GetComponent<bulletScript> ().speed *= (Random.Range (8, 12) / 10.0f);
		}
		lastShot = System.DateTime.Now;
	}

	public override string GetName()
	{
		return "Shotgun";
	}
}
