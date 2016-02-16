using UnityEngine;
using System.Collections;

public class BaseWeapon {

	public GameObject bulletType;
	public double timeBetweenBullets = 0.5d;
	protected System.DateTime lastShot;

	protected bool checkTiming()
	{
		System.TimeSpan diff = System.DateTime.Now.Subtract (lastShot);
		double totaSeconds = diff.TotalSeconds;	
		return (totaSeconds > timeBetweenBullets);
	}

	virtual public bool Shot(GameObject shooter)
	{
		if (!checkTiming())
			return false;
		
		// some overridable code
		return true;
	}

	virtual public string GetName() {
		return " Хуй моржовый! ";
	}

	//abstract void GenerateBullets() //@todo
}
