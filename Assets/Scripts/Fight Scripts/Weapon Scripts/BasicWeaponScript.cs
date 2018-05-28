using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BasicWeaponScript:Weapon{

	void Start(){
		base.Start ();
		hints = new string[] {"Select a gem to continue",
			"Select adjacent gem to swap places with."
		};
	}



	public override GameObject[] GetCandidateAI(){
		var shapes = sm.shapes;
		List<GameObject[]> potentialHits=new List<GameObject[]>();
		int j=0, i=0;
		for (j=0; j < Constants.Rows - 2; j++) {
			for (i=0; i < Constants.Columns - 2; i++) {
				GameObject[] hits = new GameObject[4];
				//check horizontal
				Shape sh1 = shapes [i, j].GetComponent<Shape> ();
				Shape sh2 = shapes [i, j+1].GetComponent<Shape> ();
				Shape sh3 = shapes [i, j+2].GetComponent<Shape> ();
				List<int[]> validOptions = new List<int[]>();
				//Test #1:
				// * * ? *
				// * ? X ?
				// * * O *
				// * * O *
				// * ? X ?
				// * * ? *
				if (sh1.IsSameType (sh2)) {
					hits [2] = shapes[i,j];
					hits [3] = shapes[i,j+1];

					if(j>1)validOptions.Add(new int[]{i,j-2});
					if(j>0 && i>0)validOptions.Add(new int[]{i-1,j-1});
					if(j>0 && i<Constants.Columns-1)validOptions.Add(new int[]{i+1,j-1});

					foreach (int[] opt in validOptions) {
						if (shapes [opt[0],opt[1]].GetComponent<Shape> ().IsSameType (sh1)) {
							hits [1] = shapes [i, j - 1];
							hits [0] = shapes [opt[0],opt[1]];
							potentialHits.Add (hits);
						}
					}
					validOptions.Clear ();

					if(j<Constants.Rows-3) validOptions.Add(new int[]{i,j+3});
					if(j<Constants.Rows-2 && i>0) validOptions.Add(new int[]{i-1,j+2});
					if (j < Constants.Rows - 2 && i < Constants.Columns - 1)
						validOptions.Add (new int[]{ i + 1, j + 2 });
					
					foreach (int[] opt in validOptions) {
						if (shapes [opt[0],opt[1]].GetComponent<Shape> ().IsSameType (sh1)) {
							hits [1] = shapes [i, j +2];
							hits [0] = shapes [opt[0],opt[1]];
							potentialHits.Add (hits);
						}
					}
					validOptions.Clear ();
				}
				// * * * *
				// * * O *
				// * ? X ?
				// * * O *
				// * * * *
				else if(sh1.IsSameType(sh3)){
					hits [2] = shapes[i,j];
					hits [3] = shapes[i,j+2];
					if(i>0) validOptions.Add(new int[]{i-1,j+1});
					if(i<Constants.Columns-1) validOptions.Add(new int[]{i+1,j+1});

					foreach (int[] opt in validOptions) {
						if (shapes [opt[0],opt[1]].GetComponent<Shape> ().IsSameType (sh1)) {
							hits [0] = shapes [i, j + 1];
							hits [1] = shapes [opt[0],opt[1]];
							potentialHits.Add (hits);
						}
					}
					validOptions.Clear ();
				}

				//vertical
				sh1 = shapes [i, j].GetComponent<Shape> ();
				sh2 = shapes [i+1, j].GetComponent<Shape> ();
				sh3 = shapes [i+2, j].GetComponent<Shape> ();

				if (sh1.IsSameType (sh2)) {
					hits [2] = shapes[i,j];
					hits [3] = shapes[i+1,j];

					if(i>1)validOptions.Add(new int[]{i-2,j});
					if(i>0 && j>0)validOptions.Add(new int[]{i-1,j-1});
					if(i>0 && j<Constants.Rows-1)validOptions.Add(new int[]{i-1,j+1});

					foreach (int[] opt in validOptions) {
						if (shapes [opt[0],opt[1]].GetComponent<Shape> ().IsSameType (sh1)) {
							hits [1] = shapes [i-1, j];
							hits [0] = shapes [opt[0],opt[1]];
							potentialHits.Add (hits);
						}
					}
					validOptions.Clear ();

					if(i<Constants.Rows-3) validOptions.Add(new int[]{i+3,j});
					if(i<Constants.Rows-2 && j>0) validOptions.Add(new int[]{i+2,j-1});
					if (i < Constants.Rows - 2 && j < Constants.Columns - 1)
						validOptions.Add (new int[]{ i +2, j + 1 });

					foreach (int[] opt in validOptions) {
						if (shapes [opt[0],opt[1]].GetComponent<Shape> ().IsSameType (sh1)) {
							hits [1] = shapes [i+2, j];
							hits [0] = shapes [opt[0],opt[1]];
							potentialHits.Add (hits);
						}
					}
					validOptions.Clear ();
				}
				// * * * *
				// * * O *
				// * ? X ?
				// * * O *
				// * * * *
				else if(sh1.IsSameType(sh3)){
					hits [2] = shapes[i,j];
					hits [3] = shapes[i+2,j];
					if(j>0) validOptions.Add(new int[]{i+1,j-1});
					if(j<Constants.Columns-1) validOptions.Add(new int[]{i+1,j+1});

					foreach (int[] opt in validOptions) {
						if (shapes [opt[0],opt[1]].GetComponent<Shape> ().IsSameType (sh1)) {
							hits [1] = shapes [i+1, j];
							hits [0] = shapes [opt[0],opt[1]];
							potentialHits.Add (hits);
						}
					}
					validOptions.Clear ();
				}
			}
		}
		return potentialHits.ElementAt(Random.Range(0,potentialHits.Count()));
	}

	public override IEnumerable<GameObject> GetAllMatches(GameObject go1, GameObject go2)
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
	public override bool CheckConditions (GameObject go1, GameObject go2)
	{
		Shape h1 = go1.GetComponent<Shape> ();
		Shape h2 = go2.GetComponent<Shape> ();
		return go1!=go2 && ((h1.Column == h2.Column ||
			h1.Row == h2.Row)
			&& Mathf.Abs(h1.Column - h2.Column) <= 1
			&& Mathf.Abs(h1.Row - h2.Row) <= 1);
	}
	public override void HighlightSelection (GameObject go)
	{
		List<Vector3> positions = new List<Vector3> (); 
		int posX = go.GetComponent<Shape> ().Column;
		int posY = go.GetComponent<Shape> ().Row;
		GameObject marker;
		if (posX > 0)
			positions.Add (sm.shapes [posY, posX - 1].transform.position);
		if (posY > 0)
			positions.Add (sm.shapes [posY - 1, posX].transform.position);
		if (posX < Constants.Columns - 1)
			positions.Add (sm.shapes [posY, posX + 1].transform.position);
		if (posY < Constants.Rows - 1)
			positions.Add (sm.shapes [posY + 1, posX].transform.position);
		positions.Add (sm.shapes [posY, posX].transform.position);

		foreach (Vector3 position in positions) {
			marker = Instantiate (sm.MarkerPrefabs [0], position, Quaternion.identity) as GameObject;
			markers.Add (marker);
		}
		Color c = go.GetComponent<SpriteRenderer> ().color;
		c.a = 0.6f;
		go.GetComponent<SpriteRenderer> ().color = c;
	}
	public override float getDamage (float matchedGems)
	{
		return 12 * Mathf.Pow (1.25f, (matchedGems - 2f));
	}
	public override IEnumerator PerformAttack (GameObject go1, GameObject go2)
	{
		sm.shapes.Swap(go1, go2);
		go1.transform.positionTo(Constants.AnimationDuration, go2.transform.position);
		go2.transform.positionTo(Constants.AnimationDuration, go1.transform.position);
		yield return new WaitForSeconds(Constants.AnimationDuration);

		var hitGomatchesInfo = getMatches(go1,go2);
		var hitGo2matchesInfo = getMatches (go2,go1);

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
