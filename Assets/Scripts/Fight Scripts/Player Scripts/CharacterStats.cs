using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats: MonoBehaviour {

	public Slider PlayerHP;
	public Text HPText;

	public int maximumHP;
	public int currentHP;
	public Stat[] resistances;
	public Stat dodge;
	public Stat damageMultiplier;
	public Stat strength;
	public Stat regeneration;

    

    
	void Awake(){
		GetComponent<PopupTextContoroller>().Initialize ();

		ResetHP ();
		strength.setCap (0, 100);
		dodge.setCap (0, 80);
		damageMultiplier.setCap (-75, 500);
		regeneration.setCap (-80, 80);
		foreach (Stat res in resistances)
			res.setCap (-500, 100);
	}

	public void ResetHP(){
		currentHP = maximumHP;
		PlayerHP.maxValue = maximumHP;
		UpdateHP ();
	}
	void UpdateHP(){
		HPText.text = currentHP.ToString()+"/"+maximumHP;
		PlayerHP.value = currentHP;
	}
	public void TakeDamage(int damage, int type){
		int damageTaken=0;
		if (type < resistances.Length) {
			damageTaken= Mathf.Clamp (Mathf.RoundToInt((float)damage * (1.0f - ((float)resistances [type].getValue()/100))), 0, maximumHP);
		} else {
			damageTaken= Mathf.Clamp (damage, 0, maximumHP);
		}
		if (damageTaken > 0) {
			if (UnityEngine.Random.Range (0, 100) > dodge.getValue ()) {
				currentHP -= damageTaken;
				GetComponent<PopupTextContoroller> ().CreatePopupText (("-" + damageTaken.ToString ()), transform);
			}
			else {
				GetComponent<PopupTextContoroller> ().CreatePopupText ("DODGE", transform);
			}
		}
		UpdateHP ();
	}

	public void DisplayCharacterStats(){
		string fullText="";
		for (int i=0;i<resistances.Length;i++) {
			Stat resist = resistances [i];
			if (resist.getValue () != 0) {
				fullText +=(resist.getValue () > 0 ? "-" : "+") + Mathf.Abs (resist.getValue ()) + "% DMG taken from " + GameObject.Find ("ShapesManager").GetComponent<ShapesManager> ().GemTypes [i] + ".\n";
			}
		}
		if (dodge.getValue () != 0) {
			fullText +=Mathf.Abs (dodge.getValue ()) +"% chance to avoid damage.\n\n";
		}
		if (regeneration.getValue () != 0) {
			fullText +=(regeneration.getValue () > 0 ? "Gains " : "Loses ")+Mathf.Abs (regeneration.getValue ()) +" HP at the start of the turn.\n\n";
		}
		if (damageMultiplier.getValue () != 0 ) {
			if (damageMultiplier.getValue () != 0)
				fullText += "Deals "+Mathf.Abs (damageMultiplier.getValue ())+(damageMultiplier.getValue()>0?"% more ":"% less ")+ "damage.\n";
		}
		if (strength.getValue () != 0) {
			fullText += "Deals +" + (float)strength.getValue () / 10 + " gems worth of damage.\n";
		}
		fullText += "\nAttacks by: ";
		string weaponType = GetComponent<PlayerScript> ().weaponType;
		switch (weaponType) {
		case "basic":
			fullText += "swapping adjacent gems.\n";
			break;
		case "dragndrop":
			fullText += "swapping any two gems.\n";
			break;
		case "dragthrough":
			fullText += "dragging a gem along an axis.\n";
			break;
		case "hammer":
			fullText += "breaking gems in a 3x3 square.\n";
			break;
		case "scattershot":
			fullText += "breaking random gems of the same type.\n";
			break;
		case "doubleattack":
			fullText += "breaking gems in a line.\n";
			break;
		}
		GetComponentInChildren<Button> ().GetComponentInChildren<Text> ().text = fullText;
	}
	public void HideCharacterStats(){
		string fullText = "";
		GetComponentInChildren<Button> ().GetComponentInChildren<Text> ().text = fullText;
	}
	public void Regenerate(){
		currentHP = Mathf.Clamp ((currentHP + regeneration.getValue()), 0, maximumHP);
		if (regeneration.getValue() != 0 && currentHP>0)
			GetComponent<PopupTextContoroller>().CreatePopupText (((regeneration.getValue()>0?"+":"") + regeneration.getValue().ToString ()), transform);
		UpdateHP ();
	}

	public void removeAllModifiers(){
		for (int i = 0; i < resistances.Length; i++)
			resistances [i].RemoveAll ();
		dodge.RemoveAll ();
		damageMultiplier.RemoveAll ();
		regeneration.RemoveAll ();
		strength.RemoveAll ();

	}

    
    
}
