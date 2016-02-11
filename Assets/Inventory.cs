using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

	ArrayList weapons;
	int currentWeaponIndex;

	public BaseWeapon nextWeapon ()
	{
		currentWeaponIndex++;
		if (currentWeaponIndex == weapons.Count)
			currentWeaponIndex = 0;
		return (BaseWeapon)weapons [currentWeaponIndex];
	}

	// Use this for initialization
	void Start () {
		weapons = new ArrayList ();
		weapons.Add (new Pistol ());
		weapons.Add (new Shotgun ());
		currentWeaponIndex = 0;
	}
}
