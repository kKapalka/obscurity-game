using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : PlayerScript {

	float time=0;


	// Update is called once per frame
	void Update () {
		if (sm.turntag.text == "Enemy Turn" && sm.getState () != GameState.Animating && !ShapesManager.gameOver) {
			if (time < 1f){
				time += Time.deltaTime;
				selectedGem1 = null;
			}
			else {
				if (selectedGem1==null) {
					sm.setState (GameState.SelectionStarted);
					var AISelectedGems=weapon.GetCandidateAI();
					selectedGem1 = AISelectedGems [0];
					weapon.HighlightSelection (selectedGem1);
					selectedGem2 = AISelectedGems [1];
				}
				if (time < 2.0f) {
					time += Time.deltaTime;
				} else {
					Attack ();
					time = 0;
					//selectedGem1 = null;
				}
			}
		}
	}


	public override void Attack(){
		sm.hintText.text = "";
		sm.setState (GameState.SelectionStarted);
		weapon.RemoveSelection (selectedGem1);
			if (weapon.CheckConditions (selectedGem1, selectedGem2)) {
				sm.setState (GameState.Animating);
				sm.FixSortingLayer (selectedGem1, selectedGem2);
				StartCoroutine (weapon.PerformAttack (selectedGem1, selectedGem2));
			}
	}

}
