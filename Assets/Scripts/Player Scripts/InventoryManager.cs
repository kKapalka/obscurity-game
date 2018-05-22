using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : ItemManager{

	public Scrollbar scrollBar;
	public Vector3 relocation;
	public GameObject equipPanel;
	// Use this for initialization
	void Awake () {
		Initialize ();
	}

	public override bool isEquipPanelActive ()
	{
		return equipPanel.activeSelf;
	}

	public override int getShift ()
	{
		return (int)Mathf.Floor (scrollBar.value * scrollBar.numberOfSteps) * 3;
	}
	public override Vector3 getRelocation ()
	{
		return relocation;
	}
	public override void Initialize ()
	{
		for (int i = 0; i < itemSlots.Length; i++) {
			if (items[i+getShift()]!=null)
				itemSlots [i].GetComponent<Image> ().sprite = items [i+getShift()].icon;
			else 
				itemSlots [i].GetComponent<Image> ().sprite = slotIcon;

		}
		currentPos = 0;
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
	}
}
