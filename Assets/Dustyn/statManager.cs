using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statManager : MonoBehaviour {

	[Header("Stat Ints")]

	public int maxHealth;
	public int curHealth;

	//NOTE: Spec refers to either Stamina or Mana
	public int maxSpec;
	public int curSpec;

	public int attackLvl;
	public int defenseLvl;

	[Header("Leveling")]

	public int curLvl;
	public int curExp;
	public int nextLvl;
	public int[] toLevelUp;
	//public int LevelUpCredit;

	[Header("KeyCodes for testing")]
	public KeyCode DrainHealth;
	public KeyCode ReplenishHealth;
	public KeyCode DrainSpec;
	public KeyCode ReplenishSpec;
	public KeyCode AddXP;

	[Header("References")]
	public GameObject healthBar;
	public GameObject specBar;
	public GameObject expBar;
	public GameObject lvlTxt;
	public GameObject LevelUpSystem;
	public attackStat attStat;

	void Start () {
		LevelUpSystem = GameObject.Find("LevelingUpSystem");
		curHealth = maxHealth;
		curSpec = maxSpec;
		curExp = 0;
	
		attStat = this.gameObject.GetComponent<attackStat> ();

	}
	

	void Update () {

	
		// FOR TESTING PURPOSES

		if (Input.GetKeyDown (DrainHealth)) {
			curHealth -= 10;
			healthBar.SendMessage ("Appear");
		}
		if (Input.GetKeyDown (ReplenishHealth)) {
			curHealth += 10;
			healthBar.SendMessage ("Appear");
		}
		if (Input.GetKeyDown (DrainSpec)) {
			curSpec -= 10;
			specBar.SendMessage ("Appear");
		}
		if (Input.GetKeyDown (ReplenishSpec)) {
			curSpec += 10;
			specBar.SendMessage ("Appear");
		}

		if (Input.GetKeyDown (AddXP)) {
			curExp += 10;
			expBar.SendMessage ("Appear");
		}

		if (curExp >= toLevelUp [curLvl]) {
			LevelUp ();
			lvlTxt.SendMessage ("Appear");
		}

		Stats ();
	}

	void Stats ()
	{
		if (curHealth <= 0) {
			curHealth = 0;
		}
		if (curHealth >= maxHealth) {
			curHealth = maxHealth;

		}
		if (curSpec <= 0) {
			curSpec = 0;
		}
		if (curSpec > maxSpec) {
			curSpec = maxSpec;
		}
			
		nextLvl= toLevelUp[curLvl];

	}

	void LevelUp()
	{
		curLvl++;
		if (curLvl > 1) {
			LevelUpSystem.SendMessage ("AddCredits");
		}
	}

	public void HealthLvlUp()
	{
		maxHealth += 10;
	}
	public void SpecialLvlUp()
	{
		maxSpec += 5;
	}
	public void AttackLvlUp()
	{
		attackLvl++;
		attStat.SendMessage ("UpgradeAttack");

	}
	public void DefenceLvlUp()
	{
		defenseLvl++;
	}

}
