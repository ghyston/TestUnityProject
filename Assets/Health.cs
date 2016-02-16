using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour {

	public float health = 100.0f;

	void Start()
	{
		UpdateHealthLabel ();
	}

	public void takeDamage(float damage)
	{
		health -= damage;
		CheckPulse ();
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

		GameObject.Find ("RestartButton").GetComponent<Button> ().interactable = true;

		Destroy (this.gameObject);
	}

	void UpdateHealthLabel()
	{
		Debug.Log ("UpdateHealthLabel()" + health);
		//@todo: all work with gui would be better to 
		GameObject.Find ("HealthLabel").GetComponent<Text> ().text = string.Format ("HEALTH: {0}", Mathf.RoundToInt(health));
	}
}
