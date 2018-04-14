using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour {

	public void QuitGame(){
		Debug.Log ("Game has just quitted.");
		Application.Quit ();
	}
}
