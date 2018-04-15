using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightPanelScript : MonoBehaviour {

	void Start(){
		GameObject.Find ("End Fight Screen").SetActive (false);
	}

	public void TransitionToLocationSelection(){
		SceneManager.LoadScene("Location Selection");
	}
}
