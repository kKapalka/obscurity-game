using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {


	public void QuitGame(){
		Debug.Log ("Game has just quitted.");
		Application.Quit ();
	}
	public void LoadCredits(){
		SceneManager.LoadScene ("Credits");
	}
	public void LoadGame(){
		SceneManager.LoadScene ("Location Selection");
	}
	public void StartNewGame(){
		SceneManager.LoadScene ("Story");
	}
}
