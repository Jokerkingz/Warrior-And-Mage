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
	public death dead;

	void Start () {
		curHealth = maxHealth;


		attStat = this.gameObject.GetComponent<attackStat> ();
		defStat = this.gameObject.GetComponent<defenseStat> ();
		dead = this.gameObject.GetComponent<death> ();
	}


	void Update () {

		Stats ();

	}

	void Stats ()
	{
		if (curHealth <= 0) {
			curHealth = 0;
			//this.GetComponent<Scr_SFX_Damage_Blinker> ();
			//sfxBlink.Die ();
			dead.SendMessage("Die");

		}
		if (curHealth >= maxHealth) {
			curHealth = maxHealth;
		}
	}
		


	public void Damage(float healthToRemove)
	{
		curHealth += healthToRemove;
		//healthBar.SendMessage ("Appear");
		healthBar.GetComponent<statBarFade> ().Appear ();
	}
}
