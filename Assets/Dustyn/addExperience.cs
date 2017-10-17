using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class addExperience : MonoBehaviour {

	[Header("Refs")]
	public Slider expBar;
	public Text lvlTxt;
	public GameObject LevelUpSystem;

	[Header("Leveling")]

	public int curLvl;
	public float curExp;
	public int nextLvl;
	public int[] toLevelUp;

	// Use this for initialization
	void Start () {
		
		LevelUpSystem = GameObject.Find("LevelingUpSystem");
	}
	
	// Update is called once per frame
	void Update () {
		nextLvl= toLevelUp[curLvl];

		if (curExp >= toLevelUp [curLvl]) {
			LevelUp ();
			lvlTxt.SendMessage ("Appear");
		}

		expBar.maxValue = nextLvl;
			expBar.value = curExp;
			lvlTxt.text = "LEVEL " + curLvl;
			

	}

	void LevelUp()
	{
		curLvl++;
		if (curLvl > 1) {
			LevelUpSystem.SendMessage ("AddCredits");
		}
	}

	public void AddExp(float exp)
	{
		Debug.Log (exp.ToString() + " xp added");
		curExp += exp; 
	}
}
