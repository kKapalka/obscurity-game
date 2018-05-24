using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootManager : ItemManager {

	public Vector3 relocation;

	public override void Initialize ()
	{
		Debug.Log ("Initializing");
		for (int i = 0; i < itemSlots.Length; i++) {
			if (items[i]!=null)
				itemSlots [i].GetComponent<Image> ().sprite = items [i].icon;
			else 
				itemSlots [i].GetComponent<Image> ().sprite = slotIcon;
		}
	}

	public override bool isEquipPanelActive ()
	{
		return false;
	}
	public override Vector3 getRelocation ()
	{
		return relocation;
	}
	public override int getShift ()
	{
		return 0;
	}
}
