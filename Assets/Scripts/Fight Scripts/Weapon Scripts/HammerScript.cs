using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class HammerScript: Weapon{

	void Start(){
		base.Start ();
		hints = new string[] {"Select a gem to continue. Gems of this color will be destroyed.",
			"Click once again to confirm."
		};
	}

	public override GameObject[] GetCandidateAI(){
		var sm = GameObject.Find ("ShapesManager").GetComponent<ShapesManager> ();
		var shapes = sm.shapes;
		bool foundAValidMove = false;
		int[,] arrayOfSameTypeCells = new int[Constants.Rows, Constants.Columns];
		while (!foundAValidMove) {
			for (int i = 0; i < Constants.Columns; i++) {
				for (int j = 0; j < Constants.Rows; j++) {
					arrayOfSameTypeCells [j,i] = GetAllMatches (shapes [j,i],shapes[j,i]).Count ();
					if (arrayOfSameTypeCells [j,i] >= getMinimumMatches ())
						foundAValidMove = true;
				}
			}
			if (!foundAValidMove)
				sm.InitializeCandyAndSpawnPositions ();
		}
		GameObject candidate = null;
		while (candidate == null) {
			int i = Random.Range (0, Constants.Columns);
			int j = Random.Range (0, Constants.Rows);
			if (arrayOfSameTypeCells [j,i] >= getMinimumMatches ())
				candidate= shapes [j,i];
		}
		GameObject[] cand = new GameObject[]{ candidate ,candidate};
		return cand;
	}

	public override IEnumerable<GameObject> GetAllMatches(GameObject go1, GameObject go2)
	{
		List<GameObject> matches = new List<GameObject> ();
		var shape = go1.GetComponent<Shape> ();
		matches.Add(go1);
		int col = Correct(go1.GetComponent<Shape>().Column,Constants.Columns-1);
		int row = Correct(go1.GetComponent<Shape> ().Row,Constants.Rows-1);		
		for (int i = row - 1; i <= row + 1; i++) {
			for (int j = col - 1; j <= col + 1; j++) {
				if (sm.shapes [i, j].GetComponent<Shape> ().IsSameType (shape)) {
					matches.Add (sm.shapes [i, j]);
				}
			}
		}		
		return matches.Distinct();
	}

	public override void HighlightSelection(GameObject go){
		int posX = Correct(go.GetComponent<Shape>().Column,Constants.Columns-1);
		int posY = Correct(go.GetComponent<Shape> ().Row,Constants.Rows-1);
		GameObject marker = Instantiate (sm.MarkerPrefabs [1], sm.shapes [posY, posX].transform.position, Quaternion.identity) as GameObject;
		marker.transform.localScale *= 3;
		markers.Add (marker);
		marker = Instantiate (sm.MarkerPrefabs [0], go.transform.position, Quaternion.identity) as GameObject;
		markers.Add (marker);

		Color c = go.GetComponent<SpriteRenderer> ().color;
		c.a = 0.6f;
		go.GetComponent<SpriteRenderer> ().color = c;
	}

	public override IEnumerator PerformAttack (GameObject go1, GameObject go2)
	{
		var totalMatches = getMatches (go1,go2).MatchedCandy;
		if (totalMatches.Count () >= getMinimumMatches ()) {
			sm.StartCoroutine (sm.StartSpriteEffectCoroutine (sm.EffectPrefabs [1], go2.transform.position, Quaternion.identity));
			yield return sm.BreakMatchesAndGravity (totalMatches);
			sm.processEndOfTurn ();
		} else {
			sm.setState (GameState.None);
		}
	}

	public override bool CheckConditions (GameObject go1, GameObject go2)
	{
		return go1 == go2;
	}

	public override float getDamage(float matchedGems){
		return 10 * Mathf.Pow (1.25f, (matchedGems - 2f))+5;
	}

	int Correct(int value, int max){
		if (value == 0)
			return 1;
		else if (value == max)
			return value - 1;
		else
			return value;
	}
}
