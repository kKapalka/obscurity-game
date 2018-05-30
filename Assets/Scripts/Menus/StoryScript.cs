using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryScript : MonoBehaviour {

	public GameObject Background;
	public Text story;
	public Sprite[] bgs;
	bool appearing=true;
	bool interrupt=false;
	string[] slides=new string[]{
		"Name's Vincent Scottins, and I never had any problems with the law.\n" +
		"In fact, my reputation is well established in the Cartels, and all the\n" +
		"goods I have attained, were procured through legal means.\n" +
		"Obviously, my leisurely lifestyle must have caught the attention of\n" +
		"certain... someones within the city.",

		"Looking back at it, this was the perfect opportunity for them. People\n" +
		"were out, working their butts off in the factories, research labs\n" +
		"or refineries.\n" +
		"I was in a pub, downing my bourbon and pondering on my future\n" +
		"investments. Now this, was hard work - so I thought.\n" +
		"And they appeared out of nowhere - the House of Cards.",

		"'You'll be coming with us, if you please', said one of them.\n" +
		"With their weapons, aiming point-blank at me, what was there to do?\n" +
		"I obeyed...\n" +
		"I could only guess where would they be taking me. \n" +
		"One thing was sure - it would not be pleasant experience. Not. At. All.",

		"We went on, and on, and on, towards the zeppelin station.\n" +
		"Towards my untimely end.\n" +
		"In heat of a moment, an idea sprung up. I tackled one of the gangsters,\n" +
		"kicked the other one. Then...\n" +
		"I took the leap of faith. Jumped past the safety barrier.\n" +
		"To the vast, open skies below.\n"+
		"For a moment, I flew... Thank goodness I had allies in all the good places.\n" +
		"I was retrieved mid-air.",

		"I ended up in the Steel Guts of the city. Machinations constructed here\n" +
		" keep the city afloat, clean, and supplied with electricity.\n" +
		"Only a few actively work here. Other than that, the place is desolate.\n" +
		"I can hide here, and remain in obscurity until the situation stabilizes.\n" +
		"It doesn't mean I'm safe, though. Even now, I am pursued...",

		"'You! Whatcha playin at? Boss said he wants you alive!'\n" +
		"They must have split up. That would explain why this scruffy, unkempt\n" +
		"guy had caught up with me so quickly.\n" +
		"I pick up a rusty pipe and move towards the gangster, with the intent\n" +
		"of giving him a good beating..."
	};

	int sequence=0;

	void Start(){
		Background.GetComponent<Image> ().sprite = bgs [sequence];
		StartCoroutine(Appear(slides[sequence]));
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			GetComponent<EnemySelection> ().Create ();
			SceneManager.LoadScene ("Fight");
		} else if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) {
			if (!appearing) {
				FlipToNextSlide ();
				interrupt = false;
			} else {
				interrupt = true;
			}
		}
	}

	void FlipToNextSlide(){
		sequence++;
		if (sequence >= bgs.Length) {
			GetComponent<EnemySelection> ().Create ();
			SceneManager.LoadScene ("Fight");
		} else {
			Background.GetComponent<Image> ().sprite = bgs[sequence];
			StartCoroutine(Appear(slides[sequence]));
		}
	}
	IEnumerator Appear(string textToAppear){
		story.text = "";
		appearing = true;
		yield return new WaitForSeconds (.5f);
		foreach (char c in textToAppear.ToCharArray()) {
			if (interrupt) {
				story.text = textToAppear;
				interrupt = false;
				appearing = false;
				yield break;
			}
			story.text += c;
			if (c == '\n')
				yield return new WaitForSeconds (0.4f);
			else
				yield return new WaitForSeconds (0.03f);
		}
		appearing = false;
	}
}
