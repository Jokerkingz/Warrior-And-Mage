using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TargetingSystem : MonoBehaviour {
	public int vTargetIndex;
	public GameObject vTargetedObject; // The Target
	public GameObject vPreFab;
	public GameObject vTarget; // The object used to target
	public GameObject vOwner;
	public string vTargetStatus;
	public List<GameObject> myTargets;
	public LayerMask vRayLayers;

	public GameObject vCurrentTarget;


	// Use this for initialization
	void Start () {
		vTarget = Instantiate (vPreFab)as GameObject;
		//Dictionary<GameObject,Vector3> dTargets = new Dictionary<GameObject, Vector3> ();
	}
	
	// Update is called once per frame
	void Update () {
		vCurrentTarget = NearestTarget ();
		FindATarget ("Antagonist");
		//Ray rRay; Proper Raycasting
		//Vector3 tAngle = vTarget.transform.position - this.transform.position;
		//	rRay = new Ray(this.transform.position, tAngle);
		//Debug.DrawRay (this.transform.position, tAngle);
		vTargetStatus = "poop";
	}
	GameObject NearestTarget(){
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
		//Debug.
		switch (tTargetType) {
		case "Protagonist":


			break;
		case "Antagonist":
			GameObject[] Those = GameObject.FindGameObjectsWithTag ("Enemy");
			foreach (GameObject That in Those) {
				tTargetSpot = That.transform.position;
				tDistance = Vector3.Distance (this.transform.position, tTargetSpot);
				if (tDistance < 10f) {
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
