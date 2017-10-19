using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_IceSpear : MonoBehaviour {
	public Vector3 vStartPoint;
	public GameObject vTarget;
	public Vector3 vTargetSpot;
	public float tTemp;
	public float vCarriedValue;
	public bool vDone;
	// Use this for initialization
	void Start () {
		vStartPoint = transform.position;
		Invoke("Die",10f);
		vTargetSpot = vTarget.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.LookAt(vTargetSpot);
		transform.Translate(Vector3.forward*4f*Time.deltaTime,Space.Self);
		tTemp = Vector3.Distance(transform.position,vTargetSpot);
		if (tTemp<.5f && !vDone){
			if (vTarget!= null)
				TurnOff();
			else {
				GetComponentInChildren<MeshRenderer>().enabled = false;
				GetComponentInChildren<MeshRenderer>().gameObject.tag ="Untagged";
				Invoke("Die",2f);
				tag = "Untagged";
			}
			}
	}
	void TurnOff(){
		vDone = true;
		if (vTarget.tag == "Enemy") {
			vTarget.GetComponent<Scr_SFX_Damage_Blinker> ().vBlinkFrame += .01f;
			vTarget.GetComponent<Scr_SFX_Damage_Blinker> ().vBlinkFrame += .01f;
			float Damage = 3f;//GetComponent<attackStat> ().DamageCalculation();
			//float Damage = this.GetComponent<attackStat> ().DamageCalculation();
			vTarget.GetComponent<defenseStat> ().DamageEquation (Damage);
		}
		else if (vTarget.tag == "Targetable")
			vTarget.GetComponent<Scr_Switch> ().GetHit();
		GetComponentInChildren<MeshRenderer>().enabled = false;
		GetComponentInChildren<MeshRenderer>().gameObject.tag ="Untagged";
		Invoke("Die",2f);
		tag = "Untagged";
	}
	void Die(){
		Destroy(this.gameObject);
	}
}
