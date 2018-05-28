using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoubleAttack : Weapon
{
   
	public override void setHints ()
	{
		hints = new string[] {"Select a gem to continue. Gems of this color will be destroyed.",
			"Select a gem along an axis."
		};
	}
		

    public override GameObject[] GetCandidateAI()
    {
        var shapes = sm.shapes;
        bool foundAValidMove = false;
        List<GameObject[]> matches = new List<GameObject[]>();
		int match;
        while (!foundAValidMove)
        {
            for (int i = 0; i < Constants.Columns-1; i++)
            {
                for (int j = 0; j < Constants.Rows-1; j++)
                {
					match = GetAllMatches(shapes[j, i],shapes[j,i+1]).Count();
                    if (match >= getMinimumMatches())
                    {
                        foundAValidMove = true;
						int k = Random.Range(0,Constants.Rows);
						while (k == i)
							k = Random.Range (0, Constants.Rows);
						matches.Add (new GameObject[]{shapes[j,i],shapes[j,k] });
                    }
					match = GetAllMatches(shapes[j, i],shapes[j+1,i]).Count();
					if (match >= getMinimumMatches())
					{
						foundAValidMove = true;
						int k = Random.Range(0,Constants.Rows);
						while (k == j)
							k = Random.Range (0, Constants.Rows);
						matches.Add (new GameObject[]{shapes[j,i],shapes[k,i] });
					}
                }
            }
            if (!foundAValidMove)
                sm.InitializeCandyAndSpawnPositions();
        }
		return matches.ElementAt (Random.Range (0, matches.Count ()));
    }
		

	public override IEnumerable<GameObject> GetAllMatches(GameObject go1, GameObject go2)
    {
        var shapes = sm.shapes;
        List<GameObject> matches = new List<GameObject>();
        var shape = go1.GetComponent<Shape>();
        matches.Add(go1);

		for (int i = 0; i < Constants.Columns; i++) {
			if (go1.GetComponent<Shape>().Column==go2.GetComponent<Shape>().Column) {
				if (shapes[i, shape.Column].GetComponent<Shape>().IsSameType(shape))
				{
					matches.Add(shapes[i, shape.Column]);
				}
			}
			else if (go1.GetComponent<Shape>().Row==go2.GetComponent<Shape>().Row) {
				if (shapes[shape.Row, i].GetComponent<Shape>().IsSameType(shape))
				{
					matches.Add(shapes[shape.Row, i]);
				}
			}
		}
        return matches.Distinct();
    }

	public override bool CheckConditions (GameObject go1, GameObject go2)
	{
		return go1!=go2 && (go1.GetComponent<Shape> ().Column == go2.GetComponent<Shape> ().Column ||
		go1.GetComponent<Shape> ().Row == go2.GetComponent<Shape> ().Row);
	}

	public override void HighlightSelection (GameObject go)
	{
		Vector3 posit = go.transform.position;
		Vector3 middle = (sm.shapes [0, 0].transform.position + sm.shapes [Constants.Columns - 1, Constants.Rows - 1].transform.position) / 2;

		Vector3 X = new Vector3 (posit.x, middle.y, posit.z);
		GameObject marker = Instantiate (sm.MarkerPrefabs [1], X, Quaternion.identity) as GameObject;
		Vector3 sc = marker.transform.localScale;
		marker.transform.localScale = new Vector3 (sc.x, sc.y * 8, sc.z);
		markers.Add (marker);

		Vector3 Y = new Vector3 (middle.x, posit.y, posit.z);
		marker = Instantiate (sm.MarkerPrefabs [1], Y, Quaternion.identity) as GameObject;
		sc = marker.transform.localScale;
		marker.transform.localScale = new Vector3 (sc.x * 8, sc.y, sc.z);
		markers.Add (marker);
		Color c = go.GetComponent<SpriteRenderer> ().color;
		c.a = 0.6f;
		go.GetComponent<SpriteRenderer> ().color = c;
	}

	public override IEnumerator PerformAttack (GameObject go1, GameObject go2)
	{
		Vector3 posit;
		Quaternion rot;
		Vector3 middle = (sm.shapes [0, 0].transform.position + sm.shapes [Constants.Columns - 1, Constants.Rows - 1].transform.position) / 2;
		IEnumerable<GameObject> totalMatches;
		if (go1.GetComponent<Shape> ().Column == go2.GetComponent<Shape> ().Column) {
			posit = new Vector3 (go1.transform.position.x, middle.y, go1.transform.position.z);
			rot = Quaternion.Euler (0, 0, -90);

		} else { 
			posit = new Vector3 (middle.x,go1.transform.position.y, go1.transform.position.z);
			rot = Quaternion.Euler (0, 0, 00);
		}
		totalMatches = getMatches (go1,go2).MatchedCandy;
		if (totalMatches.Count () >= getMinimumMatches ()) {
			StartCoroutine (sm.StartSpriteEffectCoroutine (sm.EffectPrefabs [0], posit, Quaternion.identity * rot));
			yield return sm.BreakMatchesAndGravity (totalMatches);
			sm.processEndOfTurn ();
		} else {
			sm.setState (GameState.None);
		}
	}

	public override float getDamage (float matchedGems)
	{
		
		return 12 * Mathf.Pow (1.25f, (matchedGems - 2f));
	}
}