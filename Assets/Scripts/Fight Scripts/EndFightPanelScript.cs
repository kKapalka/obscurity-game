using System.Collections;
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
	int[] playerStatus=new int[2];
	LootManager lm;
	public void EndOfFight(bool won){
		GameObject.Find ("PlayerData").SetActive (false);
		GameObject.Find ("EnemyData").SetActive (false);
		GameObject.Find ("CrystalsBG").SetActive (false);
		GameObject.Find ("TurnTag").SetActive (false);
		title.text=won?"VICTORY!":"DEFEAT!";

		if (won) {
			//Add loot
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
			string[] currentInv = ReadScript.Read<string[]> ("Inventory");
			if (currentInv == default(string[]))
				currentInv = new string[0];
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
			//Save progress
			GameObject.Find ("ShapesManager").GetComponent<ShapesManager> ().SaveProgress ();

			//Add experience points
			int[] playerStatus = ReadScript.Read<int[]> ("PlayerXP");
			if (playerStatus == default(int[])) {
				playerStatus = new int[]{ 1, 0 };
			}
			if (playerStatus[1]+EnemySelection.Instance.experiencePoints >= 100*playerStatus[0]) {
				levelUpText.text="level up! "+playerStatus[0]+" -> "+(playerStatus[0]+1);

			}
			StartCoroutine (AnimateXPBar ());

			file = File.Create(Application.persistentDataPath + "/PlayerXP.dat");
			bf.Serialize (file,playerStatus);
			file.Close ();
		} else {
			loot.SetActive (false);
		}
	}

	public void Continue(){
		SceneManager.LoadScene ("Location Selection");
	}


	public IEnumerator AnimateXPBar(){
		XPSlider.maxValue = (float)100 * playerStatus [0];
		XPSlider.value = (float)playerStatus [1];
		for (int i = 0; i <= EnemySelection.Instance.experiencePoints; i++,playerStatus[1]++) {
			XPText.text = playerStatus[1] + "/" + XPSlider.maxValue;
			XPSlider.value = playerStatus [1];
			if (playerStatus[1] >= 100*playerStatus[0]) {
				playerStatus [1] -= 100 * playerStatus [0];
				playerStatus [0]++;
				XPSlider.maxValue = (float)100 * playerStatus [0];
			}
			yield return new WaitForSeconds (0.002f);
		}
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file;
		file = File.Create(Application.persistentDataPath + "/PlayerXP.dat");
		Debug.Log ("saved");
		bf.Serialize (file,playerStatus);
		file.Close ();
	}
}
