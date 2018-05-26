using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocationSelectionScript : MonoBehaviour {

	public Text dayCounter;
	int days;
	GameObject[] FightSelection;
	// Use this for initialization
	void Start () {
		FightSelection = GameObject.FindGameObjectsWithTag ("FightSelection");
		foreach (GameObject go in FightSelection) {
			go.SetActive (false);
		}
		GameObject.Find ("NextMissionPanel").SetActive (false);
		days = (PlayerPrefs.HasKey ("Days") ? PlayerPrefs.GetInt ("Days") : 1);
		dayCounter.text = "Day #" + days;
	}
	
	public void ZoneChosen(GameObject toActivate){
		foreach (GameObject go in FightSelection) {
			go.SetActive (false);
		}
		toActivate.SetActive (true);
	}
	public void Embark(){
		days++;
		PlayerPrefs.SetInt ("Days", days);
		SceneManager.LoadScene ("Fight");
	}
	public void ReturnToMainMenu(){
		SceneManager.LoadScene ("Main Menu");
	}
}
