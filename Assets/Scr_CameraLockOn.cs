using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CameraLockOn : MonoBehaviour {
	public string vCameraState;
	public GameObject vCamera;
	public GameObject vObj_Warrior;
	public GameObject vObj_Mage;
	public Vector3 vVectDestination;
	public float vXAccel;
	public float vZAccel;
	public float vDeltaTimeAdjustor;

	// Use this for initialization
	void Start () {
		vCameraState = "Follow";
		vObj_Warrior = GameObject.FindGameObjectWithTag ("Warrior");
		vObj_Mage = GameObject.FindGameObjectWithTag ("Mage");
		vCamera = GameObject.FindGameObjectWithTag ("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		switch (vCameraState) {
		case "Follow":
			Vector3 tWarPoint = Vector3.Scale (vObj_Warrior.transform.position, new Vector3 (1f, 0f, 1f));
			Vector3 tMagPoint = Vector3.Scale (vObj_Mage.transform.position, new Vector3 (1f, 0f, 1f));
			float tDistance = Vector3.Distance (tWarPoint, tMagPoint);
			Vector3 tVectorDifference = ((tWarPoint - tMagPoint) / 2f);
			vVectDestination = new Vector3 (tWarPoint.x - tVectorDifference.x, 1f, tWarPoint.z - tVectorDifference.z);
			// XXXXXXXXXXXXXXXXXXXXXXXXX
			if (vVectDestination.x > this.transform.position.x + 1)
				vXAccel += 1f*vDeltaTimeAdjustor*Time.deltaTime;
			if (vVectDestination.x < this.transform.position.x - 1)
				vXAccel -= 1f*vDeltaTimeAdjustor*Time.deltaTime;
			if (vXAccel > .001f || vXAccel < -.001f)
				vXAccel = (vXAccel * .8f);
			else
				vXAccel = 0;
			// YYYYYYYYYYYYYYYYYYYYYYYYYYYY
			if (vVectDestination.z > this.transform.position.z + 1)
				vZAccel += 1f*vDeltaTimeAdjustor*Time.deltaTime;
			if (vVectDestination.z < this.transform.position.z - 1)
				vZAccel -= 1f*vDeltaTimeAdjustor*Time.deltaTime;
			if (vZAccel > .001f || vZAccel < -.001f)
				vZAccel = (vZAccel * .8f);
			else
				vZAccel = 0;
			transform.position = transform.position + new Vector3 (vXAccel/100f,0,vZAccel/100f);
			//transform.position = vVectDestination;
			vCamera.transform.localPosition = new Vector3 (tDistance*2f,0,0);
			if (tDistance < 4f)
				vCamera.transform.localPosition = new Vector3 (4f*2f,0,0);
			break;

		}
	}
}
