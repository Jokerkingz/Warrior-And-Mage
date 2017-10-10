using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Switch : MonoBehaviour {
	public string vState; 
	public GameObject[] vFloors;
	// Use this for initialization
	void Start () {
		vState = "Off";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void GetHit(){
		Debug.Log("I Got Hit");
		foreach (GameObject tThis in vFloors){


		}

	}
}
