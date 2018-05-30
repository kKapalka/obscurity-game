using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class EquipmentManager : ItemManager {

	public GameObject playerData;
	CharacterStats stats;
	//itemSlots = new Button[4];
	public bool fight;
	bool created;

	bool locked;
	public Vector3 relocation;

	void Awake(){
		Load ("Equipment");
		Initialize ();
	}
	public void AwakeOnFight(){
		if (fight) {
			stats = GetComponent<CharacterStats> ();
		}
			Initialize ();
	}
	public void AddModifiersOfType(string type){
		stats = GetComponent<CharacterStats> ();
		if (type == "static")
			stats.removeAllModifiers ();
		if(type=="base")
			stats.ResetStats ();
		
		foreach (Item item in items) {
			if (item != null) {
				List<Modifier> modifiers = item.getModifiersOfType (type);
				if (type == "unique") {
					foreach (Modifier mod in modifiers) {
						GetComponent<PlayerController> ().addToUniqueMods (mod.stat);
						Debug.Log (mod.stat);
					}
				}
				foreach (Modifier mod in modifiers) {
					if (mod.stat.Contains ("resistance")) {
						int index = (int)Char.GetNumericValue (mod.stat.ToCharArray () [12]);
						if (stats.resistances.Length > index)
						if (type == "base")
							stats.resistances [index].setValue (int.Parse (mod.value));
						else
							stats.resistances [index].AddModifier (int.Parse (mod.value));
					} else if (mod.stat.Contains ("dodge")) {
						if (type == "base")
							stats.dodge.setValue (int.Parse (mod.value));
						else
							stats.dodge.AddModifier (int.Parse (mod.value));
					} else if (mod.stat.Contains ("damage")) {
						if (type == "base")
							stats.damageMultiplier.setValue (int.Parse (mod.value));
						else
							stats.damageMultiplier.AddModifier (int.Parse (mod.value));
					} else if (mod.stat.Contains ("strength")) {
						if (type == "base")
							stats.strength.setValue (int.Parse (mod.value));
						else
							stats.strength.AddModifier (int.Parse (mod.value));
					} else if (mod.stat.Contains ("regeneration")) {
						if (type == "base")
							stats.regeneration.setValue (int.Parse (mod.value));
						else
							stats.regeneration.AddModifier (int.Parse (mod.value));
					} else if (mod.stat.Contains ("weaponType") && !locked) {						
						GetComponent<PlayerScript> ().weaponType = mod.value;
						GetComponent<PlayerScript> ().LoadWeapon (mod.value);
						locked = true;
					} 
				}
			}
		}
		if (type=="static" && !locked) {
			GetComponent<PlayerScript> ().LoadWeapon ("basic");
			locked = true;
		}
		playerData.GetComponent<PlayerDataScript>().Assign(this.gameObject);
	}

	public override bool isEquipPanelActive ()
	{
		return false;
	}
	public override int getShift ()
	{
		return 0;
	}
	public override Vector3 getRelocation(){
		return relocation;
	}
	public static EquipmentManager Instance {
		get;
		set;
	}
	public override void Initialize ()
	{
		locked = false;

		for (int i = 0; i < itemSlots.Length; i++) {
			if (items[i]!=null)
				itemSlots [i].GetComponent<Image> ().sprite = items [i].icon;
			else 
				itemSlots [i].GetComponent<Image> ().sprite = slotIcon;
		}
		if (fight) {
			AddModifiersOfType ("static");
			AddModifiersOfType ("base");
			AddModifiersOfType ("unique");
		}
		currentPos = 0;
	}
	public void Equip(int slot){
		InventoryManager im = GameObject.Find ("Inventory").GetComponent<InventoryManager> ();
		Item item = im.getItem ();
		if (items [slot] != null)
			im.ReplaceItemWith (items [slot]);
		else
			im.ReplaceItemWith (null);
		Debug.Log("Item "+item.itemName+" equipped at Slot #"+(slot+1));
		items [slot] = item;
		if (fight) {
			stats.removeAllModifiers ();
			stats.ResetStats ();
		}
		Initialize ();
		Save ("Equipment");
		im.hideEquipMenu ();
		if (!fight) {
			Instance = this;
		}
	}

	public void Unequip(int slot,GameObject Inventory){
		InventoryManager im = Inventory.GetComponent<InventoryManager> ();
		im.Load ("Inventory");
		im.addToInventory (new Item[]{ GameObject.Instantiate (items [slot]) as Item });
		im.Initialize ();
		items [slot] = null;
		Initialize ();
		Save ("Equipment");
		Instance = this;
	}

	public void Load(string name){
		string[] itemNames = ReadScript.Read<string[]> (name);
		if(itemNames==default(string[])) return;

		this.items = new Item[itemNames.Length];
		for (int i = 0; i < itemNames.Length; i++) {
			if (itemNames [i] != null) {
				GameObject newItem = (GameObject)Instantiate (Resources.Load ("Items/" + itemNames [i]));
				this.items [i] = newItem.GetComponent<Item> ();
			}
		}
	}
	public void Save(string name){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/"+name+".dat");
		file.SetLength (0);
		string[] itemNames = new string[items.Length];
		for (int i = 0; i < items.Length; i++)
			if (items [i] != null)
				itemNames [i] = items [i].name.Replace("(Clone)","");
			else
				itemNames [i] = null;

		bf.Serialize(file, itemNames);
		file.Close();
	}

}
