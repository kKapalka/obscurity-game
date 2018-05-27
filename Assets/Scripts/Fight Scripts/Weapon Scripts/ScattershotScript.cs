using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScattershotScript :Weapon{


	public override GameObject[] GetCandidateAI(){
		var shapes = sm.shapes;
		int matches = 0;

		GameObject candidate = null;
		while (candidate == null) {
			int i = Random.Range (0, Constants.Columns);
			int j = Random.Range (0, Constants.Rows);
			matches = GetAllMatches (shapes[j, i],shapes[j,i]).Count();
			if (matches >= getMinimumMatches ())
				candidate= shapes [j,i];
		}
		return new GameObject[]{candidate,candidate};
	}

	public List<GameObject> GetAllPotentialMatches(GameObject go){
		
		var shapes = sm.shapes;
		List<GameObject> potentialMatches = new List<GameObject> ();
		var shape = go.GetComponent<Shape> ();

		for (int i = 0; i < Constants.Rows; i++) {
			for (int j = 0; j < Constants.Columns; j++) {
				if (shapes [i, j].GetComponent<Shape> ().IsSameType (shape)) {
					potentialMatches.Add (shapes [i, j]);
				}
			}
		}
		potentialMatches.Remove (go);
		return potentialMatches;
	}

	public override IEnumerable<GameObject> GetAllMatches(GameObject go1,GameObject go2)
	{
		
		List<GameObject> matches = new List<GameObject> ();
		var potentialMatches = GetAllPotentialMatches(go1);
		matches.Add(go1);

		if (!(potentialMatches.Count () >getMinimumMatches())) {
			matches.Clear ();
		} else {
			while (matches.Count () < getMinimumMatches()) {
				GameObject gemToAdd = potentialMatches.ElementAt (Random.Range (0, potentialMatches.Count ()));
				matches.Add (gemToAdd);
				potentialMatches.Remove (gemToAdd);
			}
		}
		return matches.Distinct();
	}

	public override bool CheckConditions (GameObject go1, GameObject go2)
	{
		return go1 == go2;
	}
	public override float getDamage (float matchedGems)
	{
		return 6 * Mathf.Pow (1.45f, (matchedGems - 2f))+2;
	}

	public override void HighlightSelection (GameObject go)
	{
		List<GameObject> targets = GetAllPotentialMatches (go);
		GameObject marker;
		foreach (GameObject target in targets) {
			marker = Instantiate (sm.MarkerPrefabs [1], target.transform.position, Quaternion.identity) as GameObject;
			markers.Add (marker);
		}
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
			foreach (GameObject pos in totalMatches) {
				StartCoroutine (sm.StartSpriteEffectCoroutine (sm.EffectPrefabs [2], pos.transform.position, Quaternion.identity));
			}
			yield return sm.BreakMatchesAndGravity (totalMatches);
			sm.processEndOfTurn ();
		} else {
			sm.setState (GameState.None);
		}
	} 

}
