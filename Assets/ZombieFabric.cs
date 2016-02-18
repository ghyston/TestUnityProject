using UnityEngine;
using System.Collections;

public class ZombieFabric : MonoBehaviour {

	public GameObject zombieObject;

	System.DateTime lastZombieCreatedTime;
	public double minCreateTime = 2.0d;
	public double maxCreateTime = 4.0d;
	public float increaseZombiesTime = 7.0f;
	public float increaseZombiesFactor = 0.8f;
	public bool isEnabled = true;
	public float innerRadius = 5;
	public float outherRadius = 3;

	public int zombiesKilled = 0;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("DecreaseZombiesTime", increaseZombiesTime, increaseZombiesTime);
	}

	void DecreaseZombiesTime()
	{
		minCreateTime *= increaseZombiesFactor;
		maxCreateTime *= increaseZombiesFactor;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isEnabled)
			return;

		System.TimeSpan diff = System.DateTime.Now.Subtract (lastZombieCreatedTime);
		double totaSeconds = diff.TotalSeconds;

		if (Random.value < ((totaSeconds - minCreateTime) / (maxCreateTime - minCreateTime))) 
		{
			lastZombieCreatedTime = System.DateTime.Now;
			createZombie ();
		}
	}

	void createZombie()
	{
		Vector2 rndVec = Random.insideUnitCircle * outherRadius;
		rndVec.x += ((rndVec.x < 0) ? -1 : 1) * innerRadius;
		rndVec.y += ((rndVec.y < 0) ? -1 : 1) * innerRadius;

		GameObject instance = Instantiate (zombieObject, new Vector3 (rndVec.x, rndVec.y, 0), Quaternion.identity) as GameObject;
		instance.GetComponent<zombiescript>().targetObject = GameObject.Find("Player");
		instance.name = "zombie";
	}
}
