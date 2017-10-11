using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SwitchFloor : MonoBehaviour {
	public Vector3 vOrigin;
	public Vector3 vGoTo;
	public bool vIsUp;
	public GameObject vModel;
	public GameObject vPit;
	// Use this for initialization
	[ContextMenu("Initialize")]
	void EditOnly () {
		vOrigin = vModel.transform.position;
		vGoTo = vOrigin;
		vGoTo.y -= 20;
		vIsUp = true;
	}
	
	// Update is called once per frame
	void Update () {
			
	}
	[ContextMenu("Switch This")]
	public void Activate(){
		//Debug.Log("Switched");
			if (!vIsUp){
				vModel.transform.position = vOrigin;
				vPit.SetActive(false);
				vIsUp = true;
				}
			else{
				vModel.transform.position = vGoTo;
				vPit.SetActive(true);
				vIsUp = false;
			}
	}
}
