using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationSelectionScript : MonoBehaviour {
	
	GameObject[] FightSelection;
	// Use this for initialization
	void Start () {
		FightSelection = GameObject.FindGameObjectsWithTag ("FightSelection");
		foreach (GameObject go in FightSelection) {
			go.SetActive (false);
		}
		GameObject.Find ("NextMissionPanel").SetActive (false);
	}
	
	public void ZoneChosen(GameObject toActivate){
		foreach (GameObject go in FightSelection) {
			go.SetActive (false);
		}
		toActivate.SetActive (true);
	}
	public void Embark(){
		SceneManager.LoadScene ("Fight");
	}
	public void ReturnToMainMenu(){
		SceneManager.LoadScene ("Main Menu");
	}
}
