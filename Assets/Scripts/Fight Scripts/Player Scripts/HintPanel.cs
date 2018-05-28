using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintPanel : MonoBehaviour {

	public GameObject hintPanel;

	public void Toggle(){
		hintPanel.SetActive (!hintPanel.activeSelf);
		GetComponentInChildren<Text>().text = (hintPanel.activeSelf ? "Hide" : "Show") + " hints";
	}
}
