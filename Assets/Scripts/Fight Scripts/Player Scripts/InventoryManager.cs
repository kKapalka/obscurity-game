﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class InventoryManager : ItemManager{

	public Scrollbar scrollBar;
	public Vector3 relocation;
	public GameObject equipPanel;
	// Use this for initialization
	void Start () {
		Load("Inventory");
		Initialize ();
	}

	public override bool isEquipPanelActive ()
	{
		return equipPanel.activeSelf;
	}

	public override int getShift ()
	{
		return Mathf.Clamp(Mathf.RoundToInt (scrollBar.value * scrollBar.numberOfSteps) * GetComponentInChildren<GridLayoutGroup>().constraintCount,0,65535);
	}
	public override Vector3 getRelocation ()
	{
		return relocation;
	}
	public override void Initialize ()
	{
		currentPos = 0;
		for (int i = 0; i < itemSlots.Length; i++) {
			if (i + getShift () >= items.Length)
				return;
			if (items[i+getShift()]!=null)
				itemSlots [i].GetComponent<Image> ().sprite = items [i+getShift()].icon;
			else 
				itemSlots [i].GetComponent<Image> ().sprite = slotIcon;

		}

	}
	public void onShift(){
		for (int i = 0; i < itemSlots.Length; i++) {
			if (i+getShift()<items.Length && items[i+getShift()]!=null)
				itemSlots [i].GetComponent<Image> ().sprite = items [i+getShift()].icon;
			else 
				itemSlots [i].GetComponent<Image> ().sprite = slotIcon;

		}
	}
	public void showEquipMenu(){
		if (items [currentPos + getShift ()] != null && tooltipPanel.activeSelf) {
			equipPanel.SetActive (true);
			equipPanel.transform.position = itemSlots [currentPos].transform.position+(getRelocation()/2);
		}		
	}
	public void hideEquipMenu(){
		if (equipPanel.activeSelf) {
			equipPanel.SetActive (false);
			HideTooltip ();
		}
	}
	public Item getItem(){
		return items [currentPos + getShift ()];
	}
	public void ReplaceItemWith(Item item){
		items [currentPos + getShift ()] = item;
		Initialize ();
		Save("Inventory");
	}

	public void Load(string name){

		if (File.Exists(Application.persistentDataPath + "/"+name+".dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/"+name+".dat", FileMode.Open);
			string[] itemNames = (string[])bf.Deserialize(file);
			this.items = new Item[itemNames.Length];
			int slotIndex = 0;
			for (int i = 0; i < itemNames.Length; i++) {
				if (itemNames [i] != null && itemNames[i]!="Null") {
					Debug.Log (itemNames [i]);
					GameObject newItem = (GameObject)Instantiate (Resources.Load ("Items/" + itemNames [i]));
					this.items [slotIndex++] = newItem.GetComponent<Item> ();
				}
			}
			file.Close ();
			scrollBar.numberOfSteps = (int)Mathf.Ceil(((float)slotIndex-9)/3f);
		}

	}
	public void Save(string name){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/"+name+".dat");
		string[] itemNames = new string[items.Length];
		for (int i = 0; i < items.Length; i++) {
			if (items [i] != null)
				itemNames [i] = items [i].name.Replace ("(Clone)", "");
			else
				itemNames [i] = null;
		}

		bf.Serialize(file, itemNames);
		file.Close();
	}

	public void addToInventory(Item[] loot){
		int allItems = 0;
		foreach (Item item in this.items) {
			if (item != null)
				allItems++;
		}
		Item[] newItems = new Item[allItems + loot.Length];
		int i = 0;
		foreach (Item item in this.items)
			if(item!=null)
				newItems [i++] = item;
		foreach (Item item in loot)
			newItems [i++] = item;
		this.items = newItems;
		Save ("Inventory");
	}

}
