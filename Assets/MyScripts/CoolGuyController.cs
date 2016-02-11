using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CoolGuyController : MonoBehaviour {

	public float moveForce = 2;
	BaseWeapon weapon = null;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 moveVec = new Vector3 (CrossPlatformInputManager.GetAxis ("Horizontal"), CrossPlatformInputManager.GetAxis ("Vertical"), 0);
		moveVec.Normalize ();
		moveVec *= moveForce * Time.fixedDeltaTime;
		transform.position += moveVec; //@todo: Rigidbody.MovePosition?

		Vector2 angleVec = new Vector2 (CrossPlatformInputManager.GetAxis ("Horizontal1"), CrossPlatformInputManager.GetAxis ("Vertical1"));
		float mg = angleVec.magnitude;
		if (mg == 0)
			return;
		
		float angle = Vector3.Angle(angleVec, new Vector3(1, 0, 0));
		if (angleVec.y < 0) angle *= -1;
		var rotationVector = transform.rotation.eulerAngles;
		rotationVector.z = angle;
		transform.rotation = Quaternion.Euler(rotationVector);

		if (mg > 0.99) 
		{
			weapon.Shot (this.gameObject);
			/*if (weapon != null) {
				
			} 
			else 
			{
				Debug.Log ("No weapon!" + weapon.GetName());	
			}*/
		}
	}

	void SwitchWeapon()
	{
		Debug.Log ("Plyer::SwitchWeapon()");
		Inventory inv = GetComponent<Inventory> ();
		this.weapon = inv.nextWeapon ();
		Debug.Log ("set to " + weapon.GetName ());
	}
}