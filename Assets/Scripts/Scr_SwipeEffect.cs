using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SwipeEffect : MonoBehaviour {
	public float vFacingDirection;
	private Vector3 vAngle;
	private bool vDone;
	public GameObject vStick;
	private float vSwipingAngle;

	// Use this for initialization
	void Start () {
		vAngle.x = 45f;
		vAngle.y = vFacingDirection-90;
		vAngle.z = vSwipingAngle;
		transform.localEulerAngles = vAngle;
		Invoke("Die",2f);
	}
	
	// Update is called once per frame
	void Update () {
		vSwipingAngle += Time.deltaTime*180f*4f;
		if (vSwipingAngle > 180f){
			vSwipingAngle = 180f;
			if (!vDone){
				vDone = true;
				Destroy(vStick);
			}
			}
		vAngle.x = 45f;
		vAngle.y = vFacingDirection-90f;
		vAngle.z = vSwipingAngle;
		transform.localEulerAngles = vAngle;
	}
	void Die(){
		Destroy(this.gameObject);
	}
}
