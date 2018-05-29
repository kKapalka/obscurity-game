using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Stat {

	public int value = 0;
	int lowCap, highCap;
	List<int> modifiers = new List<int> ();

	public Stat(int low, int high){
		this.lowCap = low;
		this.highCap = high;
	}
	public void setValue(int value){
		this.value = this.value+ value;
	}
	public int getValue(){
		int finalValue = value;
		foreach (int modifier in modifiers) {
			finalValue += modifier;
		}
		return Mathf.Clamp (finalValue, value + lowCap, value + highCap);
	}

	public void AddModifier(int modifier){
		modifiers.Add (modifier);
	}
	public void RemoveModifier(int modifier){
		modifiers.Remove (modifier);
	}
	public void RemoveAll(){
		modifiers.Clear ();
	}
}
