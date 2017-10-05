using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlateOngoing : MonoBehaviour {
	public GameObject[] vWallToMove;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerStay(Collider tOther){
		if (tOther.tag == "Warrior" || tOther.tag == "Mage"){
		foreach (GameObject tThis in vWallToMove){
				tThis.GetComponent<Scr_MovingWall>().vIsOn += 1f;
			}
		}

	}
}
