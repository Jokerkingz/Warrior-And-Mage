﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ProtagonistAction : MonoBehaviour {
	public Vector3 vPrevVect3;
	public Vector3 vNextVect3;
	public string vAnimationState;
	public float vAnimationFrame;
	public string vInputType;
	public AnimationCurve vBounce;
	public Scr_Global vGlobal;
	private Scr_CreateEffect cCE;
	private Scr_AnimationControl cAC;
	private Rigidbody cRB;
	public string vDirection;

	public Scr_TargetingSystem cTS;
	public float vLookDirection; // Direction of the amibo after doing an action;
	public LayerMask lOccupatedCheck;
	public LayerMask lPitLayer;
	public GameObject vOtherPartner; // privatable
	public GameObject vSkillBall;
	public GameObject vSwipeObject;
	public GameObject vProjectile;
	// Note: Do not try Place holder for the protagonist because it WILL know if they will collide, but it will be tougher to calculate when the character is pushed. So Dont add a PlaceHolder object or script.


	// Use this for initialization
	void Start () {
		vAnimationState = "Idle";
		vPrevVect3 = this.transform.position;
		vNextVect3 = this.transform.position;
		vAnimationFrame = 0;
		vGlobal = GameObject.FindGameObjectWithTag ("GameController").GetComponent<Scr_Global> ();
		cRB = this.GetComponent<Rigidbody>();
		cTS = this.GetComponent<Scr_TargetingSystem>();
		cAC = this.GetComponent<Scr_AnimationControl>();
		cCE = this.GetComponent<Scr_CreateEffect>();
		if (this.tag=="Mage")
			vOtherPartner = GameObject.FindGameObjectWithTag("Warrior");
		else  if (this.tag == "Warrior")
			vOtherPartner = GameObject.FindGameObjectWithTag("Mage");
	}

	// Update is called once per frame
	void Update () {
		
			
		bool tSkilling = false;
		switch (vAnimationState) {
		case "Idle":
			break;
		case "StartActing":
			vPrevVect3 = transform.position;
			//Debug.Log(vInputType);
			switch (vInputType) {
			case "MoveUp":
				vDirection = "North";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,1);
				cAC.Act("Idle",DirectionToPoint(vDirection,1));
				break;
			case "MoveDown":
				vDirection = "South";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,1);
				cAC.Act("Idle",DirectionToPoint(vDirection,1));
				break;
			case "MoveLeft":
				vDirection = "West";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,1);
				cAC.Act("Idle",DirectionToPoint(vDirection,1));
				break;
			case "MoveRight":
				vDirection = "East";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,1);
				cAC.Act("Idle",DirectionToPoint(vDirection,1));
				break;
			case "PushN":
				vDirection = "North";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,2);
				cAC.Act("Idle",DirectionToPoint(vDirection,1));
				break;
			case "PushS":
				vDirection = "South";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,2);
				cAC.Act("Idle",DirectionToPoint(vDirection,1));
				break;
			case "PushW":
				vDirection = "West";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,2);
				cAC.Act("Idle",DirectionToPoint(vDirection,1));
				break;
			case "PushE":
				vDirection = "East";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,2);
				cAC.Act("Idle",DirectionToPoint(vDirection,1));
				break;
			case "PushNE":
				vDirection = "NorthEast";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,1);
				cAC.Act("Idle",DirectionToPoint(vDirection,1));
				break;
			case "PushSE":
				vDirection = "SouthEast";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,1);
				cAC.Act("Idle",DirectionToPoint(vDirection,1));
				break;
			case "PushSW":
				vDirection = "SouthWest";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,1);
				cAC.Act("Idle",DirectionToPoint(vDirection,1));
				break;
			case "PushNW":
				vDirection = "NorthWest";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,1);
				cAC.Act("Idle",DirectionToPoint(vDirection,1));
				break;

			case "Basic Attack":
				vNextVect3 = transform.position;
				AttackEnemy ();
				break;
			case "Wait":
				vNextVect3 = transform.position;
				this.GetComponent<statManager>().SpecRegen(4);
				break;
			case "Push":
				if (this.GetComponent<statManager>().curSpec >= 15){
					this.GetComponent<statManager>().SpecDrain(15);
					PushSpell();
					tSkilling = true;
				} else 
				this.GetComponent<statManager>().SpecRegen(1);
				vNextVect3 = transform.position;
				break;
			case "Bash":
				if (this.GetComponent<statManager>().curSpec >= 10){
					this.GetComponent<statManager>().SpecDrain(10);
					BashSpell();
					tSkilling = true;
				} else 
				this.GetComponent<statManager>().SpecRegen(1);
				vNextVect3 = transform.position;
				break;
			case "Ice Spear":
				if (this.GetComponent<statManager>().curSpec >= 20){
					this.GetComponent<statManager>().SpecDrain(20);
					IceSpearSpell();
					tSkilling = true;
				} else 
				this.GetComponent<statManager>().SpecRegen(2);
				vNextVect3 = transform.position;
				break;
			}
			// Snap
			vPrevVect3 = new Vector3(Mathf.Round(vPrevVect3.x),1f,Mathf.Round(vPrevVect3.z));
			vNextVect3 = new Vector3(Mathf.Round(vNextVect3.x),1f,Mathf.Round(vNextVect3.z));
			if (tSkilling)
				vAnimationState = "Skill";
			else 
				vAnimationState = "Move";
			vAnimationFrame = 0f;
			break;

		case "Move":
			vAnimationFrame = vGlobal.Global_AnimationFrame;
			if (vGlobal.vCurrentTurnState == "PlayerEndAnimate") {
				vAnimationFrame = 0f;
				vAnimationState = "Idle";

				PitFallCheck();
				vInputType = "Wait";
				transform.position = Vector3.Lerp (vPrevVect3, vNextVect3, 1f);
				// Snap
				transform.position = new Vector3(Mathf.Round(transform.position.x),1f,Mathf.Round(transform.position.z));
			} else {
				transform.position = Vector3.Lerp (vPrevVect3, vNextVect3, vAnimationFrame);
				transform.position = new Vector3(transform.position.x,vBounce.Evaluate (vAnimationFrame)+1f,transform.position.z);
			}
			break;
		case "Falling":
			if (transform.position.y < -10f)
				Falling();
			break;
		case "MoveBack":
			vAnimationFrame -=  2f*Time.unscaledDeltaTime;// .05f is correct
			if (vAnimationFrame < 0f) {
				vAnimationFrame = 0f;
				vAnimationState = "Idle";

				PitFallCheck();
				vInputType = "Wait";
				transform.position = Vector3.Lerp (vPrevVect3, vNextVect3, 0f);
				transform.position = new Vector3(Mathf.Round(transform.position.x),1f,Mathf.Round(transform.position.z));
			} else {
				transform.position = Vector3.Lerp (vPrevVect3, vNextVect3, vAnimationFrame);
				transform.position = new Vector3(transform.position.x,vBounce.Evaluate (vAnimationFrame)+1f,transform.position.z);
			}
			break;
		case "Skill":
			vAnimationFrame = vGlobal.Global_AnimationFrame;
			if (vGlobal.vCurrentTurnState == "PlayerEndAnimate") {
				vAnimationFrame = 0f;
				vAnimationState = "Idle";
				PitFallCheck();
				vInputType = "Wait";
				transform.position = Vector3.Lerp (vPrevVect3, vNextVect3, 1f);
				// Snap
				transform.position = new Vector3(Mathf.Round(transform.position.x),1f,Mathf.Round(transform.position.z));}
		break;
		}
	}
	void OnTriggerStay(Collider Other){
		if (Other.tag == "Wall" || Other.tag == "Breakable" || Other.tag == "Movable" || Other.tag == "Targetable") {
			if (vAnimationState == "Move") {
				vAnimationState = "MoveBack";
				vPrevVect3 = new Vector3 (Mathf.Round (transform.position.x), 1f, Mathf.Round (transform.position.z));
			}
		}
		else if (Other.tag == "Warrior" || Other.tag == "Mage" || Other.tag == "Enemy" ) {
			if (vAnimationState == "Move") {
				vAnimationState = "MoveBack";
				}
			}
	}

	public void PitFallCheck(){
		Ray tRay;
		Vector3 tMySpot = this.transform.position;
		tRay = new Ray (tMySpot,  new Vector3(0f,-2f,0f));
		if (Physics.Raycast (tRay, 2f, lPitLayer)){
			cRB.useGravity = true;
			cRB.isKinematic = false;
			vAnimationState = "Falling";
			cAC.Act("Fall",Vector3.zero);
		}
		else if (Vector3.Distance(this.transform.position,vOtherPartner.transform.position) > 7f)
			Falling();
	}
	void Falling(){
		cAC.Act("Idle",DirectionToPoint(vDirection,1));
		if (!FindSpot(vGlobal.vFallCheckPoint))
			{if (vOtherPartner.GetComponent<Scr_ProtagonistAction>().vAnimationState == "Falling")
				FindSpot(vGlobal.vFallCheckPoint);
			else {if (!FindSpot(vOtherPartner))
				FindSpot(vGlobal.vFallCheckPoint);
			}
		}
		if (Vector3.Distance(this.transform.position,vOtherPartner.transform.position) > 6f){
			vOtherPartner.GetComponent<Scr_ProtagonistAction>().FindSpot(vGlobal.vFallCheckPoint);
			//FindSpot(vGlobal.vFallCheckPoint);

		}
			/*
		Ray tRay;
		Vector3 tMySpot = vOtherPartner.transform.position;
		bool tDone = false; {
			tMySpot.x -= 1f;
			tMySpot.y += 2f;
			tRay = new Ray (tMySpot,  new Vector3(0f,-5f,0f));
			if (!Physics.Raycast (tRay, 5f, lOccupatedCheck)){
				tMySpot.y -= 2f;
				transform.position = tMySpot;
				cRB.useGravity = false;
				cRB.isKinematic = true;
				cRB.velocity = Vector3.zero;
				vAnimationState = "Idle";
				tDone = true;
			}
			tMySpot = vOtherPartner.transform.position;
			}
		if (!tDone) {
			tMySpot.z += 1f;
			tMySpot.y += 2f;
			tRay = new Ray (tMySpot,  new Vector3(0f,-5f,0f));
			if (!Physics.Raycast (tRay, 5f, lOccupatedCheck)){
				tMySpot.y -= 2f;
				transform.position = tMySpot;
				cRB.useGravity = false;
				cRB.isKinematic = true;
				cRB.velocity = Vector3.zero;
				vAnimationState = "Idle";
				tDone = true;
			}
		tMySpot = vOtherPartner.transform.position;}
		if (!tDone) {
			tMySpot.x += 1f;
			tMySpot.y += 2f;
			tRay = new Ray (tMySpot,  new Vector3(0f,-5f,0f));
			if (!Physics.Raycast (tRay, 5f, lOccupatedCheck)){
				tMySpot.y -= 2f;
				transform.position = tMySpot;
				cRB.useGravity = false;
				cRB.isKinematic = true;
				cRB.velocity = Vector3.zero;
				vAnimationState = "Idle";
				tDone = true;
				}
			tMySpot = vOtherPartner.transform.position;}
		if (!tDone){
			tMySpot.z -= 1f;
			tMySpot.y += 2f;
			tRay = new Ray (tMySpot,  new Vector3(0f,-5f,0f));
			if (!Physics.Raycast (tRay, 5f, lOccupatedCheck)){
				tMySpot.y -= 2f;
				transform.position = tMySpot;
				cRB.useGravity = false;
				cRB.isKinematic = true;
				cRB.velocity = Vector3.zero;
				vAnimationState = "Idle";
				}
			}*/
	}
	public bool FindSpot(GameObject tObj){
		Ray tRay;
		Vector3 tMySpot = tObj.transform.position;
		bool tDone = false;
			{
			tMySpot.x -= 1f;
			tMySpot.y += 2f;
			tRay = new Ray (tMySpot,  new Vector3(0f,-5f,0f));
			if (!Physics.Raycast (tRay, 5f, lOccupatedCheck)){
				tMySpot.y -= 2f;
				transform.position = tMySpot;
				cRB.useGravity = false;
				cRB.isKinematic = true;
				cRB.velocity = Vector3.zero;
				vAnimationState = "Idle";
				tDone = true;
			}
			tMySpot = tObj.transform.position;
			}
		if (!tDone) {
			tMySpot.z += 1f;
			tMySpot.y += 2f;
			tRay = new Ray (tMySpot,  new Vector3(0f,-5f,0f));
			if (!Physics.Raycast (tRay, 5f, lOccupatedCheck)){
				tMySpot.y -= 2f;
				transform.position = tMySpot;
				cRB.useGravity = false;
				cRB.isKinematic = true;
				cRB.velocity = Vector3.zero;
				vAnimationState = "Idle";
				tDone = true;
			}
			tMySpot = tObj.transform.position;}
		if (!tDone) {
			tMySpot.x += 1f;
			tMySpot.y += 2f;
			tRay = new Ray (tMySpot,  new Vector3(0f,-5f,0f));
			if (!Physics.Raycast (tRay, 5f, lOccupatedCheck)){
				tMySpot.y -= 2f;
				transform.position = tMySpot;
				cRB.useGravity = false;
				cRB.isKinematic = true;
				cRB.velocity = Vector3.zero;
				vAnimationState = "Idle";
				tDone = true;
				}
			tMySpot = tObj.transform.position;}
		if (!tDone){
			tMySpot.z -= 1f;
			tMySpot.y += 2f;
			tRay = new Ray (tMySpot,  new Vector3(0f,-5f,0f));
			if (!Physics.Raycast (tRay, 5f, lOccupatedCheck)){
				tMySpot.y -= 2f;
				transform.position = tMySpot;
				cRB.useGravity = false;
				cRB.isKinematic = true;
				cRB.velocity = Vector3.zero;
				vAnimationState = "Idle";
				}
			}
		return tDone;
	}
	void AttackEnemy(){
		if (cTS.vCurrentTarget != null) {
			if (Vector3.Distance (this.transform.position, cTS.vCurrentTarget.transform.position) < 1.9f) {
				
				DoAttack();
			}
			else {cTS.vCurrentTarget = cTS.NearestTarget();
				if (Vector3.Distance (this.transform.position, cTS.vCurrentTarget.transform.position) < 1.9f)
					DoAttack();
			}
			vLookDirection = Vector2.Angle(new Vector2(this.transform.position.x,this.transform.position.z),new Vector2(cTS.transform.position.x,cTS.transform.position.z));
		}
	}
	void DoAttack(){
		vLookDirection = Vector2.Angle(new Vector2(this.transform.position.x,this.transform.position.z),new Vector2(cTS.transform.position.x,cTS.transform.position.z));
		Vector3 tGoto;
		tGoto = cTS.vCurrentTarget.transform.position;
		Vector3 tMyXZ = this.transform.position;
		float tDifferenceX = tMyXZ.x - tGoto.x;
		float tDifferenceY = tMyXZ.z - tGoto.z;
		float tAngle;
		tAngle = Mathf.Atan2 (tDifferenceX, tDifferenceY) * 180 / Mathf.PI;
		tMyXZ.x -= tDifferenceX / 2f;
		tMyXZ.y = 1.5f;
		tMyXZ.z -= tDifferenceY / 2f;
		GameObject tTemp = Instantiate (vSwipeObject) as GameObject;
		tTemp.transform.position = tMyXZ;
		tTemp.GetComponent<Scr_SwipeEffect> ().vFacingDirection = tAngle;
		cAC.Act ("Swing", Vector3.Normalize (new Vector3 (-tDifferenceX, 0f, -tDifferenceY)));
		if (cTS.vCurrentTarget.tag == "Enemy") {
			cTS.vCurrentTarget.GetComponent<Scr_SFX_Damage_Blinker> ().vBlinkFrame += .01f;
			//float Damage = 3f;//GetComponent<attackStat> ().DamageCalculation();
			float Damage = this.GetComponent<attackStat> ().DamageCalculation();
			cTS.vCurrentTarget.GetComponent<defenseStat> ().DamageEquation (Damage);

		
		}
		else if (cTS.vCurrentTarget.tag == "Targetable")
			cTS.vCurrentTarget.GetComponent<Scr_Switch> ().GetHit();// += .01f;
	}

	Vector3 DirectionToPoint(string tDirection,int tMultiplier){
		Vector3 tResult;
		switch (tDirection) {
		case "North":
			tResult = (new Vector3 (-1f, 0f, 0f) * tMultiplier);
			break;
		case "East":
			tResult = (new Vector3 (0f, 0f, 1f) * tMultiplier);
			break;
		case "South":
			tResult = (new Vector3 (1f, 0f, 0f) * tMultiplier);
			break;
		case "West":
			tResult = (new Vector3 (0f, 0f, -1f) * tMultiplier);
			break;
		case "NorthEast":
			tResult = (new Vector3 (-1f, 0f, 1f) * tMultiplier);
			break;
		case "SouthEast":
			tResult = (new Vector3 (1f, 0f, 1f) * tMultiplier);
			break;
		case "SouthWest":
			tResult = (new Vector3 (1f, 0f, -1f) * tMultiplier);
			break;
		case "NorthWest":
			tResult = (new Vector3 (-1f, 0f, -1f) * tMultiplier);
			break;
		default:
			tResult = (new Vector3 (0f, 0f, -1f) * tMultiplier);
			break;
		}
		return tResult;
	}
	/// Skill List ///
	void SkillSort(string tWhichSkill){
	}

	void BashSpell(){
		GameObject tObj;
		Scr_SkillBall tStat;
		tObj = Instantiate(vSkillBall) as GameObject;
		tObj.transform.position = transform.position;
		tStat = tObj.GetComponent<Scr_SkillBall>();
		tStat.vSkillType = "Bash";
		this.GetComponent<Scr_CreateEffect>().Activate(this.transform.position,0);
		cAC.Act("Spell1",Vector3.Normalize (new Vector3 (1f, 0f, 0f)));
		}

	void PushSpell(){
		GameObject tObj;
		Scr_SkillBall tStat;
		tObj = Instantiate(vSkillBall) as GameObject;
		tObj.transform.position = transform.position;
		tStat = tObj.GetComponent<Scr_SkillBall>();
		tStat.vSkillType = "Push";
		cAC.Act("Spell3",Vector3.zero);
		Vector3 tTemp = this.transform.position;
		tTemp.y += 2f;
		this.GetComponent<Scr_CreateEffect>().Activate(tTemp,0);
		cAC.Act("Spell3",Vector3.Normalize (new Vector3 (1f, 0f, 0f)));
	}
	void IceSpearSpell(){
		if (cTS.vCurrentTarget != null){
			vLookDirection = Vector2.Angle(new Vector2(this.transform.position.x,this.transform.position.z),new Vector2(cTS.transform.position.x,cTS.transform.position.z));

			Debug.Log("Created");
			GameObject tObj;
			Scr_IceSpear tStat;
			tObj = Instantiate(vProjectile) as GameObject;
			tObj.transform.position = transform.position;
			tStat = tObj.GetComponent<Scr_IceSpear>();
			tStat.vTarget = cTS.vCurrentTarget.gameObject;

			float Damage = this.GetComponent<attackStat> ().DamageCalculation()*2f;
			tStat.vCarriedValue = Damage;


			Vector3 tGoto;
			tGoto = cTS.vCurrentTarget.transform.position;
			Vector3 tMyXZ = this.transform.position;
			float tDifferenceX = tMyXZ.x - tGoto.x;
			float tDifferenceY = tMyXZ.z - tGoto.z;
			float tAngle;
			tAngle = Mathf.Atan2 (tDifferenceX, tDifferenceY) * 180 / Mathf.PI;
			tMyXZ.x -= tDifferenceX / 2f;
			tMyXZ.y = 1.5f;
			tMyXZ.z -= tDifferenceY / 2f;

			//tStat.vCarriedValue = this.GetComponent<attackStat> ().DamageCalculation();
			cAC.Act("Spell1",Vector3.Normalize (new Vector3 (-tDifferenceX, 0f, -tDifferenceY)));
		}
	}
}
