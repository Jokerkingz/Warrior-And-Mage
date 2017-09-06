using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SFX_Damage_Blinker : MonoBehaviour {
	public AnimationCurve vBlinker;
	public float vBlinkFrame;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (vBlinkFrame > 0f) {
			vBlinkFrame += .05f;
			if (vBlinker.Evaluate (vBlinkFrame) < 0f)
				this.GetComponent<MeshRenderer> ().enabled = false;
			else
				this.GetComponent<MeshRenderer> ().enabled = true;
				
			if (vBlinkFrame > 3f) {
				if (this.tag == "Enemy")
					Destroy (this.gameObject);
				vBlinkFrame = 0;
			}
		}
	}
}
