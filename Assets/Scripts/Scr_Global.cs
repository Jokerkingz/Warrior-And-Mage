using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Global : MonoBehaviour {

	public string vCurrentTurnState;
	public string vNextTurn;
	private GameObject Object_Warrior;
	private Scr_ProtagonistAction cWPA;
	private GameObject Object_Mage;
	private Scr_ProtagonistAction cMPA;
	public Scr_CanvasController cCn;
	public GameObject Object_Orc;
	public float TurnCoolDown;
	private float LerpFrame;
	private Transform Warrior_Previous;
	private Transform Warrior_Next;
	private Transform Mage_Previous;
	private Transform Mage_Next;
	private Transform Enemy_Previous;
	private Transform Enemy_Next;
	private float tTimerA;
	private float tTimerB;
	public GameObject vFallCheckPoint;
	private LineRenderer cLR;

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

	// Targeting Icon
	public float Global_WarriorIconRotation = 0f;
	public float Global_MageIconRotationt   = 180f;


	public bool vDebugIsEveryoneIdle;

	// Use this for initialization
	void Start () {
		vCurrentTurnState = "PlayerInputWait";
		Object_Warrior = GameObject.FindGameObjectWithTag ("Warrior");
		cWPA = Object_Warrior.GetComponent<Scr_ProtagonistAction>();
		Object_Mage = GameObject.FindGameObjectWithTag ("Mage");
		cMPA = Object_Mage.GetComponent<Scr_ProtagonistAction>();
		cLR = this.GetComponent<LineRenderer>();
		TurnCoolDown = 0;

		Global_AnimationState = "EndAnimate";
		Global_AnimationFrame = 0f;
		SubStatus = "Play";
		Global_WarriorItem = 0f;
		Global_MageItem = 180f;
		SkillList = new string[8]{"Basic Attack","","","","Basic Attack","","",""};
		//SkillList = new string[8]{"BasicAttack","Bash","Roar","Swing","BasicAttack","Push","IceSpear","Block"};
		vNextTurn = "Player";
	}

	void Update () {
		LineControl();
		tTimerA -= Time.deltaTime;
		tTimerB -= Time.deltaTime;
		if (tTimerA < 0f)
			tTimerA = 0;
		if (tTimerB < 0f)
			tTimerB = 0;
		GameObject[] Those;
		/// Target Spinner
		Global_WarriorIconRotation += Time.deltaTime*45f;
		if (Global_WarriorIconRotation > 360f)
			Global_WarriorIconRotation -= 360f;
		Global_MageIconRotationt +=Time.deltaTime*45f;
		if (Global_MageIconRotationt > 360f)
			Global_MageIconRotationt -= 360f;
		///
		switch (vCurrentTurnState){
			case "PlayerInputWait":
				Those = GameObject.FindGameObjectsWithTag ("PlaceHolders");
					foreach (GameObject That in Those) {
						Destroy(That);
						}
				Object_Warrior.GetComponent<Scr_TargetingSystem>().AfterMove();
				Object_Mage.GetComponent<Scr_TargetingSystem>().AfterMove();
				if (Is_Everyone_Idle()){
					InputCheck ();
					}
			break;
			case "PlayerStartAnimate":
				Global_AnimationFrame = 0f;
				vCurrentTurnState = "PlayerAnimate";
			break;
			case "PlayerAnimate":
				Global_AnimationFrame += Time.deltaTime*vDeltaTimeAdjustor;
				if (Global_AnimationFrame >= 1f) {
					Global_AnimationFrame = 1f;
					vCurrentTurnState = "PlayerEndAnimate";}
			break;
			case "PlayerEndAnimate":
				Global_AnimationFrame = 1f;
				Those = GameObject.FindGameObjectsWithTag ("Enemy");
				bool tThereAreAIs = false;
				foreach (GameObject That in Those) {
					tThereAreAIs = true;}
				if (!tThereAreAIs)
					vCurrentTurnState = "EnvironmentStart";
				else{
				 	if (Is_Everyone_Idle())
						vCurrentTurnState = "AIStart";
					}
			break;
		case "AIStart":
				Those = GameObject.FindGameObjectsWithTag ("PlaceHolders");
					foreach (GameObject That in Those) {
						Destroy(That);
						}
				Those = GameObject.FindGameObjectsWithTag ("Enemy");
					foreach (GameObject That in Those) {
						That.GetComponent<Scr_TargetingSystem>().AfterMove();
						That.GetComponent<Scr_AIControl>().vStatus = "MyTurn";
						}
					Global_AnimationFrame = 0f;
					vCurrentTurnState = "AIAnimate";
			break;
			case "AIAnimate":
				bool vAnyoneonSight = false;
				Those = GameObject.FindGameObjectsWithTag ("Enemy");
				foreach (GameObject That in Those) {
				GameObject tTemp = That.GetComponent<Scr_AntagonistAction>().vModel;
				Renderer tRender = tTemp.GetComponent<MeshRenderer>();
					Plane[] tPlane = GeometryUtility.CalculateFrustumPlanes(Camera.main);
					if (GeometryUtility.TestPlanesAABB(tPlane,tRender.bounds))
						vAnyoneonSight = true;
					}
				if (vAnyoneonSight)
					Global_AnimationFrame += Time.deltaTime*vDeltaTimeAdjustor;
				else Global_AnimationFrame += .5f;
				if (Global_AnimationFrame >= 1f) {
					Global_AnimationFrame = 1f;
					vCurrentTurnState = "AIEndAnimate";
					}
			break;
			case "AIEndAnimate":
				vCurrentTurnState = "EnvironmentStart";
			break;
			case "EnvironmentStart":
				Those = GameObject.FindGameObjectsWithTag ("Movable");
					foreach (GameObject That in Those) {
					That.GetComponent<Scr_SharedEnviro>().vEvent = "StartActing";
			}
				Those = GameObject.FindGameObjectsWithTag ("Targetable");
					foreach (GameObject That in Those) {
					That.GetComponent<Scr_Switch>().vEvent = "StartActing";
						}
			vCurrentTurnState = "EnvironmentAnimate";
				//Global_AnimationFrame += Time.deltaTime*vDeltaTimeAdjustor;
				//if (Global_AnimationFrame >= 1f) {
				//	Global_AnimationFrame = 1f;
				//	vCurrentTurnState = "PlayerEndAnimate";}
			break;
			case "EnvironmentAnimate":
				bool tReady = true;
				Those = GameObject.FindGameObjectsWithTag ("Movable");
					foreach (GameObject That in Those) {
					if (That.GetComponent<Scr_SharedEnviro>().vEvent != "Idle")
							tReady = false;
						}
				if (tReady)
					vCurrentTurnState = "EnvironmentEnd";
			break;
			case "EnvironmentEnd":

				Those = GameObject.FindGameObjectsWithTag ("Warrior");
					foreach (GameObject That in Those) {
						That.GetComponent<Scr_ProtagonistAction>().PitFallCheck();
						}
				Those = GameObject.FindGameObjectsWithTag ("Mage");
					foreach (GameObject That in Those) {
						That.GetComponent<Scr_ProtagonistAction>().PitFallCheck();
						}

				Those = GameObject.FindGameObjectsWithTag ("Enemy");
					foreach (GameObject That in Those) {
						That.GetComponent<Scr_AntagonistAction>().PitFallCheck();
						}
			 	if (Is_Everyone_Idle())
				vCurrentTurnState = "PlayerInputWait";
			break;
			case "Options":
				

			break;
			case "Scene":
				

			break;
			}
	}
	void InputCheck(){
		string WarriorOrder = WarriorInput();
		string MageOrder = MageInput();
		if (WarriorOrder != "None" && MageOrder != "None") { // Both have an action

			cWPA.vAnimationState = "StartActing";
			cWPA.vInputType = WarriorOrder;

			cMPA.vAnimationState = "StartActing";
			cMPA.vInputType = MageOrder;

			vCurrentTurnState = "PlayerStartAnimate"; 
		}
		
		WarriorOrder = "None";
		MageOrder = "None";
	}

	string WarriorInput(){
		string Order = "None";
		if (!Input.GetButton ("WarriorItem")) {
			if (Input.GetAxis ("Analog1Y") * Mathf.Sign (Input.GetAxis ("Analog1Y")) > Input.GetAxis ("Analog1X") * Mathf.Sign (Input.GetAxis ("Analog1X"))) {
				if (Input.GetAxis ("Analog1Y") < 0f)
					Order = "MoveUp";
				else if (Input.GetAxis ("Analog1Y") > 0f)
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
			if (Input.GetAxis ("WarriorSkill2") > 0f || Input.GetButton ("WarriorSkill2")) {
				Order = SkillList[2];
			}
			if (Input.GetAxis ("WarriorSkill3") > 0f || Input.GetButton ("WarriorSkill3")) {
				Order = SkillList[3];
			}
			if (Input.GetAxis ("WarriorWait") > 0f || Input.GetButton ("WarriorWait"))
				Order = "Wait";
		} else if (Input.GetButton ("WarriorItem"))
		 	{cCn.vWarrItem += 1f;
			if (Input.GetAxis ("WarriorAttack") < 0f || Input.GetButton ("WarriorAttack")) {
				Order = "HealthPotion";
			}
			if ((Input.GetAxis ("WarriorSkill1") < 0f || Input.GetButton ("WarriorSkill1")) && tTimerA == 0f) {
				tTimerA = .5f;
				Order = "Next Target";
				Object_Warrior.GetComponent<Scr_TargetingSystem>().NextTarget();
			}
			if (Input.GetAxis ("WarriorSkill2") < 0f || Input.GetButton ("WarriorSkill2")) {
				Order = "TeleportStone";
			}
			if (Input.GetAxis ("WarriorSkill2") < 0f || Input.GetButton ("WarriorSkill2")) {
				Order = "Elixer";
			}
		}
		return Order;
	}

	string MageInput(){
		string Order = "None";
		if (!Input.GetButton ("MageItem")) {
			if (Input.GetAxis ("Analog2Y") * Mathf.Sign (Input.GetAxis ("Analog2Y")) > Input.GetAxis ("Analog2X") * Mathf.Sign (Input.GetAxis ("Analog2X"))) {
				if (Input.GetAxis ("Analog2Y") < 0f)
					Order = "MoveUp";
				else if (Input.GetAxis ("Analog2Y") > 0f)
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
			if (Input.GetAxis ("MageWait") > 0f || Input.GetButton ("MageWait"))
				Order = "Wait";
				}
			else if (Input.GetButton ("MageItem")){
				{cCn.vMageItem += 1f;
				if (Input.GetButton ("MageAttack")) {
					Order = "HealthPotion";
					}
				if (Input.GetButton ("MageSkill1") && tTimerB == 0f) {
					tTimerB = .5f;
					Order = "Next Target";
					Object_Mage.GetComponent<Scr_TargetingSystem>().NextTarget();
					}
				if (Input.GetButton ("MageSkill2")) {
					Order = "TeleportStone";
					}
				if (Input.GetButton ("MageSkill3")) {
					Order = "Elixer";
					}
			}
		}
		return Order;
	}

	void Revive(){
		Object_Warrior.GetComponent<Scr_ProtagonistAction>().FindSpot(vFallCheckPoint);
		Object_Mage.GetComponent<Scr_ProtagonistAction>().FindSpot(vFallCheckPoint);
	}


	bool Is_Everyone_Idle(){
		bool tEveryoneisIdle = true;
		GameObject[] tThose;
		tThose = GameObject.FindGameObjectsWithTag ("Warrior");
		foreach (GameObject tThat in tThose) {
			if (tThat.GetComponent<Scr_ProtagonistAction>().vAnimationState != "Idle")
				tEveryoneisIdle = false;
		}
		tThose = GameObject.FindGameObjectsWithTag ("Mage");
		foreach (GameObject tThat in tThose) {
			if (tThat.GetComponent<Scr_ProtagonistAction>().vAnimationState != "Idle")
				tEveryoneisIdle = false;
		}
		tThose = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject tThat in tThose) {
			if (tThat.GetComponent<Scr_AntagonistAction>().vAnimationState != "Idle")
				tEveryoneisIdle = false;
		}
		tThose = GameObject.FindGameObjectsWithTag ("AnimationObject");
		foreach (GameObject tThat in tThose) {
			tEveryoneisIdle = false;
		}
		vDebugIsEveryoneIdle = tEveryoneisIdle;
		return tEveryoneisIdle;
	}
	void LineControl(){
		float tDistance = Vector3.Distance(Object_Warrior.transform.position,Object_Mage.transform.position);
		cLR.SetPosition(0,Object_Warrior.transform.position);
		cLR.SetPosition(1,Object_Mage.transform.position);
		if (tDistance >= 5f)
			cLR.enabled = true;
		else
			cLR.enabled = false;
	}
}
