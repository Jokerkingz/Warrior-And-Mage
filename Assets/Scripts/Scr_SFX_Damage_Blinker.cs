using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SFX_Damage_Blinker : MonoBehaviour {
	public AnimationCurve vBlinker;
	public GameObject vModel;
	public float vBlinkFrame;

	public string owner;
	public bool vDie;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (vBlinkFrame > 0f) {
			vBlinkFrame += .05f;
			if (vBlinker.Evaluate (vBlinkFrame) < 0f)
				vModel.SetActive (false);
			else
				vModel.SetActive (true);
				
			if (vBlinkFrame > 3f) {
				//if (this.tag == "Enemy")
				//	Destroy (this.gameObject);
				vBlinkFrame = 0;

				if (owner == "slime") {
					if (vDie)
						Destroy (this.gameObject);
					//ADD XP HERE
					
				}
			}
		}
	}
}


