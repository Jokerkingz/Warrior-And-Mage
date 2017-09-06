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

	public float vDeltaTimeAdjustor;

	// Global shared variable
	public string Global_AnimationState;
	public float Global_AnimationFrame;

	// Use this for initialization
	void Start () {
		CurrentTurn = "Player";
		Object_Warrior = GameObject.FindGameObjectWithTag ("Warrior");
		Object_Mage = GameObject.FindGameObjectWithTag ("Mage");
		Object_Orc = GameObject.FindGameObjectWithTag ("Enemy");
		TurnCoolDown = 0;

		Global_AnimationState = "EndAnimate";
		Global_AnimationFrame = 0f;
	}
	
	// Update is called once per frame
	void Update () {
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

		if (TurnCoolDown > 0)
			TurnCoolDown -= 1.0f;
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
		string WarriorOrder = "None";
		string MageOrder = "None";

		/// Player 1
		if (Input.GetAxis ("Analog1Y") * Mathf.Sign (Input.GetAxis ("Analog1Y")) > Input.GetAxis ("Analog1X") * Mathf.Sign (Input.GetAxis ("Analog1X"))) {
			if (Input.GetAxis ("Analog1Y") > 0f)
				WarriorOrder = "MoveUp";
			else if (Input.GetAxis ("Analog1Y") < 0f)
				WarriorOrder = "MoveDown";
		} else {
			if (Input.GetAxis("Analog1X") < 0f) WarriorOrder = "MoveLeft";
			else if (Input.GetAxis("Analog1X") > 0f) WarriorOrder = "MoveRight";
		}
		/// Player 2
		if (Input.GetAxis ("Analog2Y") * Mathf.Sign (Input.GetAxis ("Analog2Y")) > Input.GetAxis ("Analog2X") * Mathf.Sign (Input.GetAxis ("Analog2X"))) {
			if (Input.GetAxis ("Analog2Y") > 0f)
				MageOrder = "MoveUp";
			else if (Input.GetAxis ("Analog2Y") < 0f)
				MageOrder = "MoveDown";
		} else {
			if (Input.GetAxis("Analog2X") < 0f)
				MageOrder = "MoveLeft";
			else if (Input.GetAxis("Analog2X") > 0f)
				MageOrder = "MoveRight";
		}
		/// 
		if (Input.GetAxis ("WarriorAttack") < 0f) {
			WarriorOrder = "Attack";
		}
		if (Input.GetAxis ("WarriorSkill1") < 0f) {
			WarriorOrder = "Break";
		}
		if (Input.GetAxis ("LeftTrigger") > 0f)
			WarriorOrder = "Wait";

		if (Input.GetButton("MageSkill1")) {
			MageOrder = "Push";
		}
		if (Input.GetAxis ("LeftTrigger") < 0f)
			MageOrder = "Wait";
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

		return tEveryoneisIdle;
	}
}
