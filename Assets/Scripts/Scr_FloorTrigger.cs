using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_FloorTrigger : MonoBehaviour {
	public bool vHasTriggered;
	public GameObject vParent;
	public float vPressure;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		vPressure -= 0.25f;
		vPressure = Mathf.Clamp(vPressure,0f,1f);
	}

	void OnTriggerStay(){
		vPressure +=1f;
		vParent.GetComponent<Scr_FloorBreakable>().Activate();
	}
}
