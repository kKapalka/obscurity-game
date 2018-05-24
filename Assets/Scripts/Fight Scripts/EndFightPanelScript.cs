using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndFightPanelScript : MonoBehaviour {
	public Text title;
	public GameObject[] itemPool;

	public void Load(bool won){
		GameObject.Find ("PlayerData").SetActive (false);
		GameObject.Find ("EnemyData").SetActive (false);
		GameObject.Find ("CrystalsBG").SetActive (false);
		GameObject.Find ("TurnTag").SetActive (false);
		title.text=won?"VICTORY!":"DEFEAT!";
	}

	public void Continue(){
		SceneManager.LoadScene ("Location Selection");
	}
}
