using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RestartButtonScript : MonoBehaviour {

	public GameObject playerType;

	public void OnClick()
	{
		GetComponent<Button> ().interactable = false;

		GameObject instance = Instantiate (playerType, Vector3.zero, Quaternion.identity) as GameObject;
		GameObject.FindObjectOfType<MyCameraFollowScript> ().followToObj = instance;

		//tell the zombies, they have new food!
		Object[] objects = GameObject.FindObjectsOfType<zombiescript> ();
		foreach (Object go in objects) {
			((zombiescript)go).targetObject = instance;;
		}
		ZombieFabric fabric = GameObject.FindObjectOfType<ZombieFabric> ();
		fabric.enabled = true;
	}
}
