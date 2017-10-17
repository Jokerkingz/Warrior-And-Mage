using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CreateEffect : MonoBehaviour {
	public GameObject vSpark;

	void Update(){
		//if (Input.GetKeyDown("Space"))
		//	Activate(Vector3.zero);
	}

	public void Activate(Vector3 tTargetSpot){
		GameObject Temp = Instantiate(vSpark) as GameObject;
		Temp.transform.position = tTargetSpot;
	}
}
