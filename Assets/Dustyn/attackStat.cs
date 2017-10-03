using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackStat : MonoBehaviour {

	public statManager sm;
	public int attackPower;

	public float minAttack;
	public float maxAttack;

	void Start () {
		attackPower = sm.attackLvl;
	}
	

	void Update () {
		
	}
}
