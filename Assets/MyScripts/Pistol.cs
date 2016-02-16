using UnityEngine;
using System.Collections;

public class Pistol : BaseWeapon {
	
	public override bool Shot(GameObject shooter)
	{
		if (!checkTiming ()) {
			return false;
		}

		GameObject bullet = MonoBehaviour.Instantiate (bulletType, shooter.transform.position, shooter.transform.rotation) as GameObject;
		Physics2D.IgnoreCollision(shooter.GetComponent<BoxCollider2D>(), bullet.GetComponent<BoxCollider2D>());
		lastShot = System.DateTime.Now;
		return true;
	}

	public override string GetName()
	{
		return "Pistol";
	}
}
