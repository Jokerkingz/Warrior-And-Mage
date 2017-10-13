using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defenseStat : MonoBehaviour {

	public statManager sm;
	public statManagerEnemy sme;

	//public int defensePower;

	public float healthToRemove;

	public float minDef;
	public float maxDef;
	public float Protection;

	public float defBoost;


	public string owner;

	void Start () {
		
		if (owner == "player") {
			sm = this.gameObject.GetComponent<statManager> ();
		} else {
			sme = this.gameObject.GetComponent<statManagerEnemy> ();
		}
	}

	void Update () {


		/*if (Input.GetKeyDown(KeyCode.Home)){
			Protection= (Mathf.Round(Random.Range (minDef, maxDef)));
			Debug.Log (Protection);
		}*/
	}

	public void UpgradeDefense()
	{
		minDef += defBoost;
		maxDef += defBoost;
	}


	public void DamageEquation(float dmg)
	{
		Protection= (Mathf.Round(Random.Range (minDef, maxDef)));
		Debug.Log (Protection.ToString() + " protection");
		healthToRemove = Protection - dmg;
		if (healthToRemove >= 0) {
			healthToRemove = -1f;
		}

		if (owner == "player") {
			//sm.SendMessage ("Damage", healthToRemove);
			sm.Damage (healthToRemove);
		} else {
			//sme.SendMessage ("Damage", healthToRemove);
			sme.Damage(healthToRemove);
		}

	}

}
