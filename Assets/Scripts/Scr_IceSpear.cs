using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_IceSpear : MonoBehaviour {
	public Vector3 vStartPoint;
	public Vector3 vTargetSpot;
	public float tTemp;
	// Use this for initialization
	void Start () {
		vStartPoint = transform.position;
		Invoke("Die",10f);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.LookAt(vTargetSpot);
		transform.Translate(Vector3.forward*4f*Time.deltaTime,Space.Self);
		tTemp = Vector3.Distance(transform.position,vTargetSpot);
		if (tTemp<.5f)
			TurnOff();
	}
	void TurnOff(){
		GetComponent<ParticleSystem>().Stop();
		GetComponentInChildren<MeshRenderer>().enabled = false;
		tag = "Untagged";
	}
	void Die(){
		Destroy(this.gameObject);
	}
}
