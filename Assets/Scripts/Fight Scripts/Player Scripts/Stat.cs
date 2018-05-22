using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Stat {

	public int value;
	List<int> modifiers = new List<int> ();

	public int getValue(){
		int finalValue = value;
		foreach (int modifier in modifiers) {
			finalValue += modifier;
		}
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
