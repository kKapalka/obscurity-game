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

	List<string> uniqueModifiers=new List<string>();

	public List<string> getUniqueMods(){
		return uniqueModifiers;
	}
	public void addToUniqueMods(string mod){
		uniqueModifiers.Add (mod);
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
		if (sm.turntag.text == "Player Turn") {
			for (int i = 4; i <= matches; i++) {
				GetComponent<EquipmentManager> ().AddModifiersOfType ("highmatch"); 
			}
		}
		CharacterStats stats = this.GetComponent<CharacterStats> ();
		float modifiedMatches = matches + ((float)stats.strength.getValue () / 10);
		if (sequence == 1)
			damage = weapon.getDamage (modifiedMatches);
		else
			damage = 6 * Mathf.Pow (1.35f, (modifiedMatches - 2f)) - 1;
		
		int totalDamage = Mathf.RoundToInt(damage * (1.0f+((float)stats.damageMultiplier.getValue()/100)));

		if(uniqueModifiers.Contains("claws of hate"))
			totalDamage*=2;
		if (uniqueModifiers.Contains ("unstable battery"))
			totalDamage = Random.Range (1, Mathf.RoundToInt(totalDamage * 2.5f));

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
