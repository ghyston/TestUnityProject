using UnityEngine;
using System.Collections;

public class Pistol : BaseWeapon {
	
	public override void Shot(GameObject shooter)
	{
		if (!checkTiming ()) {
			return;
		}

		GameObject bullet = Instantiate (bulletType, shooter.transform.position, shooter.transform.rotation) as GameObject;
		Physics2D.IgnoreCollision(shooter.GetComponent<BoxCollider2D>(), bullet.GetComponent<BoxCollider2D>());
		lastShot = System.DateTime.Now;
	}

	public override string GetName()
	{
		return "Pistol";
	}
}
