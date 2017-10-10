using System.Collections;
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
	private Rigidbody cRB;
	public string vDirection;

	public Scr_TargetingSystem cTS;
	public float vLookDirection; // Direction of the amibo after doing an action;
	public LayerMask lOccupatedCheck;
	public LayerMask lPitLayer;
	public GameObject vOtherPartner; // privatable
	public GameObject vSkillBall;
	public GameObject vSwipeObject;
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
			case "Push":
				PushSpell();
				tSkilling = true;
				vNextVect3 = transform.position;
				break;
			case "Bash":
				BashSpell();
				tSkilling = true;
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
		if (Other.tag == "Wall" || Other.tag == "Breakable" || Other.tag == "Movable" ) {
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

		}
	}
	void Falling(){
		if (vOtherPartner.GetComponent<Scr_ProtagonistAction>().vAnimationState == "Falling")
			return;
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
			}
	}
	void AttackEnemy(){
		if (cTS.vCurrentTarget != null) {
			vLookDirection = Vector2.Angle(new Vector2(this.transform.position.x,this.transform.position.z),new Vector2(cTS.transform.position.x,cTS.transform.position.z));
			if (Vector3.Distance (this.transform.position, cTS.vCurrentTarget.transform.position) < 1.9f) {
				//Debug.Log ("I Attacked " + cTS.vCurrentTarget.name);
				//Temporaryattack = this.GetComponent<AttackScript>().Attackpoints;
				//cTS.vCurrentTarget.GetComponent<Prameters> ().Damage = Temporaryattack;
				Vector3 tGoto;
				tGoto = cTS.vCurrentTarget.transform.position;
				Vector3 tMyXZ = this.transform.position;
				float tDifferenceX = tMyXZ.x - tGoto.x;
				float tDifferenceY = tMyXZ.z - tGoto.z;
				float tAngle;
				tAngle = Mathf.Atan2 (tDifferenceX,tDifferenceY)*180/Mathf.PI;
				tMyXZ.x -= tDifferenceX/2f;
				tMyXZ.y = 1.5f;
				tMyXZ.z -= tDifferenceY/2f;
				GameObject tTemp = Instantiate(vSwipeObject) as GameObject;
				tTemp.transform.position = tMyXZ;
				tTemp.GetComponent<Scr_SwipeEffect>().vFacingDirection = tAngle;
				if (cTS.vCurrentTarget.tag == "Enemy")
					cTS.vCurrentTarget.GetComponent<Scr_SFX_Damage_Blinker> ().vBlinkFrame += .01f;
				else if (cTS.vCurrentTarget.tag == "Targetable")
					cTS.vCurrentTarget.GetComponent<Scr_Switch> ().GetHit();// += .01f;
			}
		}
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
		/*
		if (GameObject.FindGameObjectsWithTag ("Breakable").Length > 0) {
			GameObject tWall = GameObject.FindGameObjectWithTag ("Breakable");
			float tOX = tWall.transform.position.x,
			tWX = .transform.position.x,
			tOZ = tWall.transform.position.z,
			tWZ = Object_Warrior.transform.position.z;
			if (Vector2.Distance (new Vector2 (tOX, tOZ), new Vector2 (tWX, tWZ)) < 2f) {
				cWPA.vAnimationState = "StartActing";
				Destroy (tWall.gameObject);
				cWPA.vInputType = "Wait";
			}
		}*/
	}
	void BashSpell(){
		GameObject tObj;
		Scr_SkillBall tStat;
		tObj = Instantiate(vSkillBall) as GameObject;
		tObj.transform.position = transform.position;
		tStat = tObj.GetComponent<Scr_SkillBall>();
		tStat.vSkillType = "Bash";
		}

	void PushSpell(){
		GameObject tObj;
		Scr_SkillBall tStat;
		tObj = Instantiate(vSkillBall) as GameObject;
		tObj.transform.position = transform.position;
		tStat = tObj.GetComponent<Scr_SkillBall>();
		tStat.vSkillType = "Push";
		/*
		float tMX = Object_Mage.transform.position.x,
		tWX = Object_Warrior.transform.position.x,
		tMZ = Object_Mage.transform.position.z,
		tWZ = Object_Warrior.transform.position.z;
		cMPA.vAnimationState = "StartActing";
		cMPA.vInputType = "Wait";
		if (Vector2.Distance(new Vector2(tMX,tMZ),new Vector2(tWX,tWZ))<1.1f){
			if ((tMX-tWX)*Mathf.Sign(tMX-tWX) > (tMZ-tWZ)*Mathf.Sign(tMZ-tWZ)) {
				cWPA.vAnimationState = "StartActing";
				if (tMX < tWX)
					cWPA.vInputType = "PushUp";
				if (tMX > tWX)
					cWPA.vInputType = "PushDown";
			} else {
				cWPA.vAnimationState = "StartActing";
				if (tMZ < tWZ)
					cWPA.vInputType = "PushRight";
				if (tMZ > tWZ)
					cWPA.vInputType = "PushLeft";
			}
		}*/
	}
}
