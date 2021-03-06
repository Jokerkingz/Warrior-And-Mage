﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_AntagonistAction : MonoBehaviour {
	public Vector3 vPrevVect3;
	public Vector3 vNextVect3;
	public string vOwner = "Slime";
	public string vAnimationState;
	public float vAnimationFrame;
	public string vInputType;
	public AnimationCurve vBounce;
	public Scr_Global vGlobal; 
	private Rigidbody cRB;
	public string vDirection;
	public GameObject vModel;

	public Scr_TargetingSystem cTS;
	public float vLookDirection; // Direction of the amibo after doing an action;
	public LayerMask lPitLayer;



	// Use this for initialization
	void Start () {
		vAnimationState = "Idle";
		vPrevVect3 = this.transform.position;
		vNextVect3 = this.transform.position;
		vAnimationFrame = 0;
		vGlobal = GameObject.FindGameObjectWithTag ("GameController").GetComponent<Scr_Global> ();
		cRB = this.GetComponent<Rigidbody>();
		cTS = this.GetComponent<Scr_TargetingSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		switch (vAnimationState) {
		case "Idle":
			break;
		case "StartActing":
			vPrevVect3 = transform.position;
			switch (vInputType) {
			case "MoveUp":
				vDirection = "North";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,1);
				break;
			case "MoveDown":
				vDirection = "South";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,1);
				break;
			case "MoveLeft":
				vDirection = "West";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,1);
				break;
			case "MoveRight":
				vDirection = "East";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,1);
				break;
			case "PushN":
				vDirection = "North";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,2);
				break;
			case "PushS":
				vDirection = "South";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,2);
				break;
			case "PushW":
				vDirection = "West";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,2);
				break;
			case "PushE":
				vDirection = "East";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,2);
				break;
			case "PushNE":
				vDirection = "NorthEast";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,1);
				break;
			case "PushSE":
				vDirection = "SouthEast";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,1);
				break;
			case "PushSW":
				vDirection = "SouthWest";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,1);
				break;
			case "PushNW":
				vDirection = "NorthWest";
				vNextVect3 = transform.position + DirectionToPoint(vDirection,1);
				break;

			case "BasicAttack":
				vNextVect3 = transform.position;
				AttackEnemy ();
				break;
			case "Wait":
				vNextVect3 = transform.position;
				break;
			}
			// Snap
			vPrevVect3 = new Vector3(Mathf.Round(vPrevVect3.x),1f,Mathf.Round(vPrevVect3.z));
			vNextVect3 = new Vector3(Mathf.Round(vNextVect3.x),1f,Mathf.Round(vNextVect3.z));
			vAnimationState = "Move";
			vAnimationFrame = 0f;
			break;

		case "Move":
			//vAnimationFrame += .05f // is correct
			vAnimationFrame = vGlobal.Global_AnimationFrame;
			if (vGlobal.vCurrentTurnState == "PlayerEndAnimate") {
				vAnimationFrame = 0f;
				vAnimationState = "Idle";
				PitFallCheck();
				vInputType = "Wait";
				transform.position = Vector3.Lerp (vPrevVect3, vNextVect3, 1f);
				// Snap
				transform.position = new Vector3(Mathf.Round(transform.position.x),1f,Mathf.Round(transform.position.z));
			}else if (vGlobal.vCurrentTurnState == "AIEndAnimate") {
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
		case "MoveCorrect":
			vAnimationFrame += .05f; // is correct
			if (vAnimationFrame > 1f) {
				vAnimationFrame = 0f;
				vAnimationState = "Idle";
				PitFallCheck();
				vInputType = "Wait";
				transform.position = Vector3.Lerp (vPrevVect3, vNextVect3, 1f);
				transform.position = new Vector3(Mathf.Round(transform.position.x),1f,Mathf.Round(transform.position.z));
			} else {
				transform.position = Vector3.Lerp (vPrevVect3, vNextVect3, vAnimationFrame);
				transform.position = new Vector3(transform.position.x,vBounce.Evaluate (vAnimationFrame)+1f,transform.position.z);
			}
			break;
		case "MoveBack":
			vAnimationFrame -=  2f*Time.unscaledDeltaTime;// .05f is correct
			if (vAnimationFrame < 0f) {
				vAnimationFrame = 0f;
				vAnimationState = "Idle";
				vInputType = "Wait";
				transform.position = Vector3.Lerp (vPrevVect3, vNextVect3, 0f);
				transform.position = new Vector3(Mathf.Round(transform.position.x),1f,Mathf.Round(transform.position.z));
			} else {
				transform.position = Vector3.Lerp (vPrevVect3, vNextVect3, vAnimationFrame);
				transform.position = new Vector3(transform.position.x,vBounce.Evaluate (vAnimationFrame)+1f,transform.position.z);
			}
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
		else if (Other.tag == "Warrior" || Other.tag == "Mage" || Other.tag == "Enemy" ){
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

		}
	}
	void Falling(){
		Destroy(this.gameObject);

		}

	//void AttackEnemy(){
		/* Generate a swipe in a given spot
		if (GameObject.FindGameObjectsWithTag ("Enemy").Length > 0) {
			float tOX = Object_Orc.transform.position.x,
			tWX = Object_Warrior.transform.position.x,
			tOZ = Object_Orc.transform.position.z,
			tWZ = Object_Warrior.transform.position.z;
			if (Vector2.Distance (new Vector2 (tOX, tOZ), new Vector2 (tWX, tWZ)) < 2f) {
				Object_Orc.GetComponent<Scr_SFX_Damage_Blinker> ().vBlinkFrame = .01f;
			}
			Object_Warrior.GetComponent<Scr_ActionAnimation> ().vAnimationState = "StartActing";
			Object_Warrior.GetComponent<Scr_ActionAnimation> ().vInputType = "Wait";
		}
		*/
	//}

	void AttackEnemy(){
		if (cTS.vCurrentTarget != null) {
			vLookDirection = Vector2.Angle(new Vector2(this.transform.position.x,this.transform.position.z),new Vector2(cTS.transform.position.x,cTS.transform.position.z));
			if (Vector3.Distance (this.transform.position, cTS.transform.position) < 2f) {
				//Debug.Log ("I Attacked " + cTS.vCurrentTarget.name);
				cTS.vCurrentTarget.GetComponent<Scr_SFX_Damage_Blinker> ().vBlinkFrame += .01f;
				float Damage = this.GetComponent<attackStat> ().DamageCalculation();
				cTS.vCurrentTarget.GetComponent<defenseStat> ().DamageEquation (Damage);


				/// Do Attack 8===D
				if (vOwner == "Slime")
				this.GetComponent<Scr_CreateEffect>().Activate(cTS.vCurrentTarget.transform.position,0);

			}
		}

		/*
		if (GameObject.FindGameObjectsWithTag ("Enemy").Length > 0) {
			float tOX = Object_Orc.transform.position.x,
			tWX = Object_Warrior.transform.position.x,
			tOZ = Object_Orc.transform.position.z,
			tWZ = Object_Warrior.transform.position.z;
			if (Vector2.Distance (new Vector2 (tOX, tOZ), new Vector2 (tWX, tWZ)) < 2f) {
				Object_Orc.GetComponent<Scr_SFX_Damage_Blinker> ().vBlinkFrame = .01f;
			}
			Object_Warrior.GetComponent<Scr_ActionAnimation> ().vAnimationState = "StartActing";
			Object_Warrior.GetComponent<Scr_ActionAnimation> ().vInputType = "Wait";
		}'*/
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
}
