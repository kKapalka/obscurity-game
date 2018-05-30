using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataScript : MonoBehaviour {

	public Text[] display;
	public Sprite[] weaponSprites;
	public Button weaponButton;
	string[] weaponTypes = new string[]{"scattershot","doubleattack","basic","dragndrop","hammer","dragthrough"};
	public void Assign(GameObject player){
		CharacterStats stats = player.GetComponent<CharacterStats> ();
		Weapon weapon = player.GetComponent<PlayerScript> ().getWeapon ();

		display [0].text = stats.dodge.getValue ().ToString();
		display [1].text = ((float)stats.strength.getValue ()/10f).ToString();
		display [2].text = stats.regeneration.getValue ().ToString();
		for (int i = 0; i < stats.resistances.Length; i++) {
			display [3 + i].text = stats.resistances [i].getValue ().ToString ();
		}
		float matches = 3 + ((float)stats.strength.getValue () / 10);
		int weaponDMG = Mathf.RoundToInt (weapon.getDamage (matches) * (1.0f + ((float)stats.damageMultiplier.getValue () / 100)));
		int gravityDMG = Mathf.RoundToInt ((6 * Mathf.Pow (1.35f, (matches - 2f)) - 1) * (1.0f + ((float)stats.damageMultiplier.getValue () / 100)));
		if(player.GetComponent<PlayerScript>().getUniqueMods().Contains("claws of hate")){
			weaponDMG*=2;
			gravityDMG*=2;
		}
		display[8].text= weaponDMG.ToString();
		display[8].text+=" / "+gravityDMG.ToString();

		for (int i = 0; i < weaponTypes.Length; i++) {
			if (player.GetComponent<PlayerScript>().weaponType == weaponTypes [i]) {
				weaponButton.GetComponent<Image>().sprite = weaponSprites [i];
				break;
			}
		}
	}
}
