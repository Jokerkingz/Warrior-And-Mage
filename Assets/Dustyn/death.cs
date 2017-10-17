using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death : MonoBehaviour {
	
	public addExperience addXp;
	public GameObject expBar;
	public float exp; 
	public string owner;

	void Start () {
		expBar = GameObject.Find ("ExperienceBar");

		if (owner == "slime") {
			exp = 10f;
		}
		if (owner == "ghost") {
			exp = 20f;
		}
	}
	

	void Update () {
		
	}
		
	public void Die ()
	{
		if (owner == "slime") {
			Destroy (this.gameObject);
			addXp.AddExp (exp);
			expBar.SendMessage("Appear");
		}

		if (owner == "player") {
		
		}
	}
}

