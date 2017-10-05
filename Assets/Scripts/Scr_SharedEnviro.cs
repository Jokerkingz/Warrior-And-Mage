using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SharedEnviro : MonoBehaviour {
	public string vEvent;
	public bool vHasExtra;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!vHasExtra)
			vEvent = "Idle";
	}
}
