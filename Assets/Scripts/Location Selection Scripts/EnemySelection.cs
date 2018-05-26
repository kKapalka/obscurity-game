using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class EnemySelection : MonoBehaviour {

	public string encounterName;
	public string infoAboutEnemy;

	public int maximumHP;
	public int experiencePoints;
	public Stat[] resistances = new Stat[]{new Stat(-300,75),new Stat(-300,75),new Stat(-300,75),new Stat(-300,75),new Stat(-300,75)};
	public Stat dodge = new Stat (0, 85);
	public Stat damageMultiplier = new Stat (-75, 500);
	public Stat strength = new Stat (0, 50);
	public Stat regeneration = new Stat (-80, 80);	
	public string weapon;
	public static bool created=false;
	public static EnemySelection Instance {
		get;
		set;
	}

	void Start(){
		List<string> encountersDefeated = ReadScript.Read<List<string>>("Progress");
		if (encountersDefeated == default (List<string>))
			return;
		if(encountersDefeated.Contains(this.encounterName) && Application.loadedLevelName=="Location Selection"){
			this.GetComponent<Image> ().color = Color.green;
		}
	}

	public void LoadIntoCharacter(GameObject character){
		CharacterStats stats = character.GetComponent<CharacterStats> ();
		stats.maximumHP = maximumHP;
		stats.resistances = resistances;
		stats.dodge = dodge;
		stats.damageMultiplier = damageMultiplier;
		stats.strength = strength;
		stats.regeneration = regeneration;
		stats.ResetHP ();
		character.GetComponent<PlayerScript> ().LoadWeapon (weapon);
	}
	public void Display(){
		GameObject sf =	GameObject.Find ("NextMissionPanel");
		sf.GetComponent<SelectFight>().Load (this.GetComponent<EnemySelection>());
		DontDestroyOnLoad (this.gameObject);
		Create ();
	}
	public void Create(){
		created = true;
		Instance = this;
	}
}
