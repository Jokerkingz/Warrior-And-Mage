using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour {

	//UI
	[Header("Warrior UI References")]
	public Slider healthBarWarrior;
	public Text healthTextWarrior;
	public Slider staminaBar;
	public Text staminaText;

	public Slider warriorExpBar;
	public Text warriorExpText;
	public Text warriorLevel;

	[Header("Mage UI References")]
	public Slider healthBarMage;
	public Text healthTextMage;
	public Slider manaBar;
	public Text manaText;

	public Slider mageExpBar;
	public Text mageExpText;
	public Text mageLevel;

	//Characters
	[Header("Characters References")]
	public statManager smWarrior;
	public statManager smMage;

	private static bool UIExist;

	void Awake()
	{
		if (!UIExist) {
			UIExist = true;
			DontDestroyOnLoad (transform.gameObject);
		} else {
			Destroy (gameObject);
		}

	}

	void Update () {
		
		//WARRIOR HEALTH
		healthBarWarrior.maxValue = smWarrior.maxHealth;
		healthBarWarrior.value = smWarrior.curHealth;
		healthTextWarrior.text = "HEALTH: " + smWarrior.curHealth + "/" + smWarrior.maxHealth;
	
		//MAGE HEALTH
		healthBarMage.maxValue = smMage.maxHealth;
		healthBarMage.value = smMage.curHealth;
		healthTextMage.text = "HEALTH: " + smMage.curHealth + "/" + smMage.maxHealth;

		//STAMINA
		staminaBar.maxValue = smWarrior.maxSpec;
		staminaBar.value = smWarrior.curSpec;
		staminaText.text = "STAMINA: " + smWarrior.curSpec + "/" + smWarrior.maxSpec;

		//MANA
		manaBar.maxValue = smMage.maxSpec;
		manaBar.value = smMage.curSpec;
		manaText.text = "MANA: " + smMage.curSpec + "/" + smMage.maxSpec;


		//WARRIOR LEVEL UP
		warriorExpBar.maxValue =smWarrior.maxExp;
		warriorExpBar.value = smWarrior.curExp;
		warriorExpText.text = "XP: " + smWarrior.curExp + "/" + smWarrior.maxExp;
		warriorLevel.text = "WARRIOR (LEVEL " + smWarrior.curLvl + ")";

		//MAGE LEVEL UP
		mageExpBar.maxValue =smMage.maxExp;
		mageExpBar.value = smMage.curExp;
		mageExpText.text = "XP: " + smMage.curExp + "/" + smMage.maxExp;
		mageLevel.text = "MAGE (LEVEL " + smMage.curLvl + ")";
	}
		
}
