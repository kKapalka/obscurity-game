using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopupText: MonoBehaviour{
	public Animator animator;
	private Text damageText;

	public void onEnable(){
		AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo (0);
		Destroy (gameObject, clipInfo [0].clip.length-0.2f);
		damageText = animator.GetComponent<Text> ();
	}
	public void setText(string text){
		damageText.text = text;
	}

}