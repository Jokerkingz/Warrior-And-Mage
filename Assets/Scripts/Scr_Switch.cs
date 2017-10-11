using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Switch : MonoBehaviour {
	public string vCurrentState; 
	public string vNextState; 
	public string vEvent;
	public GameObject[] vFloors;
	// Use this for initialization
	void Start () {
		vCurrentState = "Idle";
		vNextState = "Idle";
		vEvent = "Idle";
	}
	
	// Update is called once per frame
	void Update () {
		if (vEvent == "StartActing" && vNextState == "SwitchAct") {
			ActivateFloors();
			vNextState = "Idle";
			}
	}
	public void GetHit(){
		vNextState = "SwitchAct";
		Debug.Log("I Got Hit");
	}

	void ActivateFloors(){
		foreach (GameObject tThis in vFloors){
			tThis.GetComponent<Scr_SwitchFloor>().Activate();
		}
	}
}
