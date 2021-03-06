﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class EndFightPanelScript : MonoBehaviour {
	public Text title,levelUpText,XPText;
	public Slider XPSlider;
	public GameObject[] ItemPool;
	public GameObject loot;
	public Button ContinueButton;
	int[] playerStatus=new int[2];
	int XPGained;
	LootManager lm;
	bool won;

	void Awake(){
		float ratio = (float)Screen.width / (float)Screen.height;
		if (ratio<1.52)
			this.transform.localScale = new Vector3 (1.3f, 1.3f, 1.0f);
		else if (ratio<1.6)
			this.transform.localScale = new Vector3 (1.1f, 1.1f, 1.0f);
	}

	public void EndOfFight(bool won){
		this.won = won;
		//GameObject.Find ("PlayerData").SetActive (false);
		//GameObject.Find ("EnemyData").SetActive (false);
		GameObject.Find ("CrystalsBG").SetActive (false);
		GameObject.Find ("TurnTag").SetActive (false);
		title.text=won?"VICTORY!":"DEFEAT!";
		playerStatus = GameObject.Find("Player").GetComponent<PlayerController>().getXP();
		XPSlider.maxValue = (float)100 * playerStatus [0];
		XPSlider.value = (float)playerStatus [1];
		XPText.text = playerStatus[1] + "/" + XPSlider.maxValue;
		if (won) {
			//Add loot
			if (EnemySelection.created && !EnemySelection.Instance.getDefeated ()) {
				EnemySelection.Instance.setDefeated (true);
				//loot and progress should only be saved if selected enemy was not yet defeated
				loot.SetActive (true);
				lm = loot.GetComponent<LootManager> ();
				lm.items [0] = ItemPool [Random.Range (0, ItemPool.Length)].GetComponent<Item> ();
				lm.items [1] = ItemPool [Random.Range (0, ItemPool.Length)].GetComponent<Item> ();
				while (lm.items [0] == lm.items [1])
					lm.items [1] = ItemPool [Random.Range (0, ItemPool.Length)].GetComponent<Item> ();
				lm.Initialize ();


			} else {
				ContinueButton.interactable = true;
			}
			//Add experience points

			XPGained = (EnemySelection.created ? EnemySelection.Instance.experiencePoints : 5);
			if (playerStatus [1] + XPGained >= 100 * playerStatus [0]) {
				levelUpText.text = "level up! " + playerStatus [0] + " -> " + (playerStatus [0] + 1) + "\n" +
				"Damage: " + Mathf.RoundToInt (100f * (float)Mathf.Pow (1.2f, playerStatus [0] - 1)) + "% ->" + Mathf.RoundToInt (100f * (float)Mathf.Pow (1.2f, playerStatus [0])) + "%\n" +
				"HP: " + Mathf.RoundToInt (300f * (float)Mathf.Pow (1.2f, playerStatus [0] - 1)) + "->" + Mathf.RoundToInt (300f * (float)Mathf.Pow (1.2f, playerStatus [0]));
			} else {
				levelUpText.text = "";
			}
			StartCoroutine (AnimateXPBar ());

		} else {
			loot.SetActive (false);
			levelUpText.text = "";
			ContinueButton.interactable = true;
		}
	}

	public void Continue(){
		
		if (won && EnemySelection.Instance!=null && EnemySelection.Instance.encounterName == "Ace of Spades") {
				SceneManager.LoadScene ("Game Over");

		}else {
			SceneManager.LoadScene ("Location Selection");
		}
	}


	public IEnumerator AnimateXPBar(){
		XPSlider.maxValue = (float)100 * playerStatus [0];
		XPSlider.value = (float)playerStatus [1];
		for (int i = 0; i <= XPGained; i++,playerStatus[1]++) {
			XPText.text = playerStatus[1] + "/" + XPSlider.maxValue;
			XPSlider.value = playerStatus [1];
			if (playerStatus[1] >= 100*playerStatus[0]) {
				playerStatus [1] -= 100 * playerStatus [0];
				playerStatus [0]++;
				XPSlider.maxValue = (float)100 * playerStatus [0];
			}
			yield return new WaitForSeconds (1.5f/XPGained);
		}
		playerStatus [1]--;
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file;
		file = File.Create(Application.persistentDataPath + "/PlayerXP.dat");
		bf.Serialize (file,playerStatus);
		file.Close ();
	}

	public void chooseItem(int index){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file;
		string[] currentInv = ReadScript.Read<string[]> ("Inventory");
		if (currentInv == default(string[]))
			currentInv = new string[0];
		string[] newInv = new string[currentInv.Length + lm.items.Length];
		int i = 0;
		foreach (string name in currentInv) {
			newInv [i] = currentInv [i++];
		}
		newInv [i++] = lm.items[index].name.Replace("(Clone)","");

		foreach (Button button in lm.itemSlots) {
			button.gameObject.SetActive (false);
		}
		lm.lootText.text = lm.items [index].itemName + " added to Inventory.";
		lm.HideTooltip ();

		file = File.Create(Application.persistentDataPath + "/Inventory.dat");
		bf.Serialize (file, newInv);
		file.Close ();
		//Save progress
		GameObject.Find ("ShapesManager").GetComponent<ShapesManager> ().SaveProgress ();
		ContinueButton.interactable = true;
	}
}
