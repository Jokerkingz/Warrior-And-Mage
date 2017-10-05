using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_MovingWall : MonoBehaviour {
	public Vector3 vGoto;
	public Vector3 vOrigin;
	public GameObject vWall;
	public float vIsOn;
	public string vEvent;
	public float vLerp;
	public Vector3 vWallGoTo;
	public Vector3 vWallOrigin;
	public LayerMask vPlayerLayer;

	public bool vIHaveToAct;
	// Use this for initialization
	void Start () {
		vOrigin = this.transform.position;
		vGoto = vOrigin + vGoto;
		vWallGoTo = vGoto;
		vWallGoTo.y = +.5f;
		vWallOrigin = vOrigin;
		vWallOrigin.y = +.5f;
		vIHaveToAct = false;
	}
	
	// Update is called once per frame
	void Update () {
		if ((vIsOn > 0f && vLerp != 1f) || (vIsOn == 0f && vLerp != 0f) )
			vIHaveToAct = true;
		else {
			vIHaveToAct = false;
		}

		if (vEvent ==  "StartActing" && vIHaveToAct)
			vEvent = "Act";
		if (vEvent ==  "StartActing" && !vIHaveToAct) 
			vEvent =  "Idle";

		if (vEvent == "Act"){
			if (vIsOn>0f){
				vLerp += 2f*Time.deltaTime;
				this.transform.position = vGoto;
			}
			else{
				Vector3 tFrom = vOrigin;
				tFrom.y += 3f;
				Ray tRay;
				tRay = new Ray (tFrom,  new Vector3(0f,-5f,0f));
				if (!Physics.Raycast (tRay, 5f, vPlayerLayer)){
					vLerp -= 2f*Time.deltaTime;
					this.transform.position = vOrigin;
					}
				}
		}
		vIsOn-=.25f;
		vIsOn = Mathf.Clamp(vIsOn,0f,1f);
		vLerp = Mathf.Clamp(vLerp,0f,1f);
		if (vLerp == 0f || vLerp == 1f)
			vEvent = "Idle";
		vWall.transform.position = Vector3.Lerp(vWallOrigin,vWallGoTo,vLerp);
	}
}
