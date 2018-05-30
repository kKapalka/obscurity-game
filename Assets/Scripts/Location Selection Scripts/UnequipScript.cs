using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnequipScript : MonoBehaviour {

	Item selectedItem;
	int currentIndex;
	public GameObject Inventory,Equipment;

	public void Assign(GameObject item){
		this.gameObject.transform.position = item.transform.position;
		this.gameObject.SetActive (true);
		this.selectedItem = item.GetComponent<Item> ();
		this.currentIndex = int.Parse(item.name.Substring(item.name.Length-1))-1;
	}
	public void Unequip(){
		Equipment.GetComponent<EquipmentManager> ().Unequip (currentIndex, Inventory);
		this.gameObject.SetActive (false);
	}
	public void Cancel(){
		this.gameObject.SetActive (false);
	}
}
