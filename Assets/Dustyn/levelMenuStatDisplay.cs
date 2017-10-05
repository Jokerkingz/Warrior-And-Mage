using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelMenuStatDisplay : MonoBehaviour {

	public statManager sm;

	public Slider healthSlider;
	public Slider specSlider;
	public Text healthTxt;
	public Text specTxt;
	public Text attackTxt;
	public Text defenseTxt;


	void Start()
	{

	}

	void Update()
	{
		//HEALTH
		healthSlider.maxValue = sm.maxHealth;
		healthSlider.value = sm.curHealth;
		healthTxt.text = "Health: " + sm.curHealth + "/" + sm.maxHealth;

		//SPECIAL
		specSlider.maxValue = sm.maxSpec;
		specSlider.value = sm.curSpec;
		specTxt.text =  specTxt.gameObject.name +": " +sm.curSpec + "/" + sm.maxSpec;

		attackTxt.text = "ATTACK: " + sm.attackLvl;
		defenseTxt.text = "DEFENSE: " + sm.defenseLvl;
	}
	
}	
