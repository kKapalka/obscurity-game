using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : PlayerScript {
	EquipmentManager em;
	public GameObject hintPanel;
	Text hintText;
	int[] XP = new int[2];
	GameState savedState;

	void onStateSwitch(){
		if (sm.getState () != savedState) {
			if (sm.turntag.text == "Player Turn") {
				switch (sm.getState ()) {
				case GameState.None:
					hintText.text = weapon.getHint (0);
					break;
				case GameState.SelectionStarted:
					hintText.text = weapon.getHint (1);
					break;
				default:
					hintText.text = "";
					break;
				}
			}
			savedState = sm.getState ();
		}
	}

	void Awake(){
		hintText = hintPanel.GetComponentInChildren<Text> ();
		em = GetComponent<EquipmentManager> ();
		XP = ReadScript.Read<int[]> ("PlayerXP");
		if(XP==default (int[])){
			XP = new int[2];
			XP[0]=1;
			XP[1]=0;
		}
		GetComponent<CharacterStats>().maximumHP = Mathf.RoundToInt((float)GetComponent<CharacterStats>().maximumHP*(Mathf.Pow(1+(float)(XP [0]-1)/4.0f,2)));
		GetComponent<CharacterStats>().removeAllModifiers();
		GetComponent<CharacterStats>().damageMultiplier.AddModifier(XP[0]*15);
		em.AwakeOnFight ();

	}

	public int[] getXP(){
		return XP;
	}

	void Update()
	{
		
		onStateSwitch ();

		if (sm.turntag.text == "Player Turn" && sm.getState () != GameState.Animating && !ShapesManager.gameOver) {
			if (Input.GetMouseButtonDown (0)) {
				GameState state = sm.getState ();
				if (state == GameState.None || selectedGem1 == null ) {
					
					var hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
					if (hit.collider != null && hit.collider.tag == "Gem") {
						sm.setState (GameState.SelectionStarted);
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
