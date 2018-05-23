using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DragNDropScript : Weapon {

	public override bool CheckConditions (GameObject go1, GameObject go2)
	{
		return go1 != go2;
	}
	public override IEnumerable<GameObject> GetAllMatches (GameObject go1, GameObject go2)
	{
		var shapes = sm.shapes;
		List<GameObject> matches = new List<GameObject>();
		List<GameObject> matchesV = new List<GameObject>();
		matches.Add(go1);
		var shape = go1.GetComponent<Shape>();
		//check left
		if (shape.Column != 0)
			for (int column = shape.Column - 1; column >= 0; column--)
			{
				if (shapes[shape.Row, column].GetComponent<Shape>().IsSameType(shape))
				{
					matches.Add(shapes[shape.Row, column]);
				}
				else
					break;
			}

		//check right
		if (shape.Column != Constants.Columns - 1)
			for (int column = shape.Column + 1; column < Constants.Columns; column++)
			{
				if (shapes[shape.Row, column].GetComponent<Shape>().IsSameType(shape))
				{
					matches.Add(shapes[shape.Row, column]);
				}
				else
					break;
			}
		//if sides dont have sufficient matches, clear and restart
		if (matches.Count < getMinimumMatches())
			matches.Clear();
		matchesV.Add(go1);
		//check bottom
		if (shape.Row != 0)
			for (int row = shape.Row - 1; row >= 0; row--)
			{
				if (shapes[row, shape.Column] != null &&
					shapes[row, shape.Column].GetComponent<Shape>().IsSameType(shape))
				{
					matchesV.Add(shapes[row, shape.Column]);
				}
				else
					break;
			}

		//check top
		if (shape.Row != Constants.Rows - 1)
			for (int row = shape.Row + 1; row < Constants.Rows; row++)
			{
				if (shapes[row, shape.Column] != null && 
					shapes[row, shape.Column].GetComponent<Shape>().IsSameType(shape))
				{
					matchesV.Add(shapes[row, shape.Column]);
				}
				else
					break;
			}

		if (matchesV.Count < getMinimumMatches())
			matchesV.Clear();
		return matches.Union(matchesV).Distinct();
	
	}
	public override GameObject[] GetCandidateAI ()
	{
		var shapes = sm.shapes;
		List<GameObject[]> potentialHits=new List<GameObject[]>();
		for (int j = 0; j < Constants.Rows-2; j++) {
			for (int i = 0; i < Constants.Columns-2; i++) {
				GameObject[] hits = new GameObject[4];
				Shape sh1 = shapes [i, j].GetComponent<Shape> ();
				Shape sh2 = shapes [i, j+1].GetComponent<Shape> ();
				Shape sh3 = shapes [i, j+2].GetComponent<Shape> ();
				// * * X *
				// * * O *
				// * * O *
				// * * X *
				// * * * *
				if (sh1.IsSameType (sh2)) {
					hits [2] = shapes[i,j];
					hits [3] = shapes[i,j+1];
					if (j == 0)
						hits [1] = shapes [i, j + 2];
					else if (j == Constants.Rows - 1)
						hits [1] = shapes [i, j - 1];
					else{
						int[] options = { j - 1, j + 2 };
						hits [1] = shapes [i, options [Random.Range (0, 2)]];
					}
					GameObject candidate = shapes [Random.Range (0, Constants.Rows), Random.Range (0, Constants.Columns)];

					for (int k = 0; k < 10; k++) {
						if (!(candidate != shapes [i, j] && candidate != shapes [i, j + 1] && candidate.GetComponent<Shape> ().IsSameType (sh1))) {
							candidate = shapes [Random.Range (0, Constants.Rows), Random.Range (0, Constants.Columns)];
						}
					}hits [0] = candidate;

					if(hits[0].GetComponent<Shape>().IsSameType(sh1))potentialHits.Add (hits);
					// * * * *
					// * * O *
					// * * X *
					// * * O *
					// * * * *
				} else if (sh1.IsSameType (sh3)) {
					hits [2] = shapes[i,j];
					hits [3] = shapes[i,j+2];
					hits [1] = shapes[i,j+1];
					GameObject candidate = shapes [Random.Range (0, Constants.Rows), Random.Range (0, Constants.Columns)];

					for (int k = 0; k < 10; k++) {
						if (!(candidate != shapes [i, j] && candidate != shapes [i, j + 2] && candidate.GetComponent<Shape> ().IsSameType (sh1))) {
							candidate = shapes [Random.Range (0, Constants.Rows), Random.Range (0, Constants.Columns)];
						}
					}
					hits [0] = candidate;
					if(hits[0].GetComponent<Shape>().IsSameType(sh1))potentialHits.Add (hits);
				}

				sh1 = shapes [i, j].GetComponent<Shape> ();
				sh2 = shapes [i+1, j].GetComponent<Shape> ();
				sh3 = shapes [i+2, j].GetComponent<Shape> ();

				// * * * *
				// X O O X
				// * * * *
				if (sh1.IsSameType (sh2)) {
					hits [2] = shapes[i, j];
					hits [3] = shapes[i+1, j];
					if (i == 0)
						hits [1] = shapes[i+2, j];
					else if (i == Constants.Rows - 1)
						hits [1] = shapes [i-1, j];
					else {
						int[] options = { i - 1, i + 2 };
						hits [1] = shapes [options [Random.Range (0, 2)],j] ;
					}
					GameObject candidate = shapes [Random.Range (0, Constants.Rows), Random.Range (0, Constants.Columns)];
					for (int k = 0; k < 10; k++) {
						if (!(candidate != shapes [i, j] && candidate != shapes [i + 1, j] && candidate.GetComponent<Shape> ().IsSameType (sh1))) {
							candidate = shapes [Random.Range (0, Constants.Rows), Random.Range (0, Constants.Columns)];
						}
					}
					hits [0] = candidate;
					if(hits[0].GetComponent<Shape>().IsSameType(sh1))potentialHits.Add (hits);

					// * * * * *
					// * O X O *
					// * * * * *
				} else if (sh1.IsSameType (sh3)) {
					hits [2] = shapes[i, j];
					hits [3] = shapes[i+2, j];
					hits [1] = shapes[i+1, j];
					GameObject candidate = shapes [Random.Range (0, Constants.Rows), Random.Range (0, Constants.Columns)];
					for (int k = 0; k < 10; k++) {
						if (!(candidate != shapes [i, j] && candidate != shapes [i + 2, j] && candidate.GetComponent<Shape> ().IsSameType (sh1))) {
							candidate = shapes [Random.Range (0, Constants.Rows), Random.Range (0, Constants.Columns)];
						}
					}
					hits [0] = candidate;
					if(hits[0].GetComponent<Shape>().IsSameType(sh1))potentialHits.Add (hits);
				}

			}
		}
		return potentialHits.ElementAt(Random.Range(0,potentialHits.Count()));
	}
	public override float getDamage (float matchedGems)
	{
		float match_damage = 6 * (matchedGems - 2) * ((matchedGems - 1) / 2);
		return match_damage + 5;
	}
	public override void HighlightSelection (GameObject go)
	{
		Color c = go.GetComponent<SpriteRenderer> ().color;
		c.a = 0.6f;
		go.GetComponent<SpriteRenderer> ().color = c;
	}
	public override IEnumerator PerformAttack (GameObject go1, GameObject go2)
	{
		sm.shapes.Swap(go1, go2);
		go1.transform.positionTo(Constants.AnimationDuration, go2.transform.position);
		go2.transform.positionTo(Constants.AnimationDuration, go1.transform.position);
		yield return new WaitForSeconds(Constants.AnimationDuration);

		var hitGomatchesInfo = getMatches(go1,go1);
		var hitGo2matchesInfo = getMatches(go2,go2);

		var totalMatches = hitGomatchesInfo.MatchedCandy
			.Union(hitGo2matchesInfo.MatchedCandy).Distinct();

		if (totalMatches.Count () < getMinimumMatches ()) {
			sm.shapes.Swap (go1,go2);
			go1.transform.positionTo (Constants.AnimationDuration, go2.transform.position);
			go2.transform.positionTo (Constants.AnimationDuration, go1.transform.position);
			yield return new WaitForSeconds (Constants.AnimationDuration);
			sm.setState (GameState.None);
		} 
		else {
			yield return sm.BreakMatchesAndGravity (totalMatches);
			sm.processEndOfTurn ();
		}
	}

}
