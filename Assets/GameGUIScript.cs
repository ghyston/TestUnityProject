using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameGUIScript : MonoBehaviour {

	bool showRestartPopup = false;

	public void ShowRestartPopup()
	{
		showRestartPopup = true;
	}

	void OnGUI() {
		if (showRestartPopup) {


			Rect windowRect = new Rect (300, 200, 200, 150);
			GUI.Window (0, windowRect, DoMyWindow, "YOU ARE DEAD");
		}
	}

	void DoMyWindow(int windowID) {

		int kills = ((ZombieFabric)GameObject.Find ("ZombieFabric").GetComponent<ZombieFabric> ()).zombiesKilled;

		GUI.TextArea (new Rect (10, 30, 180, 30), string.Format("Zombies killed: {0}", kills));
		GUI.backgroundColor = new Color(0.5f, 0.0f, 0.0f);
		if (GUI.Button (new Rect (10, 70, 180, 45), "Restart world")) 
		{
			SceneManager.LoadScene ("FightingScene");		
		}

	}
}
