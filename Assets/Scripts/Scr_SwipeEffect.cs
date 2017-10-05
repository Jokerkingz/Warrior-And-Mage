using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SwipeEffect : MonoBehaviour {
	public float vFacingDirection;
	private Vector3 vAngle;
	private float vSwipingAngle;

	// Use this for initialization
	void Start () {
		vAngle.x = 45f;
		vAngle.y = vFacingDirection-90;
		vAngle.z = vSwipingAngle;
		transform.localEulerAngles = vAngle;
	}
	
	// Update is called once per frame
	void Update () {
		vSwipingAngle += Time.deltaTime*180f*4f;
		if (vSwipingAngle > 180f)
			vSwipingAngle = 180f;
		vAngle.x = 45f;
		vAngle.y = vFacingDirection-90;
		vAngle.z = vSwipingAngle;
		transform.localEulerAngles = vAngle;
	}
}
