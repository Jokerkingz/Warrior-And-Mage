using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvastest : MonoBehaviour {

	public Transform target;

	void Start () {
		
	}

	void Update () {
		transform.LookAt (target);
		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, 90f, transform.eulerAngles.z);
	}
}
