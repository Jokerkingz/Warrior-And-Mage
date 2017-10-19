using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CheckPoint : MonoBehaviour {
	private Scr_Global cG;
	public GameObject vOwner;
	// Use this for initialization
	void Start () {
		cG = GameObject.FindGameObjectWithTag("GameController").GetComponent<Scr_Global>();
	}
	void OnTriggerEnter(Collider tOther){
		if (tOther.tag == "Warrior" || tOther.tag == "Mage"){
			cG.vFallCheckPoint = vOwner.gameObject;
		}
	}
}
