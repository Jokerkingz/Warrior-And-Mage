using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_AnimationControl : MonoBehaviour {
	public string vModelType;
	public GameObject vModel;
	public GameObject vAnchor;
	private Animator cAC;

	public Vector3 tResult;

	// Use this for initialization
	void Start () {
		cAC = vModel.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	public void Act (string tAction, Vector3 tLookAt) {
		float tAngle;
		switch (tAction){
			case "Idle":
			cAC.SetInteger("WhichAnimation",0);
			tAngle = Mathf.Atan2 (tLookAt.x,tLookAt.z)*180/Mathf.PI;
			vAnchor.transform.eulerAngles = new Vector3(0,tAngle,0);
			//tResult =tLookAt;
			break;
		case "Swing":
			cAC.SetInteger("WhichAnimation",1);
			tAngle = Mathf.Atan2 (tLookAt.x,tLookAt.z)*180/Mathf.PI;
			vAnchor.transform.eulerAngles = new Vector3(0,tAngle,0);
			break;
		case "Spell1":
			cAC.SetInteger("WhichAnimation",2);
			tAngle = Mathf.Atan2 (tLookAt.x,tLookAt.z)*180/Mathf.PI;
			vAnchor.transform.eulerAngles = new Vector3(0,tAngle,0);
			break;
		case "Spell2":
			cAC.SetInteger("WhichAnimation",3);
			break;
		case "Spell3":
			cAC.SetInteger("WhichAnimation",4);
			break;
		case "Spell4":
			cAC.SetInteger("WhichAnimation",5);
			break;
		case "Spell5":
			cAC.SetInteger("WhichAnimation",6);
			break;
		case "Fall":
			cAC.SetInteger("WhichAnimation",7);
			break;
		case "Skill1":
			cAC.SetInteger("WhichAnimation",2);
		break;
		}
	}
	/*
		switch (vModelType){
			case "Mage":
				MageAction(tAction,tLookAt);
			break;
		}
	}
	void MageAction(string tAction, Vector3 tLookAt){
	}
	*/
}
