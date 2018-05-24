using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.IO;

public class MainMenuScript : MonoBehaviour {

	public GameObject loadGame;
	void Start(){
		if (!File.Exists (Application.persistentDataPath + "/Inventory.dat") || !File.Exists (Application.persistentDataPath + "/Equipment.dat"))
			loadGame.SetActive(false);
	}
	public void ClearAllSavedData(){
		if (File.Exists (Application.persistentDataPath + "/Inventory.dat"))
			File.Delete (Application.persistentDataPath + "/Inventory.dat");
		if (File.Exists (Application.persistentDataPath + "/Equipment.dat"))
			File.Delete (Application.persistentDataPath + "/Equipment.dat");
	}
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
