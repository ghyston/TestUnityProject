using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CoolGuyController : MonoBehaviour {

	Rigidbody2D myBody;
	System.DateTime lastShot;

	public float moveForce = 2;
	public double timeBetweenBullets = 0.5d;
	public GameObject bulletType;
	//public GameObject weapon;

	// Use this for initialization
	void Start () 
	{
		myBody = this.GetComponent<Rigidbody2D> ();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		

		Vector3 moveVec = new Vector3 (CrossPlatformInputManager.GetAxis ("Horizontal"), CrossPlatformInputManager.GetAxis ("Vertical"), 0);
		moveVec.Normalize ();
		//moveVec.x =  moveVec.x * moveForce * Time.fixedDeltaTime;
		//moveVec.y =  moveVec.y * moveForce * Time.fixedDeltaTime;
		moveVec *= moveForce * Time.fixedDeltaTime;
		transform.position += moveVec; //@todo: Rigidbody.MovePosition?

		Vector2 angleVec = new Vector2 (CrossPlatformInputManager.GetAxis ("Horizontal1"), CrossPlatformInputManager.GetAxis ("Vertical1"));
		float mg = angleVec.magnitude;
		//Debug.Log(string.Format("Magnitude: {0}", mg)); // mg 0..1

		if (mg == 0)
			return;
		
		float angle = Vector3.Angle(angleVec, new Vector3(1, 0, 0));
		if (angleVec.y < 0) angle *= -1;
		var rotationVector = transform.rotation.eulerAngles;
		rotationVector.z = angle;
		transform.rotation = Quaternion.Euler(rotationVector);

		if (mg > 0.99) 
		{
			System.TimeSpan diff = System.DateTime.Now.Subtract (lastShot);
			double totaSeconds = diff.TotalSeconds;
			if (totaSeconds > timeBetweenBullets) 
			{				
				lastShot = System.DateTime.Now;
				GameObject bullet = Instantiate (bulletType, transform.position, transform.rotation) as GameObject;
				Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), bullet.GetComponent<BoxCollider2D>());
			}
		}
	}
}