using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTextContoroller : MonoBehaviour {

	public PopupText popupText;
	public GameObject canvas;

	public void Initialize(){
		canvas = GameObject.Find ("Canvas");
		if (!popupText)
			popupText = Resources.Load("PopupTextParent") as PopupText;
	}

	public void CreatePopupText(string text, Transform location){
		PopupText instance = Instantiate(popupText);
		instance.onEnable ();
		Vector3 pos = transform.position+new Vector3(Random.Range(-50,50),Random.Range(-100,100));
		instance.transform.parent=canvas.transform;
		instance.transform.position = pos;
		instance.setText (text);
	}
}
