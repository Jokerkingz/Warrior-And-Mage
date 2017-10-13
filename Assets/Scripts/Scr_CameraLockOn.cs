using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CameraLockOn : MonoBehaviour {
	public string vCameraState;
	public GameObject vCamera;
	public GameObject vObj_Warrior;
	public GameObject vObj_Mage;
	public Vector3 vVectDestination;
	private float vXAccel;
	private float vZAccel;
	private float vYAccel = 0f; // Camera distance speed
	public float vCamDistance = 0f; // Camera distance
	public float vDeltaTimeAdjustor;

	public float vShowDistance;
	// Use this for initialization
	void Start () {
		vCameraState = "Follow";
		vObj_Warrior = GameObject.FindGameObjectWithTag ("Warrior");
		vObj_Mage = GameObject.FindGameObjectWithTag ("Mage");
		vCamera = GameObject.FindGameObjectWithTag ("MainCamera");

		Vector3 tWarPoint = Vector3.Scale (vObj_Warrior.transform.position, new Vector3 (1f, 0f, 1f));
		Vector3 tMagPoint = Vector3.Scale (vObj_Mage.transform.position, new Vector3 (1f, 0f, 1f));
		float tDistance = Vector3.Distance (tWarPoint, tMagPoint);
		Vector3 tVectorDifference = ((tWarPoint - tMagPoint) / 2f);
		vVectDestination = new Vector3 (tWarPoint.x - tVectorDifference.x, 1f, tWarPoint.z - tVectorDifference.z);
		transform.position = vVectDestination;
	}
	
	// Update is called once per frame
	void Update () {
		switch (vCameraState) {
		case "Follow":
			Vector3 tWarPoint = Vector3.Scale (vObj_Warrior.transform.position, new Vector3 (1f, 0f, 1f));
			Vector3 tMagPoint = Vector3.Scale (vObj_Mage.transform.position, new Vector3 (1f, 0f, 1f));
			float tDistance = Vector3.Distance (tWarPoint, tMagPoint);
			Vector3 tVectorDifference = ((tWarPoint - tMagPoint) / 2f);
			vVectDestination = new Vector3 (tWarPoint.x - tVectorDifference.x + 1.5f, 1f, tWarPoint.z - tVectorDifference.z);

			// XXXXXXXXXXXXXXXXXXXXXXXXX
			if (vVectDestination.x > this.transform.position.x + 1)
				vXAccel += (0.5f * (vVectDestination.x - this.transform.position.x)) * vDeltaTimeAdjustor * Time.deltaTime;
			if (vVectDestination.x < this.transform.position.x - 1)
				vXAccel -= (-.5f * (vVectDestination.x - this.transform.position.x)) * vDeltaTimeAdjustor * Time.deltaTime;
			if (vXAccel > .001f || vXAccel < -.001f)
				vXAccel = (vXAccel * .8f);
			else
				vXAccel = 0;
			
			// YYYYYYYYYYYYYYYYYYYYYYYYYYYY
			if (vVectDestination.z > this.transform.position.z + 1)
				vZAccel += (0.5f * (vVectDestination.z - this.transform.position.z)) * vDeltaTimeAdjustor * Time.deltaTime;
			if (vVectDestination.z < this.transform.position.z - 1)
				vZAccel -= (-.5f * (vVectDestination.z - this.transform.position.z)) * vDeltaTimeAdjustor * Time.deltaTime;
			if (vZAccel > .001f || vZAccel < -.001f)
				vZAccel = (vZAccel * .8f);
			else
				vZAccel = 0;
			transform.position = transform.position + new Vector3 (vXAccel / 100f, 0, vZAccel / 100f);


			// XXX Cam Zoom
			if (tDistance > vCamDistance + 1f)
				vYAccel += .1f;
			if (tDistance < vCamDistance - 1f)
				vYAccel -= .1f;
			if (vYAccel > .001f || vYAccel < -.001f)
				vYAccel = (vYAccel * .8f);
			else
				vYAccel = 0;
			vShowDistance = tDistance;
			vCamDistance += vYAccel;
			vCamDistance = Mathf.Clamp (vCamDistance, 4f, 10f);
			vCamera.transform.localPosition = new Vector3 (vCamDistance*1.75f,0,0);
			break;

		}
	}

	//DUSTYN Script starts
	public void MageDead()
	{
		vObj_Warrior = GameObject.FindGameObjectWithTag ("Mage");

	}
	public void WarriorDead()
	{
		vObj_Mage = GameObject.FindGameObjectWithTag ("Warrior");
	}
	//DUSTYN script ends

}
