using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BreakablePiece : MonoBehaviour {
	public bool vActive = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Activate(){
		if (!vActive){
			vActive = true;
			Invoke("Die",1f);
			Vector3 tSpot = GameObject.FindGameObjectWithTag("Warrior").transform.position;
			Rigidbody[] tRB = this.GetComponentsInChildren<Rigidbody>();
			foreach (Rigidbody tThis in tRB){
				tThis.useGravity = true;
				tThis.isKinematic = false;
				tThis.AddExplosionForce(200f,tSpot,50f);
			}

		}
	}

	void Die(){
		Destroy(this.gameObject);
	}
}
