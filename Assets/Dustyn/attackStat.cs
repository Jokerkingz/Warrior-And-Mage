﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackStat : MonoBehaviour {

	public statManager sm;
	public statManagerEnemy sme;

	//public int attackPower;

	public float minAttack;
	public float maxAttack;
	public float Damage;

	public float attackBoost;

	public string owner;

	void Start () 
	{
	if (owner == "player") {
		sm = this.gameObject.GetComponent<statManager> ();
	} else {
		sme = this.gameObject.GetComponent<statManagerEnemy> ();
	}
}
	

	void Update () {
		//attackPower = sm.attackLvl;

		//TEST ATTACK
		/*if (Input.GetKeyDown(KeyCode.End)){
			Damage = (Mathf.Round(Random.Range (minAttack, maxAttack)));

		Debug.Log (Damage);
		}*/
			

	}
	public float DamageCalculation()
	{
		Damage = (Mathf.Round(Random.Range (minAttack, maxAttack)));
		Debug.Log (Damage.ToString() + " damage");
		return Damage;
	}


	public void UpgradeAttack()
	{
		minAttack += attackBoost;
		maxAttack += attackBoost;
	}
}
