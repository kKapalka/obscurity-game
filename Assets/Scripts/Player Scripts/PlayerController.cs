using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerScript {

	void Update()
	{

		if (sm.turntag.text == "Player Turn" && sm.getState () != GameState.Animating) {
			if (Input.GetMouseButtonDown (0)) {
				GameState state = sm.getState ();
				if (state == GameState.None || selectedGem1 == null) {
					sm.setState (GameState.SelectionStarted);
					var hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
					if (hit.collider != null && hit.collider.tag == "Gem") {
						selectedGem1 = hit.collider.gameObject;
						weapon.HighlightSelection (selectedGem1);
					} else {
						if (selectedGem1 != null) {
							weapon.RemoveSelection (selectedGem1);
							selectedGem1 = null;
						}
					}

				} else if (state == GameState.SelectionStarted && selectedGem1 != null) {
					Attack ();
				}
			} else if (Input.GetMouseButtonDown (1) && sm.getState () == GameState.SelectionStarted) {
				weapon.RemoveSelection (selectedGem1);
				selectedGem1 = null;
			}
		}
	}

	public override void Attack(){
		sm.hintText.text = "";
		sm.setState (GameState.SelectionStarted);
		weapon.RemoveSelection (selectedGem1);

		var hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
		if (hit.collider == null || hit.collider.tag != "Gem") {
			selectedGem1 = null;
			sm.setState (GameState.None);
		} else {
			selectedGem2 = hit.collider.gameObject;
			if (weapon.CheckConditions (selectedGem1, selectedGem2)) {
				sm.setState (GameState.Animating);
				sm.FixSortingLayer (selectedGem1, selectedGem2);
				StartCoroutine (weapon.PerformAttack (selectedGem1, selectedGem2));
			} else {
				weapon.RemoveSelection (selectedGem1);
				selectedGem1 = hit.collider.gameObject;
				weapon.HighlightSelection (selectedGem1);
			}
		}

	}
}
