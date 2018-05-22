using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemManager : MonoBehaviour {

	public Button[] itemSlots;
	public Item[] items;
	public Sprite slotIcon;
	public GameObject tooltipPanel;

	public int currentPos;
	public abstract void Initialize ();

	public abstract int getShift ();
	public abstract Vector3 getRelocation();
	public void ShowTooltip (int pos) {
		if (items [pos+getShift()] != null) {
			if (!tooltipPanel.activeSelf) {
				tooltipPanel.SetActive (true);
				tooltipPanel.transform.position = itemSlots [pos].transform.position+getRelocation();
				tooltipPanel.GetComponentInChildren<Text> ().text = (items [pos+getShift()].itemName+"\n\n"+items[pos+getShift()].tooltip).Replace(".",".\n");
				this.currentPos = pos;
			} else {
				if (this.currentPos != pos) {
					tooltipPanel.transform.position = itemSlots [pos].transform.position+getRelocation();
					tooltipPanel.GetComponentInChildren<Text> ().text = (items [pos+getShift()].itemName+"\n\n"+items[pos+getShift()].tooltip).Replace(".",".\n");
					this.currentPos = pos;
				}
			}
		}
	}
	public void HideTooltip(){
		if (!isEquipPanelActive()) tooltipPanel.SetActive (false);
	}
	public abstract bool isEquipPanelActive ();
}
