using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour {

	public void LoadFightingScene()
	{
		SceneManager.LoadScene ("FightingScene");
	}
}
