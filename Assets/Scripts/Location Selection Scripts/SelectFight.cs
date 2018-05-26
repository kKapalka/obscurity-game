using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectFight : MonoBehaviour {

	public Text titleText;
	public Text infoText;
	public GameObject enemyData;

	string[] adjectives = new string[]{"", "Quite", "Pretty", "Very", "Insanely" };
	string[] types = new string[]{ "blue","green","orange","red","purple" };
	public void Load(EnemySelection enemy){
		titleText.text = enemy.encounterName;
		infoText.text = enemy.infoAboutEnemy;
		infoText.text += "\n\n\n\nHealth: " + enemy.maximumHP + "\nEXP gain: " + enemy.experiencePoints;
		infoText.text += "\n\n";
		if (adjectives [Mathf.Clamp(enemy.dodge.getValue() / 20,0,adjectives.Length-1)] != "")
			infoText.text += adjectives [Mathf.Clamp(enemy.dodge.getValue() / 20,0,adjectives.Length-1)] + " agile.\n";
		if (adjectives [Mathf.Clamp(enemy.strength.getValue() / 8,0,adjectives.Length-1)] != "")
			infoText.text += adjectives [Mathf.Clamp(enemy.strength.getValue() / 6,0,adjectives.Length-1)] + " strong.\n";
		if (adjectives [Mathf.Clamp(enemy.damageMultiplier.getValue() / 30,0,adjectives.Length-1)] != "")
			infoText.text += adjectives [Mathf.Clamp(enemy.damageMultiplier.getValue() / 60,0,adjectives.Length-1)] + " lethal.\n";
		for (int i = 0; i < enemy.resistances.Length; i++) {
			if (adjectives [Mathf.Clamp(enemy.resistances[i].getValue() / 20,0,adjectives.Length-1)] != "")
				infoText.text += adjectives [Mathf.Clamp(enemy.resistances[i].getValue() / 20,0,adjectives.Length-1)] + " resistant against "+types[i]+" gems.\n";
		}
	}
}
