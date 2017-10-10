using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BreakPiece : MonoBehaviour {
	public string vState;
	private Vector3 vOrginalSpot;

	// Use this for initialization
	void Start () {
		vState = "Solid";
		vOrginalSpot = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//vState 
	}
	public void Break(Vector3 tSpot){
		Rigidbody[] tRB = this.GetComponentsInChildren<Rigidbody>();
		foreach (Rigidbody tThis in tRB){
			tThis.useGravity = true;
			tThis.isKinematic = false;
			tThis.AddExplosionForce(200f,tSpot,20f);

			}
	}
	public void Assemble(){
		transform.position = vOrginalSpot;
		Rigidbody[] tRB = this.GetComponentsInChildren<Rigidbody>();
		foreach (Rigidbody tThis in tRB){
			tThis.useGravity = false;
			tThis.isKinematic = true;
			tThis.velocity = Vector3.zero;
			//tThis.AddExplosionForce(200f,tSpot,50f);

			}
	}
}
