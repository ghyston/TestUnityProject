using UnityEngine;
using System.Collections;

public class DragableConnector : BaseTowerConnector {

	// Update is called once per frame
	void Update () 
	{
		Debug.Log("DragableConnector::Update");
		UpdateVerticles ();
	}

}
