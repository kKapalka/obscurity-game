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
	public Stat[] resistances = new Stat[]{new Stat(-300,60),new Stat(-300,60),new Stat(-300,60),new Stat(-300,60),new Stat(-300,60)};
	public Stat dodge = new Stat (0, 50);
	public Stat damageMultiplier = new Stat (-75, 400);
	public Stat strength = new Stat (-40, 40);
	public Stat regeneration = new Stat (-50, 50);

    

    
	void Awake(){
		GetComponent<PopupTextContoroller>().Initialize ();

		ResetHP ();
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
			
			if (UnityEngine.Random.Range (1, 100) > Mathf.Abs(dodge.getValue ())) {
				currentHP -= damageTaken;
				GetComponent<PopupTextContoroller> ().CreatePopupText (("-" + damageTaken.ToString ()), transform);
			}
			else {
				if (dodge.getValue() > 0)
					GetComponent<PopupTextContoroller> ().CreatePopupText ("DODGE", transform);
				else {
					currentHP -= damageTaken*2;
					GetComponent<PopupTextContoroller> ().CreatePopupText (("-" + (damageTaken*2).ToString ()+"!!"), transform);
				}
			}
		}
		UpdateHP ();
	}

	public void HideCharacterStats(){
		string fullText = "";
		GetComponentInChildren<Button> ().GetComponentInChildren<Text> ().text = fullText;
	}
	public void Regenerate(){
		int regenCap = Mathf.RoundToInt ((float)maximumHP / 12f);
		int regenHP = Mathf.Clamp (regeneration.getValue (), regenCap * -1, regenCap);
		currentHP = Mathf.Clamp ((currentHP + regenHP), 0, maximumHP);
		if (regeneration.getValue() != 0 && currentHP>0)
			GetComponent<PopupTextContoroller>().CreatePopupText (((regenHP>0?"+":"") + regenHP.ToString ()), transform);
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
	public void ResetStats(){
		for (int i = 0; i < resistances.Length; i++)
			resistances [i].setValue (0);
		dodge.setValue (0);
		damageMultiplier.setValue (0);
		regeneration.setValue (0);
		strength.setValue (0);

	}
    
    
}
