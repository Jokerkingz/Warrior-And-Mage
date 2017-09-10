using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Global : MonoBehaviour {
	public string CurrentTurn;
	public GameObject Object_Warrior;
	public GameObject Object_Mage;
	public GameObject Object_Orc;
	public float TurnCoolDown;
	private float LerpFrame;
	private Transform Warrior_Previous;
	private Transform Warrior_Next;
	private Transform Mage_Previous;
	private Transform Mage_Next;
	private Transform Enemy_Previous;
	private Transform Enemy_Next;

	public string SubStatus; // Paused, options, shop, Item, Play
	public string[] SkillList;

	public float vDeltaTimeAdjustor;

	// Global shared variable
	public string Global_AnimationState;
	public float Global_AnimationFrame;


	// Float for Canvas and Item usage
	public float Global_WarriorItem;
	public float Global_MageItem;

	// Potion can only be used once per turn. False - The player hasn't used one yet.
	private bool vPotionUsed= false;
	private bool vElixerUsed = false;

	// Inventory System
	public int Global_HealthPotion_Count = 5;
	public int Global_ElixerPotion_Count = 5;

	// Use this for initialization
	void Start () {
		CurrentTurn = "Player";
		Object_Warrior = GameObject.FindGameObjectWithTag ("Warrior");
		Object_Mage = GameObject.FindGameObjectWithTag ("Mage");
		Object_Orc = GameObject.FindGameObjectWithTag ("Enemy");
		TurnCoolDown = 0;

		Global_AnimationState = "EndAnimate";
		Global_AnimationFrame = 0f;
		SubStatus = "Play";
		Global_WarriorItem = 0f;
		Global_MageItem = 0f;
		SkillList = new string[8]{"BasicAttack","Smash","Roar","Swing","BasicAttack","Push","Ice Spear","Block"};

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("WarriorItem"))
			Debug.Log ("Pressing Warrior Item");
		switch (Global_AnimationState) {
		case "StartAnimate":
			Global_AnimationFrame = 0f;
			Global_AnimationState = "Animate";
			break;
		case "Animate":
			Global_AnimationFrame += 0.05f;
			if (Global_AnimationFrame >= 1f) {
				Global_AnimationFrame = 1f;
				Global_AnimationState = "EndAnimate";
			}
			break;
		case "EndAnimate":
			Global_AnimationFrame = 1f;
			break;
		}
		if (Global_AnimationState == "EndAnimate" && Is_Everyone_Idle())
			switch (CurrentTurn) {
				case "Player":
					InputCheck ();
					break;
				case "AI":
					if (GameObject.FindGameObjectsWithTag ("Enemy").Length > 0)
						ControlAI ();
					else
						CurrentTurn = "Player";
					break;
			}
	}
	void InputCheck(){
		string WarriorOrder = WarriorInput();
		string MageOrder = MageInput();

		if (Input.GetKey(KeyCode.Space))
			Debug.Log("Warrior " + WarriorOrder + " Mage " + MageOrder);

		if (WarriorOrder != "None" && MageOrder != "None" && TurnCoolDown <= 0f) {
			if (WarriorOrder == "Attack") {
				AttackEnemy ();
			}
			else if (WarriorOrder == "Break") {
				BreakWall ();
			}
			else 
			{
				Object_Warrior.GetComponent<Scr_ActionAnimation> ().vAnimationState = "StartMoving";
				Object_Warrior.GetComponent<Scr_ActionAnimation> ().vInputType = WarriorOrder;
			}
			if (MageOrder == "Push") {
				PushSpell ();
			} else {
				Object_Mage.GetComponent<Scr_ActionAnimation> ().vAnimationState = "StartMoving";
				Object_Mage.GetComponent<Scr_ActionAnimation> ().vInputType = MageOrder;
			}
			TurnCoolDown = 30.0f;
			CurrentTurn = "AI";
			Global_AnimationState = "StartAnimate";
		}
		
		WarriorOrder = "None";
		MageOrder = "None";
	}

	string WarriorInput(){
		string Order = "None";
		if (!Input.GetButton ("WarriorItem")) {
			if (Input.GetAxis ("Analog1Y") * Mathf.Sign (Input.GetAxis ("Analog1Y")) > Input.GetAxis ("Analog1X") * Mathf.Sign (Input.GetAxis ("Analog1X"))) {
				if (Input.GetAxis ("Analog1Y") > 0f)
					Order = "MoveUp";
				else if (Input.GetAxis ("Analog1Y") < 0f)
					Order = "MoveDown";
			} else {
				if (Input.GetAxis ("Analog1X") < 0f)
					Order = "MoveLeft";
				else if (Input.GetAxis ("Analog1X") > 0f)
					Order = "MoveRight";
			}
			if (Input.GetAxis ("WarriorAttack") < 0f || Input.GetButton ("WarriorAttack")) {
				Order = SkillList[0];
			}
			if (Input.GetAxis ("WarriorSkill1") < 0f || Input.GetButton ("WarriorSkill1")) {
				Order = SkillList[1];
			}
			if (Input.GetAxis ("WarriorSkill2") < 0f || Input.GetButton ("WarriorSkill2")) {
				Order = SkillList[2];
			}
			if (Input.GetAxis ("WarriorSkill3") < 0f || Input.GetButton ("WarriorSkill3")) {
				Order = SkillList[3];
			}
			if (Input.GetAxis ("WaitTrigger") > 0f || Input.GetButton ("WarriorWait"))
				Order = "Wait";
		} else {
			if (Input.GetAxis ("WarriorAttack") < 0f || Input.GetButton ("WarriorAttack")) {
				Order = "Attack";
			}
			if (Input.GetAxis ("WarriorSkill1") < 0f || Input.GetButton ("WarriorSkill1")) {
				Order = "Break";
			}
			if (Input.GetAxis ("WarriorSkill1") < 0f || Input.GetButton ("WarriorSkill1")) {
				Order = "Break";
			}
		}
		return Order;
	}

	string MageInput(){
		string Order = "None";
		if (!Input.GetButton ("MageItem")) {
			if (Input.GetAxis ("Analog2Y") * Mathf.Sign (Input.GetAxis ("Analog2Y")) > Input.GetAxis ("Analog2X") * Mathf.Sign (Input.GetAxis ("Analog2X"))) {
				if (Input.GetAxis ("Analog2Y") > 0f)
					Order = "MoveUp";
				else if (Input.GetAxis ("Analog2Y") < 0f)
					Order = "MoveDown";
			} else {
				if (Input.GetAxis ("Analog2X") < 0f)
					Order = "MoveLeft";
				else if (Input.GetAxis ("Analog2X") > 0f)
					Order = "MoveRight";
			}
			if (Input.GetButton ("MageAttack")) {
				Order = SkillList[4];
			}
			if (Input.GetButton ("MageSkill1")) {
				Order = SkillList[5];
			}
			if (Input.GetButton ("MageSkill2")) {
				Order = SkillList[6];
			}
			if (Input.GetButton ("MageSkill3")) {
				Order = SkillList[7];
			}
			if (Input.GetAxis ("WaitTrigger") < 0f || Input.GetButton ("MageWait"))
				Order = "Wait";
		}
		return Order;
	}


	void AttackEnemy(){
		if (GameObject.FindGameObjectsWithTag ("Enemy").Length > 0) {
			float tOX = Object_Orc.transform.position.x,
			tWX = Object_Warrior.transform.position.x,
			tOZ = Object_Orc.transform.position.z,
			tWZ = Object_Warrior.transform.position.z;
			if (Vector2.Distance (new Vector2 (tOX, tOZ), new Vector2 (tWX, tWZ)) < 2f) {
				Object_Orc.GetComponent<Scr_SFX_Damage_Blinker> ().vBlinkFrame = .01f;
			}
			Object_Warrior.GetComponent<Scr_ActionAnimation> ().vAnimationState = "StartMoving";
			Object_Warrior.GetComponent<Scr_ActionAnimation> ().vInputType = "Wait";
		}
	}
	void BreakWall(){
		if (GameObject.FindGameObjectsWithTag ("Breakable").Length > 0) {
			GameObject tWall = GameObject.FindGameObjectWithTag ("Breakable");
			float tOX = tWall.transform.position.x,
			tWX = Object_Warrior.transform.position.x,
			tOZ = tWall.transform.position.z,
			tWZ = Object_Warrior.transform.position.z;
			if (Vector2.Distance (new Vector2 (tOX, tOZ), new Vector2 (tWX, tWZ)) < 2f) {
				Object_Warrior.GetComponent<Scr_ActionAnimation> ().vAnimationState = "StartMoving";
				Destroy (tWall.gameObject);
				Object_Warrior.GetComponent<Scr_ActionAnimation> ().vInputType = "Wait";
			}
		}
	}
	void PushSpell(){
		float tMX = Object_Mage.transform.position.x,
		tWX = Object_Warrior.transform.position.x,
		tMZ = Object_Mage.transform.position.z,
		tWZ = Object_Warrior.transform.position.z;
		Object_Mage.GetComponent<Scr_ActionAnimation> ().vAnimationState = "StartMoving";
		Object_Mage.GetComponent<Scr_ActionAnimation> ().vInputType = "Wait";
		if (Vector2.Distance(new Vector2(tMX,tMZ),new Vector2(tWX,tWZ))<1.1f){
			if ((tMX-tWX)*Mathf.Sign(tMX-tWX) > (tMZ-tWZ)*Mathf.Sign(tMZ-tWZ)) {
				Object_Warrior.GetComponent<Scr_ActionAnimation> ().vAnimationState = "StartMoving";
				if (tMX < tWX)
					Object_Warrior.GetComponent<Scr_ActionAnimation> ().vInputType = "PushUp";
				if (tMX > tWX)
					Object_Warrior.GetComponent<Scr_ActionAnimation> ().vInputType = "PushDown";
			} else {
				Object_Warrior.GetComponent<Scr_ActionAnimation> ().vAnimationState = "StartMoving";
				if (tMZ < tWZ)
					Object_Warrior.GetComponent<Scr_ActionAnimation> ().vInputType = "PushRight";
				if (tMZ > tWZ)
					Object_Warrior.GetComponent<Scr_ActionAnimation> ().vInputType = "PushLeft";
			}
		}
	}
	void ControlAI(){
		//if (TurnCoolDown <= 0f)
		{
			float tOX = Object_Orc.transform.position.x,
			tWX = Object_Warrior.transform.position.x,
			tOZ = Object_Orc.transform.position.z,
			tWZ = Object_Warrior.transform.position.z;

			if (Vector2.Distance(new Vector2(tOX,tOZ),new Vector2(tWX,tWZ))<2f){
				Object_Orc.GetComponent<Scr_ActionAnimation> ().vAnimationState = "StartMoving";
				Object_Warrior.GetComponent<Scr_SFX_Damage_Blinker> ().vBlinkFrame = .01f;
				Object_Orc.GetComponent<Scr_ActionAnimation> ().vInputType = "Wait";}
			else
			if ((tOX-tWX)*Mathf.Sign(tOX-tWX) > (tOZ-tWZ)*Mathf.Sign(tOZ-tWZ)) {
				Object_Orc.GetComponent<Scr_ActionAnimation> ().vAnimationState = "StartMoving";
				if (tOX < tWX)
					Object_Orc.GetComponent<Scr_ActionAnimation> ().vInputType = "MoveUp";
				if (tOX > tWX)
					Object_Orc.GetComponent<Scr_ActionAnimation> ().vInputType = "MoveDown";
			} else {
				Object_Orc.GetComponent<Scr_ActionAnimation> ().vAnimationState = "StartMoving";
				if (tOZ < tWZ)
					Object_Orc.GetComponent<Scr_ActionAnimation> ().vInputType = "MoveRight";
				if (tOZ > tWZ)
					Object_Orc.GetComponent<Scr_ActionAnimation> ().vInputType = "MoveLeft";
			}
			CurrentTurn = "Player";
			TurnCoolDown = 30.0f;
			Global_AnimationState = "StartAnimate";
		}
	}

	bool Is_Everyone_Idle(){
		bool tEveryoneisIdle = true;
		GameObject[] tThose;
		tThose = GameObject.FindGameObjectsWithTag ("Warrior");
		foreach (GameObject tThat in tThose) {
			if (tThat.GetComponent<Scr_ActionAnimation>().vAnimationState != "Idle")
				tEveryoneisIdle = false;
		}
		tThose = GameObject.FindGameObjectsWithTag ("Mage");
		foreach (GameObject tThat in tThose) {
			if (tThat.GetComponent<Scr_ActionAnimation>().vAnimationState != "Idle")
				tEveryoneisIdle = false;
		}
		tThose = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject tThat in tThose) {
			if (tThat.GetComponent<Scr_ActionAnimation>().vAnimationState != "Idle")
				tEveryoneisIdle = false;
		}
		tThose = GameObject.FindGameObjectsWithTag ("AnimationObject");
		foreach (GameObject tThat in tThose) {
			tEveryoneisIdle = false;
			}
		return tEveryoneisIdle;
	}
}
