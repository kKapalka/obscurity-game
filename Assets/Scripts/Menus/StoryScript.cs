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
	string[] slides=new string[]{"Name's Vincent Scottins, and I never had any problems with the law.\n" +
		"In fact, my reputation is well established in the Cartels, and all the goods I have attained, were procured through legal means.\n" +
		"Obviously, my leisurely lifestyle must have caught the attention of certain... someones within the city.",

		"Looking back at it, this was the perfect opportunity for them. People were out, working their butts off in the factories, cotton farms or refineries.\n" +
		"I was in a pub, downing my bourbon and pondering on my future investments. Now this, was hard work - so I thought.\n" +
		"And they appeared out of nowhere - the House of Cards.",

		"'You'll be coming with us, if you please', said one of them with a snarky tone.\n" +
		"With their weapons, aiming point-blank at me, what was there to do? I obeyed..." +
		"Until we went outside the pub, that is. Thankfully, I had allies in all the good places.",

		"I played along. We went on, and on, and on, toward the zeppelin station.\n" +
		"I took a chance. Tackled one of the gangsters, kicked the other. Then...\n" +
		"I took the leap of faith. Jumped past the barrier dividing the city and the open sky.\n" +
		"For a moment, I flew. But then I was retrieved.",

		"I ended up in the Steel Guts of the city. Machinations constructed here keep the city afloat, clean, and supplied with electricity.\n" +
		"Only a few actively work here. Other than that, the place is desolate. I can hide here, and remain in obscurity until the situation stabilizes.\n" +
		"It doesn't mean I'm safe, though. Even now, I am pursued...",

		"'You! Whatcha playin at? Boss said he wants you alive!'\n" +
		"They must have split up. That would explain why this scruffy, unkempt guy had caught up with me so quickly.\n" +
		"I pick up a rusty pipe and move towards the gangster, with the intent of giving him a good beating..."
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
		yield return new WaitForSeconds (1f);
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
