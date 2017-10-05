using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelSystem : MonoBehaviour {

	public int warrCredits;
	public int mageCredits;

	public GameObject warriorSM;
	public GameObject mageSM;

	public Text wcredTxt;
	public Text mcredTxt;

	public bool active;

	//APPEAR
	private float alpha;
	private CanvasGroup cg;

	void Start () {
		warriorSM = GameObject.FindGameObjectWithTag ("Warrior");
		mageSM = GameObject.FindGameObjectWithTag ("Mage");

		warrCredits = 0;
		mageCredits = 0;

		cg = this.gameObject.GetComponent<CanvasGroup> ();
		cg.alpha = alpha;
		active = false;
	}
	

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && !active) {
			active = true;

		}
		else if (Input.GetKeyDown (KeyCode.Escape) && active)
		{active = false;}
	
		//TEXT FOR CREDITS
		wcredTxt.text = "Credits: " + warrCredits;
		mcredTxt.text = "Credits: " + mageCredits;

		if (alpha >=1f)
		{
			alpha=1f;
		}
		if (active == false) {
			cg.alpha = alpha -= 2f * Time.deltaTime;
		}
		if (alpha <=0f)
		{
			alpha=0f;
		}


		//LEVELING UP TEST
		if (active==true)
		{
			cg.alpha = alpha += 2f * Time.deltaTime;

		if (warrCredits >= 1) 
		{
			if (Input.GetKeyDown (KeyCode.F1)) {
				warriorSM.SendMessage ("HealthLvlUp");
				warrCredits--;
			}
			if (Input.GetKeyDown (KeyCode.F2)) {
				warriorSM.SendMessage ("SpecialLvlUp");
				warrCredits--;
			}
			if (Input.GetKeyDown (KeyCode.F3)) {
				warriorSM.SendMessage ("AttackLvlUp");
				warrCredits--;
			}
			if (Input.GetKeyDown (KeyCode.F4)) {
				warriorSM.SendMessage ("DefenceLvlUp");
				warrCredits--;
			}

			
		}

			if (mageCredits >= 1) {
				if (Input.GetKeyDown (KeyCode.F5)) {
					mageSM.SendMessage ("HealthLvlUp");
					mageCredits--;
				}
				if (Input.GetKeyDown (KeyCode.F6)) {
					mageSM.SendMessage ("SpecialLvlUp");
					mageCredits--;
				}
				if (Input.GetKeyDown (KeyCode.F7)) {
					mageSM.SendMessage ("AttackLvlUp");
					mageCredits--;
				}
				if (Input.GetKeyDown (KeyCode.F8)) {
					mageSM.SendMessage ("DefenceLvlUp");
					mageCredits--;
				}
			}

		}
			

	}

	public void AddCredits()
	{
		warrCredits++;
		mageCredits++;
	}

}
