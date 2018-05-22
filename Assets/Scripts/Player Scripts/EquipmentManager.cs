using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class EquipmentManager : ItemManager {

	//items = new Item[4];
	CharacterStats stats;
	//itemSlots = new Button[4];

	//one item has to be a weapon, or else attack type i basic and damage dealt is lowered by additional 40%
	bool locked;
	public Vector3 relocation;
	void Awake(){
		stats = GetComponent<CharacterStats> ();
		Initialize ();
	}
	public void AddModifiersOfType(string type){
		stats = GetComponent<CharacterStats> ();
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
	public override void Initialize ()
	{
		locked = false;
		AddModifiersOfType ("static");
		for (int i = 0; i < itemSlots.Length; i++) {
			if (items[i]!=null)
				itemSlots [i].GetComponent<Image> ().sprite = items [i].icon;
			else 
				itemSlots [i].GetComponent<Image> ().sprite = slotIcon;

		}
		if (!locked) {
			stats.damageMultiplier.AddModifier (-40);
			GetComponent<PlayerScript> ().LoadWeapon ("basic");
			locked = true;
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
		stats.removeAllModifiers ();
		Initialize ();
		im.hideEquipMenu ();
	}
}
