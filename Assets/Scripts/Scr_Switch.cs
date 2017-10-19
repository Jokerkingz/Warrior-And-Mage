using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Switch : MonoBehaviour {
	public string vCurrentState; 
	public string vNextState; 
	public string vEvent;
	public GameObject[] vFloors;
	public int vColor;
	public GameObject vSwitchObj;
	public Material[] vSwitchOn;
	public Material[] vSwitchOff;
	// Use this for initialization
	void Start () {
		vCurrentState = "Idle";
		vNextState = "Idle";
		vEvent = "Idle";
		vColor = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)){
			Debug.Log("Before Process");
			if (vColor == 0){
				vColor = 1;
				vSwitchObj.GetComponent<Renderer>().materials = vSwitchOn;
				}
			else{
				vColor = 0;
				vSwitchObj.GetComponent<Renderer>().materials = vSwitchOff;
				}
			Debug.Log("Pressed");
	}

		if (vEvent == "StartActing" && vNextState == "SwitchAct") {
			ActivateFloors();
			if (vColor == 0){
				vColor = 1;
				vSwitchObj.GetComponent<Renderer>().materials = vSwitchOn;
				}
			else{
				vColor = 0;
				vSwitchObj.GetComponent<Renderer>().materials = vSwitchOff;
				}
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
