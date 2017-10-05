using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SFX_Damage_Blinker : MonoBehaviour {
	public AnimationCurve vBlinker;
	public GameObject vModel;
	public float vBlinkFrame;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (vBlinkFrame > 0f) {
			vBlinkFrame += .05f;
			if (vBlinker.Evaluate (vBlinkFrame) < 0f)
				vModel.GetComponent<MeshRenderer> ().enabled = false;
			else
				vModel.GetComponent<MeshRenderer> ().enabled = true;
				
			if (vBlinkFrame > 3f) {
				//if (this.tag == "Enemy")
				//	Destroy (this.gameObject);
				vBlinkFrame = 0;
			}
		}
	}
}
