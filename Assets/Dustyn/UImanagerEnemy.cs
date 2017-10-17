using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanagerEnemy : MonoBehaviour {

	[Header("UI references")]
	public Slider healthBar;

	[Header("Characters References")]

	public statManagerEnemy sme;


	void Stat()
	{
		sme =  this.GetComponentInParent<statManagerEnemy> ();
	}

	void Update () {
		
					healthBar.maxValue = sme.maxHealth;
					healthBar.value = sme.curHealth;

	}

}
