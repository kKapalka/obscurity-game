using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon:MonoBehaviour{
	
	protected string[] hints;
	int minimumMatches;
	protected List<GameObject> markers;
	protected ShapesManager sm;
	public void Start(){
		setHints ();
		while(sm==null)
			sm = GameObject.Find ("ShapesManager").GetComponent<ShapesManager> ();
		markers = new List<GameObject>();
		minimumMatches = 3;
	}

	public abstract void setHints();

	public string getHint(int i){
		return hints [i];
	}

	public MatchesInfo getMatches(GameObject go1, GameObject go2){

		MatchesInfo matchesInfo = new MatchesInfo();
		var matches=GetAllMatches(go1,go2);
		matchesInfo.AddObjectRange (matches);
		return matchesInfo;

	}

	public int getMinimumMatches(){
		return minimumMatches;
	}
	public List<GameObject> getMarkers(){
		return markers;
	}
	public void RemoveSelection (GameObject go){
		if (markers != null){
			foreach(GameObject marker in markers){
				if(marker!=null)Destroy (marker);
			}
			markers.Clear ();
		}
		Color c = go.GetComponent<SpriteRenderer>().color;
		c.a = 1f;
		go.GetComponent<SpriteRenderer>().color = c;
	}

	public abstract IEnumerable<GameObject> GetAllMatches(GameObject go1, GameObject go2);

	public abstract GameObject[] GetCandidateAI ();

	public abstract void HighlightSelection (GameObject go);

	public abstract float getDamage(float matchedGems);

	public abstract IEnumerator PerformAttack (GameObject go1, GameObject go2);

	public abstract bool CheckConditions(GameObject go1, GameObject go2);
}
