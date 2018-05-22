using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Item:MonoBehaviour{
	
	public string itemName;
	public string tooltip;
	public Sprite icon;

	public List<Modifier> modifiers;

	public List<Modifier> getModifiersOfType(string type){
		List<Modifier> validModifiers = new List<Modifier> ();
		foreach (Modifier mod in modifiers) {
			if (type == mod.modType)
				validModifiers.Add (mod);
		}
		return validModifiers;
	}

}

