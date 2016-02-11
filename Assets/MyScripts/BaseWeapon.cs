using UnityEngine;
using System.Collections;

public class BaseWeapon : MonoBehaviour {

	public GameObject bulletType;
	public double timeBetweenBullets = 0.5d;
	protected System.DateTime lastShot;

	protected bool checkTiming()
	{
		System.TimeSpan diff = System.DateTime.Now.Subtract (lastShot);
		double totaSeconds = diff.TotalSeconds;	
		return (totaSeconds > timeBetweenBullets);
	}

	virtual public void Shot(GameObject shooter)
	{
		if (!checkTiming())
			return;
		
		// some overridable code
	}

	public virtual string GetName() {
		return " Хуй моржовый! ";
	}

	//abstract void GenerateBullets() //@todo
}
