using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndFightPanelScript : MonoBehaviour {
	public Text title;
	public GameObject[] ItemPool;
	public GameObject loot;
	LootManager lm;
	public GameObject Inventory;
	public void EndOfFight(bool won){
		GameObject.Find ("PlayerData").SetActive (false);
		GameObject.Find ("EnemyData").SetActive (false);
		GameObject.Find ("CrystalsBG").SetActive (false);
		GameObject.Find ("TurnTag").SetActive (false);
		title.text=won?"VICTORY!":"DEFEAT!";

		if (won) {
			loot.SetActive (true);
			lm = loot.GetComponent<LootManager> ();
			lm.items [0] = ItemPool [Random.Range(0,ItemPool.Length)].GetComponent<Item> ();
			lm.items [1] = ItemPool [Random.Range(0,ItemPool.Length)].GetComponent<Item> ();
			while (lm.items [1] == lm.items [0]) {
				lm.items [1] = ItemPool [Random.Range (0, ItemPool.Length)].GetComponent<Item> ();
			}
			lm.Initialize ();
			Inventory.GetComponent<InventoryManager> ().addToInventory(lm.items);
			GameObject.Find ("ShapesManager").GetComponent<ShapesManager> ().SaveProgress ();
		} else {
			loot.SetActive (false);
		}
	}

	public void Continue(){
		SceneManager.LoadScene ("Location Selection");
	}
}
