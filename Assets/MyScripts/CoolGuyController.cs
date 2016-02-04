using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CoolGuyController : MonoBehaviour {

	Rigidbody2D myBody;
	System.DateTime lastShot;

	public float moveForce = 5;
	public double timeBetweenBullets = 0.5d;
	public GameObject bulletType;

	// Use this for initialization
	void Start () 
	{
		myBody = this.GetComponent<Rigidbody2D> ();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		Vector2 moveVec = new Vector2 (CrossPlatformInputManager.GetAxis ("Horizontal"), CrossPlatformInputManager.GetAxis ("Vertical"));
		moveVec *= moveForce;
		myBody.AddForce (moveVec);

		Vector2 angleVec = new Vector2 (CrossPlatformInputManager.GetAxis ("Horizontal1"), CrossPlatformInputManager.GetAxis ("Vertical1"));
		float mg = angleVec.magnitude;
		//Debug.Log(string.Format("Magnitude: {0}", mg)); // mg 0..1

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
			}
		}
	}
}