using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelButton : MonoBehaviour {

	public GameObject lastBoss;
	void Start () {
		if (lastBoss != null && !(lastBoss.GetComponent<Image> ().color == Color.green)) {
			GetComponent<Button> ().interactable = false;
			GetComponentInChildren<Text> ().color = new Color (.9f, .6f, .3f);
		}
	}

}
