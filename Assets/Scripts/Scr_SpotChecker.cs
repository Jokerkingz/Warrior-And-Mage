using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SpotChecker : MonoBehaviour {
	public GameObject vSpot;
	public GameObject vNorth;
	public GameObject vEast;
	public GameObject vSouth;
	public GameObject vWest;
	public string OnTriggerStay(Collider tOther){
		return tOther.tag;
	}
	void OnTriggerExit(){
		//vOnMe = "Nothing";
	}
}
