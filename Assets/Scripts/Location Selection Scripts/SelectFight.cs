using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectFight : MonoBehaviour {

	public Text titleText;
	public Text infoText;
	bool loaded=false;
	public GameObject enemyData;

	public void Load(EnemySelection enemy){
		titleText.text = enemy.encounterName;
		infoText.text = enemy.infoAboutEnemy;
	}
}
