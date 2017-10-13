using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour {


	[Header("UI references")]
	public Slider healthBar;
	public Slider specBar;
	public Slider expBar;
	public Text levelTxt;

	//Characters
	[Header("Characters References")]

	public statManager sm;
	public statManagerEnemy sme;
	public string owner;

	/*private static bool UIExist;

	void Awake()
	{
		if (!UIExist) {
			UIExist = true;
			DontDestroyOnLoad (transform.gameObject);
		} else {
			Destroy (gameObject);
		}

	}*/

	void Stat()
	{
		if (owner == "player") {
			sm =  this.GetComponentInParent<statManager> ();
		} else {
			sme =  this.GetComponentInParent<statManagerEnemy> ();
		}
	}

	void Update () {

		//HEALTH BAR
		if (owner == "player") {
			healthBar.maxValue = sm.maxHealth;
			healthBar.value = sm.curHealth;

			//SPEC BAR
			specBar.maxValue = sm.maxSpec;
			specBar.value = sm.curSpec;

			//EXPERIENCE BAR
			expBar.maxValue = sm.nextLvl;
			expBar.value = sm.curExp;
			levelTxt.text = "LEVEL " + sm.curLvl;

		} else {
//			healthBar.maxValue = sme.maxHealth;
//			healthBar.value = sme.curHealth;
		}
	}
		
}
