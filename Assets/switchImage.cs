using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class switchImage : MonoBehaviour {

	Button btn;
	Sprite pistolSprite;
	Sprite shotgunSprite;
	Inventory inventory;

	void Start()
	{
		btn = GetComponent<Button> ();
		pistolSprite = Resources.Load<Sprite> ("pistol");
		shotgunSprite = Resources.Load<Sprite> ("shotgun");
		inventory = GameObject.Find ("Player").GetComponent<Inventory> ();
	}

	public void SwitchWeaponImage()
	{
		if (inventory.currentWeaponIndex == 0) {
			btn.image.sprite = pistolSprite;
		} else {
			btn.image.sprite = shotgunSprite;
		}
			
	}
}
