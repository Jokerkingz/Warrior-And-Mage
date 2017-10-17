using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TargetingSystem : MonoBehaviour {
	public int vTargetIndex;
	public GameObject vTargetedObject; // The Target
	public GameObject vPreFab;
	public GameObject vTarget; // The object used to target
	public float  vFarthestDistance = 10f;
	public string vTargetStatus;
	public List<GameObject> myTargets;
	public LayerMask vRayLayers;


	public string vEnemiesToTarget = "Protagonist";
	public GameObject vCurrentTarget = null;
	private float vSpinAngle;
	private Scr_Global gGlobal;
	// Use this for initialization
	void Start () {
		if (vEnemiesToTarget == "Antagonist") 
			vTarget = Instantiate (vPreFab)as GameObject;
		gGlobal = GameObject.FindGameObjectWithTag ("GameController").GetComponent<Scr_Global>();

	}

	void Update(){
	}

	// Update is called once per frame
	public void AfterMove() {

		if (vEnemiesToTarget == "Antagonist") {
			if (vCurrentTarget == null || gGlobal.vCurrentTurnState != "PlayerInputWait")
				vTarget.SetActive(false);
			else 
				{vTarget.SetActive(true);
				vTarget.transform.position = Vector3.Scale (vCurrentTarget.transform.position, new Vector3 (1f, 2f, 1f));
				vTarget.transform.eulerAngles = new Vector3 (0f, vSpinAngle, 0f);}
			}


		RecheckForTargets ();
		if (Input.GetKeyDown (KeyCode.Space)) {
			NextTarget ();
			Debug.Log ("Next Plez");
			//vTarget
		}
		if (vEnemiesToTarget == "Antagonist") {
			switch (this.tag) {
			case "Warrior":
				vSpinAngle = gGlobal.Global_WarriorIconRotation;
				break;
			case "Mage":
				vSpinAngle = gGlobal.Global_MageIconRotationt;
				break;
			}
			//if (vCurrentTarget != null) {
			//	vTarget.transform.position = Vector3.Scale (vCurrentTarget.transform.position, new Vector3 (1f, 2f, 1f));
			//}
			//vTarget.transform.eulerAngles = new Vector3 (0f, vSpinAngle, 0f);
		}
	}
	public void NextTarget (){
		if (vCurrentTarget == null)
		return;
		int tIndex = 0; // CurrentIndex of the list
		int tFoundIndex = 0; // found Index of list
		string tOldTarget = vCurrentTarget.name;
		GameObject tNextTarget = null;
		foreach (GameObject That in myTargets) { // checking
			if (That.name == tOldTarget) {
				tFoundIndex = tIndex;
			}
			tIndex += 1;
		}
		tFoundIndex = (tFoundIndex + 1) % tIndex;
		tNextTarget = myTargets [tFoundIndex].gameObject;
		vCurrentTarget = tNextTarget;


	}
	void RecheckForTargets(){
		if (vCurrentTarget != null) {
			string tOldTarget = vCurrentTarget.name;
			GameObject NewTarget = null;
			bool tIFoundHim = false;
			FindATarget (vEnemiesToTarget);
			foreach (GameObject That in myTargets) { // checking
				if (That.name == tOldTarget) {
					tIFoundHim = true;
					NewTarget = That.gameObject;
				}
			}
			if (!tIFoundHim)
				vCurrentTarget = NearestTarget ();
		} else {
			FindATarget (vEnemiesToTarget);
			vCurrentTarget = NearestTarget ();
		}
	}
	public GameObject NearestTarget(){
		float tClosestDistance = 20f;
		float tDistance;
		GameObject tClosestOne = null;
		foreach (GameObject That in myTargets) {
			tDistance = Vector3.Distance (this.transform.position, That.transform.position);
			if (tDistance < tClosestDistance) {
				tClosestOne = That;
				tClosestDistance = tDistance;
				}
		}
		return tClosestOne;
	}

	void FindATarget(string tTargetType){
		Ray rRay;
		GameObject tOwner = this.gameObject;
		Vector3 tAngle;
		Vector3 tTargetSpot;
		float tDistance;
		myTargets.Clear ();

		switch (tTargetType) {
		case "Protagonist":
			GameObject tWarrior = GameObject.FindGameObjectWithTag ("Warrior");
			tTargetSpot = tWarrior.transform.position;
			tDistance = Vector3.Distance (this.transform.position, tTargetSpot);
			if (tDistance < vFarthestDistance) {
				tAngle = tTargetSpot - this.transform.position;
				rRay = new Ray (tOwner.transform.position, tAngle);
				if (!Physics.Raycast (rRay, tDistance, vRayLayers)) {
					Debug.DrawRay (tOwner.transform.position, tAngle, Color.green);
					myTargets.Add (tWarrior.gameObject);
					}
				}
			GameObject tMage = GameObject.FindGameObjectWithTag ("Mage");
			tTargetSpot = tMage.transform.position;
			tDistance = Vector3.Distance (this.transform.position, tTargetSpot);
			if (tDistance < vFarthestDistance) {
				tAngle = tTargetSpot - this.transform.position;
				rRay = new Ray (tOwner.transform.position, tAngle);
				if (!Physics.Raycast (rRay, tDistance, vRayLayers)) {
					Debug.DrawRay (tOwner.transform.position, tAngle, Color.green);
					myTargets.Add (tMage.gameObject);
					}
				}
			vCurrentTarget = NearestTarget();
			break;
		case "Antagonist":
			GameObject[] Those = GameObject.FindGameObjectsWithTag ("Enemy");
			foreach (GameObject That in Those) {
				tTargetSpot = That.transform.position;
				tDistance = Vector3.Distance (this.transform.position, tTargetSpot);
				if (tDistance < vFarthestDistance) {
					tAngle = tTargetSpot - this.transform.position;
					rRay = new Ray (tOwner.transform.position, tAngle);
					if (!Physics.Raycast (rRay, tDistance, vRayLayers)) {
						Debug.DrawRay (tOwner.transform.position, tAngle, Color.green);
						myTargets.Add (That.gameObject);
					}
					else
						Debug.DrawRay (tOwner.transform.position, tAngle,Color.red);
				} else {
					tAngle = tTargetSpot - this.transform.position;
					rRay = new Ray (tOwner.transform.position, tAngle);
					Debug.DrawRay (tOwner.transform.position, tAngle,Color.gray);
				}
			}
			Those = GameObject.FindGameObjectsWithTag ("Targetable");
			foreach (GameObject That in Those) {
				tTargetSpot = That.transform.position;
				tDistance = Vector3.Distance (this.transform.position, tTargetSpot);
				if (tDistance < vFarthestDistance) {
					tAngle = tTargetSpot - this.transform.position;
					rRay = new Ray (tOwner.transform.position, tAngle);
					if (!Physics.Raycast (rRay, tDistance, vRayLayers)) {
						Debug.DrawRay (tOwner.transform.position, tAngle, Color.green);
						myTargets.Add (That.gameObject);
					}
					else
						Debug.DrawRay (tOwner.transform.position, tAngle,Color.red);
				} else {
					tAngle = tTargetSpot - this.transform.position;
					rRay = new Ray (tOwner.transform.position, tAngle);
					Debug.DrawRay (tOwner.transform.position, tAngle,Color.gray);
				}
			}
			break;
		}
	}
}
