using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defenseStat : MonoBehaviour {

	public statManager sm;

	public int defensePower;

	public float healthToRemove;

	public float minDef;
	public float maxDef;
	public float Protection;

	public float defBoost;

	void Start () {
		sm = this.gameObject.GetComponent<statManager> ();
	}

	void Update () {


		if (Input.GetKeyDown(KeyCode.Home)){
			Protection= (Mathf.Round(Random.Range (minDef, maxDef)));
			Debug.Log (Protection);
		}
	}

	public void UpgradeDefense()
	{
		minDef += defBoost;
		maxDef += defBoost;
	}


	public void DamageEquation(float dmg)
	{
		healthToRemove = Protection - dmg;
		sm.SendMessage ("Damage");
	}
}
