﻿using UnityEngine;
using System.Collections;

public class ZombieFabric : MonoBehaviour {

	public GameObject zombieObject;

	System.DateTime lastZombieCreatedTime;
	public double minCreateTime = 1.0d;
	public double maxCreateTime = 5.0d;
	public bool isEnabled = true;
	public float innerRadius = 5;
	public float outherRadius = 3;

	// Use this for initialization
	void Start () {
	
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
