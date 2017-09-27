using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_AIControl : MonoBehaviour {

	public Vector3 vChaseLocation;
	public float vAttackDistance;
	public float vVisionDistance;
	public float vAttackDirection;

	public string vStatus; // Idle Chase Attack
	public LayerMask vWallLayer;
	public Scr_TargetingSystem cTS;
	public Scr_Global gGlobal;
	private Scr_AntagonistAction cAA;
	public char tSHowChar;

	public string results;
	public string tWhatIsOnHere;
	public GameObject vPlaceHolder;

	private GameObject vObjWarrior;
	private GameObject vObjMage;

	private Vector3 vChaseSpot;
	// Use this for initialization
	void Start () {
		vStatus = "Idle";
		cTS = GetComponent<Scr_TargetingSystem> ();
		gGlobal = GameObject.FindGameObjectWithTag ("GameController").GetComponent<Scr_Global> ();
		cAA = this.GetComponent<Scr_AntagonistAction>();
		vObjWarrior = GameObject.FindGameObjectWithTag ("Warrior");
			vObjMage = GameObject.FindGameObjectWithTag ("Mage");
		//vSpotChecker = Instantiate (vSpot_Prefab) as GameObject;
		//tSC = 	vSpotChecker.GetComponent<Scr_SpotChecker> ();
	}

	// Update is called once per frame
	void Update () {
		if (vStatus == "MyTurn"){
			if (gGlobal.Global_AnimationState == "Animate")
				gGlobal.Global_AnimationState = "StartAnimate";
			ControlAI ();
			}
		//if (gGlobal.Global_AnimationState = "StartAnimate")
			
		//if (cTS.vCurrentTarget != null)
		//WalkToward ();
	}
	void ControlAI(){
		vStatus = "Doing";
		char tTemp;
		char[] tArray = new char[4]{'N','E','S','W'};
		cTS.SendMessage("NearestTarget");
		if (cTS.vCurrentTarget != null) {// I Found Someone!!;
			//cTS.NearestTarget();
			if (Vector3.Distance (cTS.vCurrentTarget.transform.position, this.transform.position) < vAttackDistance) { // Within Distance, Attack
				vStatus = "Chase";
				//cTS.vCurrentTarget = vObjWarrior.gameObject;
				vChaseLocation = cTS.vCurrentTarget.transform.position;
				AttackSomeone ();
				Debug.Log ("Attack"); }
			else{ WalkToward ();
				Debug.Log ("WalkToward"); }
			return;
			}
		else
			{tTemp = tArray[Mathf.FloorToInt(Random.Range(0f,3.99f))];
			Debug.Log ("MoveAction "+ tTemp);
			MoveAction (tTemp);
		}
	}


	void AttackSomeone(){
		cAA.vAnimationState = "StartActing";
		cAA.vInputType = "BasicAttack";
	}

	void WalkToward(){
		char tNESW;
		tNESW = 'X';
		if (cTS.vCurrentTarget != null) {
			tNESW = PointToDirection ();
		}
		//vAttackDirection = Mathf.Atan2 (tDifferenceX,tDifferenceY)*180/Mathf.PI;
		MoveCheckGo(tNESW);
		tSHowChar = tNESW;
		//Debug.Log (tDirection);
	}

	char PointToDirection(){
		char tResult = 'X';
		Vector3 tGoto;
		tGoto = cTS.vCurrentTarget.transform.position;
		Vector3 tMyXZ = this.transform.position;
		float tDifferenceX = tMyXZ.x - tGoto.x;
		float tDifferenceY = tMyXZ.z - tGoto.z;
		float tAngle;
		tAngle = Mathf.Atan2 (tDifferenceX,tDifferenceY)*180/Mathf.PI;
		if (tAngle < -135)
			tResult = 'E';
		else if (tAngle < -45)
			tResult = 'S';
		else if (tAngle < 45)
			tResult = 'W';
		else if (tAngle < 135)
			tResult = 'N';
		else 
			tResult = 'E';
		return tResult;
	}

	void MoveCheckGo(char tDirection){
		char tResult = tDirection;
		Ray tRay;
		Vector3 tMySpot = this.transform.position;
		tMySpot.y += 1f;

		if (cTS.vCurrentTarget != null)
			vChaseSpot = cTS.vCurrentTarget.transform.position;
		switch (tDirection) {
		case 'N':
			tMySpot.x -= 1f;
			tMySpot.y += 2f;
			tRay = new Ray (tMySpot,  new Vector3(0f,-5f,0f));
			if (!Physics.Raycast (tRay, 5f, vWallLayer)){
				tResult = 'N';}
			else {if (tMySpot.z < vChaseSpot.z)
					tResult = 'E';
			else if (tMySpot.z > vChaseSpot.z)
						tResult = 'W';
					else
						tResult = 'X';
				}
			break;
		case 'E':
			tMySpot.z += 1f;
			tMySpot.y += 2f;
			tRay = new Ray (tMySpot,  new Vector3(0f,-5f,0f));
			if (!Physics.Raycast (tRay, 5f, vWallLayer)){
				tResult = 'E';}
			else {
				if (tMySpot.x < vChaseSpot.x)
					tResult = 'S';
				else if (tMySpot.x > vChaseSpot.x)
					tResult = 'N';
				else
					tResult = 'X';
			}
			break;
		case 'S':
			tMySpot.x += 1f;
			tMySpot.y += 2f;
			tRay = new Ray (tMySpot,  new Vector3(0f,-5f,0f));
			if (!Physics.Raycast (tRay, 5f, vWallLayer)){
					tResult = 'S';}
				else {
				if (tMySpot.z < vChaseSpot.z)
						tResult = 'E';
				else if (tMySpot.z > vChaseSpot.z)
						tResult = 'W';
					else
						tResult = 'X';
				}
				break;
		case 'W':
			tMySpot.z -= 1f;
			tMySpot.y += 2f;
			tRay = new Ray (tMySpot,  new Vector3(0f,-5f,0f));
			if (!Physics.Raycast (tRay, 5f, vWallLayer)){
					tResult = 'W';}
				else {
				if (tMySpot.x < vChaseSpot.x)
						tResult = 'S';
				else if (tMySpot.x > vChaseSpot.x)
						tResult = 'N';
					else
						tResult = 'X';
				}
				break;

		}

		cAA.vAnimationState = "StartActing";
		tMySpot = this.transform.position;
		switch (tResult) {
		case 'N':
			tMySpot.x -= 1f;
			tMySpot.y += 2f;
			tRay = new Ray (tMySpot,  new Vector3(0f,-5f,0f));
			if (Physics.Raycast (tRay, 5f, vWallLayer))
				cAA.vInputType = "Wait";
			else
				cAA.vInputType = "MoveUp";
			break;
		case 'E':
			tMySpot.z += 1f;
			tMySpot.y += 2f;
			tRay = new Ray (tMySpot,  new Vector3(0f,-5f,0f));
			if (Physics.Raycast (tRay, 5f, vWallLayer))
				cAA.vInputType = "Wait";
			else
				cAA.vInputType = "MoveRight";
			break;
		case 'S':
			tMySpot.x += 1f;
			tMySpot.y += 2f;
			tRay = new Ray (tMySpot,  new Vector3(0f,-5f,0f));
			if (Physics.Raycast (tRay, 5f, vWallLayer))
				cAA.vInputType = "Wait";
			else
				cAA.vInputType = "MoveDown";
			break;
		case 'W':
			tMySpot.z -= 1f;
			tMySpot.y += 2f;
			tRay = new Ray (tMySpot,  new Vector3(0f,-5f,0f));
			if (Physics.Raycast (tRay, 5f, vWallLayer))
				cAA.vInputType = "Wait";
			else
				cAA.vInputType = "MoveLeft";
			break;
		default:
			cAA.vInputType = "Wait";
			break;
		}
		tMySpot.y = 1f;
		Instantiate(vPlaceHolder,tMySpot,Quaternion.Euler(Vector3.zero));
	}

	void MoveAction (char tToWhichDirection){
		Ray tRay;
		Vector3 tMySpot = this.transform.position;
		cAA.vAnimationState = "StartActing";
		switch (tToWhichDirection) {
		case 'N':
			tMySpot.x -= 1f;
			tMySpot.y += 2f;
			tRay = new Ray (tMySpot,  new Vector3(0f,-5f,0f));
			if (Physics.Raycast (tRay, 5f, vWallLayer))
				cAA.vInputType = "Wait";
			else
			cAA.vInputType = "MoveUp";
			break;
		case 'E':
		tMySpot.z += 1f;
			tMySpot.y += 2f;
			tRay = new Ray (tMySpot,  new Vector3(0f,-5f,0f));
			if (Physics.Raycast (tRay, 5f, vWallLayer))
				cAA.vInputType = "Wait";
			else
				cAA.vInputType = "MoveRight";
			break;
		case 'S':
			tMySpot.x += 1f;
			tMySpot.y += 2f;
			tRay = new Ray (tMySpot,  new Vector3(0f,-5f,0f));
			if (Physics.Raycast (tRay, 5f, vWallLayer))
				cAA.vInputType = "Wait";
			else
				cAA.vInputType = "MoveDown";
			break;
		case 'W':
			tMySpot.z -= 1f;
			tMySpot.y += 2f;
			tRay = new Ray (tMySpot,  new Vector3(0f,-5f,0f));
			if (Physics.Raycast (tRay, 5f, vWallLayer))
				cAA.vInputType = "Wait";
			else
				cAA.vInputType = "MoveLeft";
			break;
		default:
				cAA.vInputType = "Wait";
			break;
		}
		tMySpot.y = 1f;
		Instantiate(vPlaceHolder,tMySpot,Quaternion.Euler(Vector3.zero));
	}
}
