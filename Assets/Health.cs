using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour {

	public int maxHealth = 100;
	public int health = 100;

	void Start()
	{
		UpdateHealthLabel ();
	}

	public void TakeDamage(int damage)
	{
		health -= damage;
		CheckPulse ();
		UpdateHealthLabel ();
	}

	public bool isFull()
	{
		return health == maxHealth;
	}

	public void IncreaseHealth(int amount)
	{
		int newValue = health + amount;
		health = Mathf.Min (newValue, maxHealth);
		UpdateHealthLabel ();
	}

	void CheckPulse()
	{
		if (health > 0)
			return;

		health = 0;

		//@todo: move this code somewhere else!
		Object[] objects = GameObject.FindObjectsOfType<zombiescript> ();
		foreach (Object go in objects) {
			((zombiescript)go).OnPlayerDead ();
		}
		ZombieFabric fabric = GameObject.FindObjectOfType<ZombieFabric> ();
		fabric.enabled = false;

		//GameObject.Find ("RestartButton").GetComponent<Button> ().interactable = true;
		((GameGUIScript)GameObject.Find("GameGUI").GetComponent<GameGUIScript>()).ShowRestartPopup();


		Destroy (this.gameObject);
	}

	void UpdateHealthLabel()
	{
		//@todo: all work with gui would be better to 
		GameObject.Find ("HealthLabel").GetComponent<Text> ().text = string.Format ("HEALTH: {0}", health);
	}
}
