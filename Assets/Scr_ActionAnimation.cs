using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ActionAnimation : MonoBehaviour {
	public Vector3 vPrevVect3;
	public Vector3 vNextVect3;
	public string vAnimationState;
	public float vAnimationFrame;
	public string vInputType;
	public AnimationCurve vBounce;
	public Scr_Global vGlobal; 
	// Use this for initialization
	void Start () {
		vAnimationState = "Idle";
		vPrevVect3 = this.transform.position;
		vNextVect3 = this.transform.position;
		vAnimationFrame = 0;
		vGlobal = GameObject.FindGameObjectWithTag ("GameController").GetComponent<Scr_Global> ();
	}
	
	// Update is called once per frame
	void Update () {
		switch (vAnimationState) {
		case "Idle":


			break;
		case "StartMoving":
			vPrevVect3 = transform.position;
			switch (vInputType) {
			case "MoveUp":
				vNextVect3 = transform.position + new Vector3 (1f, 0f, 0f);
				break;
			case "MoveDown":
				vNextVect3 = transform.position + new Vector3 (-1f, 0f, 0f);
				break;
			case "MoveLeft":
				vNextVect3 = transform.position + new Vector3 (0f, 0f, -1f);
				break;
			case "MoveRight":
				vNextVect3 = transform.position + new Vector3 (0f, 0f, 1f);
				break;
			case "PushUp":
				vNextVect3 = transform.position + new Vector3 (3f, 0f, 0f);
				break;
			case "PushDown":
				vNextVect3 = transform.position + new Vector3 (-3f, 0f, 0f);
				break;
			case "PushLeft":
				vNextVect3 = transform.position + new Vector3 (0f, 0f, -3f);
				break;
			case "PushRight":
				vNextVect3 = transform.position + new Vector3 (0f, 0f, 3f);
				break;
			case "Wait":
				vNextVect3 = transform.position;
				break;
			}
			// Snap
			vPrevVect3 = new Vector3(Mathf.Round(vPrevVect3.x),1f,Mathf.Round(vPrevVect3.z));
			vNextVect3 = new Vector3(Mathf.Round(vNextVect3.x),1f,Mathf.Round(vNextVect3.z));
			vAnimationState = "Move";
			vAnimationFrame = 0f;
			break;

		case "Move":
			//vAnimationFrame += .05f // is correct
			vAnimationFrame = vGlobal.Global_AnimationFrame;
			if (vGlobal.Global_AnimationState == "EndAnimate") {
				vAnimationFrame = 0f;
				vAnimationState = "Idle";
				vInputType = "Wait";
				transform.position = Vector3.Lerp (vPrevVect3, vNextVect3, 1f);
				// Snap
				transform.position = new Vector3(Mathf.Round(transform.position.x),1f,Mathf.Round(transform.position.z));
			} else {
				transform.position = Vector3.Lerp (vPrevVect3, vNextVect3, vAnimationFrame);
				transform.position = new Vector3(transform.position.x,vBounce.Evaluate (vAnimationFrame)+1f,transform.position.z);
			}
			break;
		case "MoveCorrect":
			vAnimationFrame += .05f; // is correct
			if (vAnimationFrame > 1f) {
				vAnimationFrame = 0f;
				vAnimationState = "Idle";
				vInputType = "Wait";
				transform.position = Vector3.Lerp (vPrevVect3, vNextVect3, 1f);
				transform.position = new Vector3(Mathf.Round(transform.position.x),1f,Mathf.Round(transform.position.z));
			} else {
				transform.position = Vector3.Lerp (vPrevVect3, vNextVect3, vAnimationFrame);
				transform.position = new Vector3(transform.position.x,vBounce.Evaluate (vAnimationFrame)+1f,transform.position.z);
			}
			break;
		case "MoveBack":
			vAnimationFrame -=  2f*Time.unscaledDeltaTime;// .05f is correct
			if (vAnimationFrame < 0f) {
				vAnimationFrame = 0f;
				vAnimationState = "Idle";
				vInputType = "Wait";
				transform.position = Vector3.Lerp (vPrevVect3, vNextVect3, 0f);
				transform.position = new Vector3(Mathf.Round(transform.position.x),1f,Mathf.Round(transform.position.z));
			} else {
				transform.position = Vector3.Lerp (vPrevVect3, vNextVect3, vAnimationFrame);
				transform.position = new Vector3(transform.position.x,vBounce.Evaluate (vAnimationFrame)+1f,transform.position.z);
			}
			break;


		}
	}
	void OnTriggerStay(Collider Other){
		if (Other.tag == "Wall" || Other.tag == "Breakable") {
			if (vAnimationState == "Move") {
				vAnimationState = "MoveBack";
				vPrevVect3 = new Vector3 (Mathf.Round (transform.position.x), 1f, Mathf.Round (transform.position.z));

			}
		}
		else if (Other.tag == "Warrior" || Other.tag == "Mage" || Other.tag == "Enemy" ){
			if (vAnimationState == "Move") {
				vAnimationState = "MoveBack";
				}
			}
	}
}
