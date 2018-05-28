using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class GameOverScreenScript : MonoBehaviour {
	public Text statText;
	// Use this for initialization
	void Start () {
		statText.text = "You completed the game in " + PlayerPrefs.GetInt ("Days") + " days.";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey) {
			ClearAllSavedData ();
			SceneManager.LoadScene ("Main Menu");
		}
	}

	public void ClearAllSavedData(){
		if (File.Exists (Application.persistentDataPath + "/Inventory.dat"))
			File.Delete (Application.persistentDataPath + "/Inventory.dat");
		if (File.Exists (Application.persistentDataPath + "/Equipment.dat"))
			File.Delete (Application.persistentDataPath + "/Equipment.dat");
		if (File.Exists (Application.persistentDataPath + "/PlayerXP.dat"))
			File.Delete (Application.persistentDataPath + "/PlayerXP.dat");
		if (File.Exists (Application.persistentDataPath + "/Progress.dat"))
			File.Delete (Application.persistentDataPath + "/Progress.dat");
		PlayerPrefs.SetInt ("Days", 1);
	}

}
