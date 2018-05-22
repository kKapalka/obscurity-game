using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DragThroughScript : Weapon {

	public override bool CheckConditions (GameObject go1, GameObject go2)
	{
		return go1!=go2 && (go1.GetComponent<Shape> ().Column == go2.GetComponent<Shape> ().Column ||
			go1.GetComponent<Shape> ().Row == go2.GetComponent<Shape> ().Row);
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
		for (int j = 0; j < Constants.Rows - 2; j++) {
			for (int i = 0; i < Constants.Columns - 2; i++) {
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
					if (j > 0) {
						for (int k = 0; k < Constants.Columns; k++) {
							validOptions.Add (new int[]{ k, j - 1 });
						}
					}
					foreach (int[] opt in validOptions) {

						if (shapes [opt[0],opt[1]].GetComponent<Shape> ().IsSameType (sh1)) {
							hits [1] = shapes [i, j - 1];
							hits [0] = shapes [opt[0],opt[1]];
							potentialHits.Add (hits);

						}
					}
					validOptions.Clear ();
					if (j <Constants.Rows-2) {
						for (int k = 0; k < Constants.Columns; k++) {
							validOptions.Add (new int[]{ k, j + 2 });
						}
					}
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
					for (int k = 0; k < Constants.Columns; k++) {
						validOptions.Add (new int[]{ k, j +1 });
					}
					foreach (int[] opt in validOptions) {
						if (shapes [opt[0],opt[1]].GetComponent<Shape> ().IsSameType (sh1)) {
							hits [1] = shapes [i, j + 1];
							hits [0] = shapes [opt[0],opt[1]];
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
					if (i > 0) {
						for (int k = 0; k < Constants.Rows; k++) {
							validOptions.Add (new int[]{ i - 1, k });
						}
					}
					foreach (int[] opt in validOptions) {
						if (shapes [opt[0],opt[1]].GetComponent<Shape> ().IsSameType (sh1)) {
							hits [1] = shapes [i-1, j];
							hits [0] = shapes [opt[0],opt[1]];
							potentialHits.Add (hits);
						}
					}
					validOptions.Clear ();
					if (i < Constants.Columns - 2) {
						for (int k = 0; k < Constants.Rows; k++) {
							validOptions.Add (new int[]{ i + 2, k });
						}
					}
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
					for (int k = 0; k < Constants.Rows; k++) {
						validOptions.Add (new int[]{ i+1, k });
					}
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
	public override float getDamage (float matchedGems)
	{
		float match_damage = 4 * (matchedGems - 2) * (matchedGems / 2);
		return match_damage + 3;
	}
	public override void HighlightSelection (GameObject go)
	{
		List<Vector3> positions = new List<Vector3> (); 
		int posX = go.GetComponent<Shape> ().Column;
		int posY = go.GetComponent<Shape> ().Row;
		GameObject marker;
		for (int i = 0; i < Constants.Rows; i++)
			positions.Add (sm.shapes [i, posX].transform.position);
		for (int i = 0; i < Constants.Columns; i++)
			positions.Add (sm.shapes [posY, i].transform.position);
		foreach (Vector3 position in positions) {
			marker = Instantiate (sm.MarkerPrefabs [0], position, Quaternion.identity) as GameObject;
			markers.Add (marker);
		}
		Color c = go.GetComponent<SpriteRenderer> ().color;
		c.a = 0.6f;
		go.GetComponent<SpriteRenderer> ().color = c;
	}
	public override IEnumerator PerformAttack (GameObject go1, GameObject go2)
	{
		var shapes = sm.shapes;
		List<MatchesInfo> allMatches = new List<MatchesInfo> ();
		var totalMatches = new List<GameObject> ();
		bool validMove = false;
		if (go1.GetComponent<Shape> ().Column == go2.GetComponent<Shape> ().Column) {
			int column = go1.GetComponent<Shape> ().Column;
			int beginning = go1.GetComponent<Shape> ().Row;
			int end = go2.GetComponent<Shape> ().Row;

			Vector3[] positions = new Vector3[Constants.Rows];

			//z dolu do gory
			if (beginning < end) {
				positions [beginning] = shapes [end, column].transform.position;
				for (int i = beginning + 1; i <= end; i++) {
					positions [i] = shapes [i - 1, column].transform.position;
				}
				for (int i = beginning; i < end; i++) {
					shapes.Swap (shapes [i, column], shapes [i + 1, column]);
					shapes [i, column].transform.positionTo (Constants.AnimationDuration, positions [i + 1]);
					allMatches.Add (getMatches (shapes [i, column],shapes [i, column]));
				}
				go1.transform.positionTo (Constants.AnimationDuration, positions [beginning]);
				allMatches.Add (getMatches (go1,go1));

				yield return new WaitForSeconds (Constants.AnimationDuration);
				//sprawdza czy sa linie
				foreach (MatchesInfo match in allMatches) {
					if (match.MatchedCandy.Count () >= getMinimumMatches ()) {
						validMove = true;
						totalMatches.AddRange (match.MatchedCandy);
					}
				}
				//jesli nie to wroc na miejsce swoje
				if (!validMove) {
					positions [end] = shapes [beginning, column].transform.position;
					for (int i = end - 1; i >= beginning; i--) {
						positions [i] = shapes [i + 1, column].transform.position;
					}
					for (int i = end; i > beginning; i--) {
						shapes.Swap (shapes [i, column], shapes [i - 1, column]);
						shapes [i, column].transform.positionTo (Constants.AnimationDuration, positions [i - 1]);
					}
					go1.transform.positionTo (Constants.AnimationDuration, positions [end]);
					yield return new WaitForSeconds (Constants.AnimationDuration);
					sm.setState(GameState.None);
				}


				//z gory na dol
			} else {
				positions [beginning] = shapes [end, column].transform.position;
				for (int i = beginning - 1; i >= end; i--) {
					positions [i] = shapes [i + 1, column].transform.position;
				}
				for (int i = beginning; i > end; i--) {
					shapes.Swap (shapes [i, column], shapes [i - 1, column]);
					shapes [i, column].transform.positionTo (Constants.AnimationDuration, positions [i - 1]);
					allMatches.Add (getMatches (shapes [i, column],shapes [i, column]));
				}
				go1.transform.positionTo (Constants.AnimationDuration, positions [beginning]);
				allMatches.Add (getMatches (go1,go1));

				yield return new WaitForSeconds (Constants.AnimationDuration);
				//sprawdza czy sa linie
				foreach (MatchesInfo match in allMatches) {
					if (match.MatchedCandy.Count () >= getMinimumMatches ()) {
						validMove = true;
						totalMatches.AddRange (match.MatchedCandy);
					}
				}

				if (!validMove) {
					positions [end] = shapes [beginning, column].transform.position;
					for (int i = end + 1; i <= beginning; i++) {
						positions [i] = shapes [i - 1, column].transform.position;
					}
					for (int i = end; i < beginning; i++) {
						shapes.Swap (shapes [i, column], shapes [i + 1, column]);
						shapes [i, column].transform.positionTo (Constants.AnimationDuration, positions [i + 1]);
					}
					go1.transform.positionTo (Constants.AnimationDuration, positions [end]);
					yield return new WaitForSeconds (Constants.AnimationDuration);
					sm.setState(GameState.None);
				}
			}


		} else {
			int row = go1.GetComponent<Shape> ().Row;
			int beginning = go1.GetComponent<Shape> ().Column;
			int end = go2.GetComponent<Shape> ().Column;

			Vector3[] positions = new Vector3[Constants.Columns];

			//z lewo na prawo
			if (beginning < end) {
				positions [beginning] = shapes [row, end].transform.position;
				for (int i = beginning+1; i <= end; i++) {
					positions [i] = shapes [row,i-1].transform.position;
				}
				for (int i = beginning; i < end; i++) {
					shapes.Swap (shapes [row,i], shapes [row,i+1]);
					shapes [row,i].transform.positionTo (Constants.AnimationDuration, positions [i+1]);
					allMatches.Add (getMatches (shapes [row,i],shapes [row,i]));
				}
				go1.transform.positionTo(Constants.AnimationDuration,positions[beginning]);
				allMatches.Add (getMatches (go1,go1));

				yield return new WaitForSeconds(Constants.AnimationDuration);
				//sprawdza czy sa linie
				foreach (MatchesInfo match in allMatches) {
					if (match.MatchedCandy.Count () >= getMinimumMatches ()) {
						validMove = true;
						totalMatches.AddRange (match.MatchedCandy);
					}
				}
				//jesli nie to wroc na miejsce swoje
				if (!validMove) {
					positions [end] = shapes [row,beginning].transform.position;
					for (int i = end-1; i >= beginning; i--) {
						positions [i] = shapes [row,i+1].transform.position;
					}
					for (int i = end; i > beginning; i--) {
						shapes.Swap (shapes [row,i], shapes [row,i-1]);
						shapes [row,i].transform.positionTo (Constants.AnimationDuration, positions [i-1]);
					}
					go1.transform.positionTo(Constants.AnimationDuration,positions[end]);
					yield return new WaitForSeconds(Constants.AnimationDuration);
					sm.setState(GameState.None);
				}


				//z prawo na lewo
			} else {
				positions [beginning] = shapes [row,end].transform.position;
				for (int i = beginning-1; i >= end; i--) {
					positions [i] = shapes [row,i+1].transform.position;
				}
				for (int i = beginning; i > end; i--) {
					shapes.Swap (shapes [row,i], shapes [row,i-1]);
					shapes [row,i].transform.positionTo (Constants.AnimationDuration, positions [i-1]);
					allMatches.Add (getMatches (shapes [row,i],shapes [row,i]));
				}
				go1.transform.positionTo(Constants.AnimationDuration,positions[beginning]);
				allMatches.Add (getMatches (go1,go1));

				yield return new WaitForSeconds(Constants.AnimationDuration);
				//sprawdza czy sa linie
				foreach (MatchesInfo match in allMatches) {
					if (match.MatchedCandy.Count () >= getMinimumMatches ()) {
						validMove = true;
						totalMatches.AddRange (match.MatchedCandy);
					}
				}

				if (!validMove) {
					positions [end] = shapes [row,beginning].transform.position;
					for (int i = end+1; i <= beginning; i++) {
						positions [i] = shapes [row,i-1].transform.position;
					}
					for (int i = end; i < beginning; i++) {
						shapes.Swap (shapes [row,i], shapes [row,i+1]);
						shapes [row,i].transform.positionTo (Constants.AnimationDuration, positions [i+1]);
					}
					go1.transform.positionTo(Constants.AnimationDuration,positions[end]);
					yield return new WaitForSeconds(Constants.AnimationDuration);
					sm.setState (GameState.None);
				}
			}
		}
		if(validMove) {
			yield return sm.BreakMatchesAndGravity (totalMatches.Distinct());
			sm.processEndOfTurn ();
		}
	}

}
