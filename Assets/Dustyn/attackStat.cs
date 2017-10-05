using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackStat : MonoBehaviour {

	public statManager sm;

	public int attackPower;

	public float minAttack;
	public float maxAttack;
	public float Damage;

	public float attackBoost;

	void Start () {
		sm = this.gameObject.GetComponent<statManager> ();
	}
	

	void Update () {
		attackPower = sm.attackLvl;

		//TEST ATTACK
		if (Input.GetKeyDown(KeyCode.End)){
			Damage = (Mathf.Round(Random.Range (minAttack, maxAttack)));

		Debug.Log (Damage);
		}
			

	}

	public void UpgradeAttack()
	{
		minAttack += attackBoost;
		maxAttack += attackBoost;
	}
}
