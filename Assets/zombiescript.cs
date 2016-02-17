using UnityEngine;
using System.Collections;

public class zombiescript : MonoBehaviour {

	public GameObject corpseObject;
	public GameObject targetObject;
	public float speed = 2;

	System.DateTime lastTargetUpdate; //@todo: use InvokeRepeating!
	public double minUpdateTime = 1.0d;
	public double maxUpdateTime = 5.0d;

	private bool following = false;

	public float deltaHitTime = 0.7f;
	public int hitDamage = 3;

	// Use this for initialization
	void Start () {
		following = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!targetObject)
			return;

		if (!following)
			return;

		System.TimeSpan diff = System.DateTime.Now.Subtract (lastTargetUpdate);
		double totaSeconds = diff.TotalSeconds;

		if (Random.value < ((totaSeconds - minUpdateTime) / (maxUpdateTime - minUpdateTime))) 
		{
			lastTargetUpdate = System.DateTime.Now;
			updateVelocity ();
		}
		//@todo: change states etc.
	}

	void updateVelocity()
	{
		Vector3 vecToTarget = targetObject.transform.position - transform.position;
		vecToTarget.Normalize ();
		GetComponent<Rigidbody2D> ().velocity = vecToTarget * speed;
		GetComponent<Rigidbody2D> ().angularVelocity = 0;

		transform.rotation = Quaternion.FromToRotation (new Vector3(1, 0, 0) , targetObject.transform.position - transform.position);
	}

	//@todo: move to zombie attack script?
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject != targetObject)
			return;
		InvokeRepeating ("HitTarget", 0, deltaHitTime);			
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject != targetObject)
			return;

		CancelInvoke ();
	}

	void HitTarget()
	{
		if (!targetObject)
			return;

		targetObject.GetComponent<Health> ().TakeDamage (hitDamage);
	}

	public void OnPlayerDead()
	{
		if (IsInvoking ())
			CancelInvoke ();

		targetObject = null;
		Debug.Log ("OnPlayerDead()");
	}
}
