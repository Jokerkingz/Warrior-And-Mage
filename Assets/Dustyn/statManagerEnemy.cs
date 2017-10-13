using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statManagerEnemy : MonoBehaviour {

	[Header("Stat Ints")]

	public float maxHealth;
	public float curHealth;

	public int attackLvl;
	public int defenseLvl;


	[Header("References")]
	public GameObject healthBar;

	public attackStat attStat;
	public defenseStat defStat;


	void Start () {
		curHealth = maxHealth;


		attStat = this.gameObject.GetComponent<attackStat> ();
		defStat = this.gameObject.GetComponent<defenseStat> ();

	}


	void Update () {

		Stats ();

	}

	void Stats ()
	{
		if (curHealth <= 0) {
			curHealth = 0;
			this.GetComponent<Scr_SFX_Damage_Blinker> ();
		}
		if (curHealth >= maxHealth) {
			curHealth = maxHealth;
		}
	}
		


	public void Damage(float healthToRemove)
	{
		curHealth += healthToRemove;
		//healthBar.SendMessage ("Appear");
		//healthBar.GetComponent<statBarFade> ().Appear ();
	}
}
