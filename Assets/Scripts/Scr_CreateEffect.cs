using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CreateEffect : MonoBehaviour {
	public GameObject[] vSpecialE;

	public void Activate(Vector3 tTargetSpot, int tWhich){
		GameObject Temp = Instantiate(vSpecialE[tWhich]) as GameObject;
		Temp.transform.position = tTargetSpot;
	}
}
