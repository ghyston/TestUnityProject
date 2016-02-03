using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class dragScript : MonoBehaviour {

	public float _minDistance = 3;

	private GameObject dynConnector;
	private GameObject newTower;

	void OnMouseDown() 
	{

		var mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos.z = 0;

		Object resTower = Resources.Load("TouchingSprite");
		newTower = Instantiate(resTower, mousePos, transform.rotation) as GameObject;

		Object resConnector = Resources.Load("DynamicConnector");
		dynConnector = Instantiate (resConnector, mousePos, transform.rotation) as GameObject;

		dynConnector.GetComponent<DragableConnector> ().obj1 = this.gameObject;
		dynConnector.GetComponent<DragableConnector> ().obj2 = newTower;

	}

	void OnMouseUp()
	{
		//Destroy (dynConnector);
		//Destroy (newTower);
		newTower = null;
		dynConnector = null;

		//var mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		//mousePos.z = 0;
	}

	void OnMouseDrag() 
	{
		var mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos.z = 0;
		newTower.transform.position = mousePos;
	}
}
