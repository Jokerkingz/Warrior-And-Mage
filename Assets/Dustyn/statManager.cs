using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statManager : MonoBehaviour {

	//public GameObject target;

	public int maxHealth;
	public int curHealth;

	//NOTE: Spec refers to either Stamina or Mana
	public int maxSpec;
	public int curSpec;

	public int maxExp;
	public int curExp;
	public int maxLvl;
	public int curLvl;

	public KeyCode DrainHealth;
	public KeyCode ReplenishHealth;
	public KeyCode DrainSpec;
	public KeyCode ReplenishSpec;

	public KeyCode AddXP;

	void Start () {
		
		curHealth = maxHealth;
		curSpec = maxSpec;
		curExp = 0;
	}
	

	void Update () {



		// FOR TESTING PURPOSES

		if (Input.GetKeyDown (DrainHealth)) {
			curHealth -= 10;
		}
		if (Input.GetKeyDown (ReplenishHealth)) {
			curHealth += 10;
		}
		if (Input.GetKeyDown (DrainSpec)) {
			curSpec -= 10;
		}
		if (Input.GetKeyDown (ReplenishSpec)) {
			curSpec += 10;
		}

		if (Input.GetKeyDown (AddXP)) {
			curExp += 10;
		}

		Stats ();
	}

	void Stats()
	{
		if (curHealth <= 0) {
			curHealth = 0;
		}
		if (curHealth > maxHealth) {
			curHealth = maxHealth;

		}if (curSpec <= 0) {
			curSpec = 0;
		}
		if (curSpec > maxSpec) {
			curSpec = maxSpec;
		}
			
		if (curExp >= maxExp) {
			curLvl += 1;
			curExp = 0;
			maxExp *= 2;
			maxHealth += 50;
			maxSpec +=50;
			curHealth += 50;
			curSpec += 50;
		}

		if (curLvl >= maxLvl) {
			curLvl = maxLvl;
		}
}
}
