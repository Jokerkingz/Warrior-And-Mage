using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SkillBall : MonoBehaviour {
	public string vSkillType;
	public string vTargetType;
	public List<GameObject> vObjectsAffected;
	// Use this for initialization
	void Start () {
		Invoke("RemoveSelf",1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void RemoveSelf(){
		Destroy(this.gameObject);
	}
	void OnTriggerEnter(Collider tOther){
		switch (vSkillType){
			case "Push":
				if (tOther.tag == "Warrior"){
					if (!HaveYouBeenTagged(tOther.gameObject)){
						vObjectsAffected.Add(tOther.gameObject);
						string tDirection = PointToRefinedDirection(tOther.gameObject);
						tOther.GetComponent<Scr_ProtagonistAction>().vAnimationState = "StartActing";
						tOther.GetComponent<Scr_ProtagonistAction>().vInputType = "Push"+tDirection;
						}
					}
				if (tOther.tag == "Enemy"){
					if (!HaveYouBeenTagged(tOther.gameObject)){
						vObjectsAffected.Add(tOther.gameObject);
						string tDirection = PointToRefinedDirection(tOther.gameObject);
						tOther.GetComponent<Scr_AntagonistAction>().vAnimationState = "StartActing";
					tOther.GetComponent<Scr_AntagonistAction>().vInputType = "Push"+tDirection;
						}
					}
				/*if (Vector2.Distance(new Vector2(tMX,tMZ),new Vector2(tWX,tWZ))<1.1f){
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
		break;
		case "Bash":
			if (tOther.tag == "Breakable"){
				if (!HaveYouBeenTagged(tOther.gameObject)){
					vObjectsAffected.Add(tOther.gameObject);
					tOther.GetComponent<Scr_BreakablePiece>().Activate();
					}
				}
			/*
			if (tOther.tag == "Enemy"){
				if (HaveYouBeenTagged(tOther)){
					vObjectsAffected.Add(tOther.gameObject);
					}
			} else if (tOther.tag == "Breakable"){
				if (HaveYouBeenTagged(tOther)){
					vObjectsAffected.Add(tOther.gameObject);
					Destroy(tOther);
					}
			}*/
		break;


		}
	}

	bool HaveYouBeenTagged(GameObject tThat){
		bool tIHaveBeenTagged = false;
		foreach (GameObject tThis in vObjectsAffected){
			if (tThis.gameObject == tThat.gameObject){
				Debug.Log("You have been tagged");
				tIHaveBeenTagged = true;
				}
		}
		return tIHaveBeenTagged;
	}

	string PointToRefinedDirection(GameObject tOther){
		string tResult = "X";
		Vector3 tGoto;
		tGoto = tOther.transform.position;
		Vector3 tMyXZ = this.transform.position;
		float tDifferenceX = tMyXZ.x - tGoto.x;
		float tDifferenceY = tMyXZ.z - tGoto.z;
		float tAngle;
		tAngle = Mathf.Atan2 (tDifferenceX,tDifferenceY)*180/Mathf.PI;
		if (tAngle < -157.5f)
			tResult = "E";
		else if (tAngle < -112.5f)
			tResult = "SE";
		else if (tAngle < -67.5)
			tResult = "S";
		else if (tAngle < -22.5)
			tResult = "SW";
		else if (tAngle < 22.5)
			tResult = "W";
		else if (tAngle < 67.5)
			tResult = "NW";
		else if (tAngle < 112.5)
			tResult = "N";
		else if (tAngle < 157.5)
			tResult = "NE";
		else 
			tResult = "E";
		return tResult;
	}
}
