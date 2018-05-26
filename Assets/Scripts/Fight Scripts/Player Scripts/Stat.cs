using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Stat {

	public int value;
	int lowCap, highCap;
	List<int> modifiers = new List<int> ();

	public int getValue(){
		int finalValue = value;
		foreach (int modifier in modifiers) {
			finalValue += modifier;
		}
		return (finalValue<lowCap?lowCap:(finalValue>highCap?highCap:finalValue));
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
	public void setCap(int low, int high){
		this.lowCap = low;
		this.highCap = high;
	}


}
