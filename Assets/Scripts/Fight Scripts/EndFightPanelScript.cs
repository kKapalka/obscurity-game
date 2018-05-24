using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class EndFightPanelScript : MonoBehaviour {
	public Text title;
	public GameObject[] ItemPool;
	public GameObject loot;
	LootManager lm;
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

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file;
			string[] currentInv;
			if (File.Exists (Application.persistentDataPath + "/Inventory.dat")) {
				file = File.Open (Application.persistentDataPath + "/Inventory.dat", FileMode.Open);
				currentInv = (string[])bf.Deserialize (file);
				file.Close ();
			} else {
				currentInv = new string[0];
			}
			string[] newInv = new string[currentInv.Length + lm.items.Length];
			int i = 0;
			foreach (string name in currentInv) {
				newInv [i] = currentInv [i++];
			}
			foreach (Item item in lm.items) {
				newInv [i++] = item.name.Replace("(Clone)","");
			}
			file = File.Create(Application.persistentDataPath + "/Inventory.dat");
			bf.Serialize (file, newInv);
			file.Close ();

		} else {
			loot.SetActive (false);
		}
	}

	public void Continue(){
		SceneManager.LoadScene ("Location Selection");
	}
}
