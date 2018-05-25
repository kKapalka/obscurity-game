using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerScript : MonoBehaviour {
	
	protected ShapesManager sm;

	public string weaponType="basic";
	protected Weapon weapon;

	protected GameObject selectedGem1, selectedGem2;

	void Start(){
		LoadWeapon(weaponType);
		sm = (ShapesManager)GameObject.Find ("ShapesManager").GetComponent ("ShapesManager");

	}
	// Update is called once per frame


	public void LoadWeapon(string type){
		weaponType = type;
		switch (type) {
		case "hammer":
			weapon =  gameObject.AddComponent<HammerScript>() as HammerScript;
			break;
		case "scattershot":
			weapon = gameObject.AddComponent<ScattershotScript>() as ScattershotScript;
			break;
		case "doubleattack":
			weapon = gameObject.AddComponent<DoubleAttack>() as DoubleAttack;
			break;
		case "basic":
			weapon = gameObject.AddComponent<BasicWeaponScript>() as BasicWeaponScript;
			break;
		case "dragndrop":
			weapon = gameObject.AddComponent<DragNDropScript>() as DragNDropScript;
			break;
		case "dragthrough":
			weapon = gameObject.AddComponent<DragThroughScript>() as DragThroughScript;
			break;
		}
	}
	public abstract void Attack ();

	public void DealDamage(int matches, int type, GameObject enemy, int sequence){
		if (matches < weapon.getMinimumMatches())
			return;
		float damage = 0;
		CharacterStats stats = this.GetComponent<CharacterStats> ();
		float modifiedMatches = matches + ((float)stats.strength.getValue () / 10);
		if (sequence == 1)
			damage = weapon.getDamage (modifiedMatches);
		else
			damage = 8 * (modifiedMatches-2);
		
		int totalDamage = Mathf.RoundToInt(damage * (1.0f+((float)stats.damageMultiplier.getValue()/100)));
		if (totalDamage > 200 && sm.turntag.text == "Player Turn")
			GetComponent<EquipmentManager> ().AddModifiersOfType ("highdamage"); 
		enemy.GetComponent<CharacterStats> ().TakeDamage (totalDamage, type);
	}

	public Weapon getWeapon(){
		return weapon;
	}

	public void RefreshSelection(){
		selectedGem1 = null;
		selectedGem2 = null;
	}
}
