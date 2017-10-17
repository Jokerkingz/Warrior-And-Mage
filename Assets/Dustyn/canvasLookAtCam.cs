using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasLookAtCam : MonoBehaviour {

	//In World UI faces camera

	public Transform target;

	void Start () {
		//target= GameObject.FindObjectOfType<Scr_CameraLockOn> ();
	}

	void Update () {
		transform.LookAt (target);
		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, 90f, transform.eulerAngles.z);
	}
}
