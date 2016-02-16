using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Inventory : MonoBehaviour { //@todo: should it be a mono behaviour?

	public class Ammo //@todo: would be cool to distinguish them by bullet type
	{
		public Ammo(int mag, int poc, int max)
		{
			magazine = mag;
			pocket = poc;
			magazineSize = max;
		}

		public int magazine;
		public int pocket;
		public int magazineSize;
	}

	ArrayList weapons;
	ArrayList ammo;
	public int currentWeaponIndex; //@todo: do getter only!
	public GameObject bulletType;

	public BaseWeapon nextWeapon ()
	{
		currentWeaponIndex++;
		if (currentWeaponIndex == weapons.Count)
			currentWeaponIndex = 0;
		updateAmmoLabel ();
		return (BaseWeapon)weapons [currentWeaponIndex];
	}

	// Use this for initialization
	public void Init () {
		weapons = new ArrayList ();

		Pistol pistol = new Pistol ();
		pistol.bulletType = bulletType;
		weapons.Add (pistol);

		Shotgun shotgun = new Shotgun ();
		shotgun.bulletType = bulletType;
		weapons.Add (shotgun);
		currentWeaponIndex = 0;

		ammo = new ArrayList ();
		ammo.Add (new Ammo (17, 34, 17));
		ammo.Add (new Ammo (3, 9, 3));
		updateAmmoLabel ();
	}

	public int getCurrentMagazineAmount()
	{
		return ((Ammo)ammo [currentWeaponIndex]).magazine;
	}

	public int getCurrentMagazinePocket()
	{
		return ((Ammo)ammo [currentWeaponIndex]).pocket;
	}

	public int decreadeCurrentMagazineAmount(int amount)
	{
		int cur = getCurrentMagazineAmount();
		int decreaseAt = (cur >= amount) ? amount : cur;
		((Ammo)ammo [currentWeaponIndex]).magazine = cur - decreaseAt;
		updateAmmoLabel ();
		return decreaseAt;
	}

	public void Reload()
	{
		Ammo currAmmo = (Ammo)ammo [currentWeaponIndex];
		if ((currAmmo.pocket == 0) || (currAmmo.magazine == currAmmo.magazineSize))
			return;

		int total = currAmmo.magazine + currAmmo.pocket;
		if (total > currAmmo.magazineSize) {
			currAmmo.magazine = currAmmo.magazineSize;
			currAmmo.pocket = total - currAmmo.magazineSize;
		} else {
			currAmmo.magazine = total;
			currAmmo.pocket = 0;
		}

		updateAmmoLabel ();
	}

	void updateAmmoLabel()
	{
		int amount = getCurrentMagazineAmount ();
		int pocket = getCurrentMagazinePocket ();

		GameObject.Find ("AmmoAmountLabel").GetComponent<Text> ().text = string.Format ("{0} / {1}", amount, pocket);
	}
		
}
