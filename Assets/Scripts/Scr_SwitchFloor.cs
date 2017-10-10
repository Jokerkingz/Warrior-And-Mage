using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SwitchFloor : MonoBehaviour {
	public Vector3 vOrigin;
	public Vector3 vGoTo;
	public bool vIsUp;
	// Use this for initialization
	void Start () {
		vOrigin = transform.position;
		vGoTo = vOrigin;
		vGoTo.y -= 10;
	}
	
	// Update is called once per frame
	void Update () {
		if (vIsUp){
			transform.position = vOrigin;
			}
		else{
			transform.position = vGoTo;
		}
			
	}
}
