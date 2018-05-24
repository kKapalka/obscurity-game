using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class EquipmentManager : ItemManager {

	//items = new Item[4];
	CharacterStats stats;
	//itemSlots = new Button[4];
	public bool fight;
	bool created;
	//one item has to be a weapon, or else attack type i basic and damage dealt is lowered by additional 40%
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
		foreach (Item item in items) {
			if (item != null) {
				List<Modifier> modifiers = item.getModifiersOfType (type);
				foreach (Modifier mod in modifiers) {
					if (mod.stat.Contains ("resistance")) {
						int index = (int)Char.GetNumericValue (mod.stat.ToCharArray () [12]);
						if (stats.resistances.Length > index)
							stats.resistances [index].AddModifier (int.Parse(mod.value));
					} else if (mod.stat.Contains ("dodge"))
						stats.dodge.AddModifier (int.Parse(mod.value));
					else if (mod.stat.Contains ("damage"))
						stats.damageMultiplier.AddModifier (int.Parse(mod.value));
					else if (mod.stat.Contains ("strength"))
						stats.strength.AddModifier (int.Parse(mod.value));
					else if (mod.stat.Contains ("regeneration"))
						stats.regeneration.AddModifier (int.Parse(mod.value));
					else if (mod.stat.Contains ("weaponType") && !locked) {						
						GetComponent<PlayerScript> ().weaponType = mod.value;
						GetComponent<PlayerScript> ().LoadWeapon (mod.value);
						locked = true;
					}
				}
			}
		}
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
			if (!locked) {
				stats.damageMultiplier.AddModifier (-40);
				GetComponent<PlayerScript> ().LoadWeapon ("basic");
				locked = true;
			}
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
		}
		Initialize ();
		Save ("Equipment");
		im.hideEquipMenu ();
		if (!fight) {
			Instance = this;
		}
	}

	/*public void Load(EquipmentManager eqman){
		fight = true;
		items = eqman.items;
	}*/

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
