using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CoolGuyController : MonoBehaviour {

	public float moveForce = 2;
	public float angleDelimiter = 0.2f; //running backwards, this character is $angleDelimiter slower
	BaseWeapon weapon = null;

	// Use this for initialization
	void Start () 
	{
		Inventory inv = GetComponent<Inventory> ();
		inv.Init ();
		SwitchWeapon ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 moveVec = new Vector3 (CrossPlatformInputManager.GetAxis ("Horizontal"), CrossPlatformInputManager.GetAxis ("Vertical"), 0);
		Vector3 lookVec = transform.rotation * Vector3.right;
		float moveLookAngle = Vector3.Angle (moveVec, lookVec);
		float limit = 1 - angleDelimiter * (moveLookAngle / 180.0f);

		moveVec.Normalize ();
		moveVec *= moveForce * Time.fixedDeltaTime * limit;
		transform.position += moveVec; //@todo: Rigidbody.MovePosition?

		Vector2 angleVec = new Vector2 (CrossPlatformInputManager.GetAxis ("Horizontal1"), CrossPlatformInputManager.GetAxis ("Vertical1"));
		float mg = angleVec.magnitude;
		if (mg == 0)
			return;
		
		float angle = Vector3.Angle(angleVec, Vector3.right);
		if (angleVec.y < 0) angle *= -1;
		var rotationVector = transform.rotation.eulerAngles;
		rotationVector.z = angle;
		transform.rotation = Quaternion.Euler(rotationVector);

		if (mg > 0.99) 
		{
			DoShot ();
		}
	}

	void DoShot()
	{
		if (weapon == null)
			return;

		Inventory inv = GetComponent<Inventory> ();
		int magazine = inv.getCurrentMagazineAmount ();

		if (inv.getCurrentMagazineAmount () == 0) {
			Debug.Log ("Need to reload!");
			return;
		}

		if(weapon.Shot (this.gameObject))
			inv.decreadeCurrentMagazineAmount (1); //@todo: it should depend on weapon and bullet type.. Who cares?
	}

	void SwitchWeapon()
	{
		Inventory inv = GetComponent<Inventory> ();
		this.weapon = inv.nextWeapon ();
		Debug.Log ("Weapon set to " + weapon.GetName ());
	}

	void Reload()
	{
		GetComponent<Inventory> ().Reload ();
	}
}