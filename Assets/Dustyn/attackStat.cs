using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackStat : MonoBehaviour {

	public statManager sm;

	public int attackPower;
	public int nextAttackPower;

	public float minAttack;
	public float maxAttack;
	public float Damage;


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
		minAttack += 2;
		maxAttack += 2;
	}
}
