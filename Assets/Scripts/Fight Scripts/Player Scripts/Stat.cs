using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Stat {

	public int value;
	int lowCap, highCap;
	List<int> modifiers = new List<int> ();

	public Stat(int low, int high){
		this.lowCap = low;
		this.highCap = high;
	}

	public int getValue(){
		int finalValue = value;
		foreach (int modifier in modifiers) {
			finalValue += modifier;
		}
		if (finalValue < lowCap)
			finalValue = lowCap;
		if (finalValue > highCap)
			finalValue = highCap;
		return finalValue;
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
