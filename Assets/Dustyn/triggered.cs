using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggered : MonoBehaviour {

	private Animator anim;
	public bool active;

	void Start()
	{
		anim = this.gameObject.GetComponent<Animator> ();
		active = false;
	}

	void Update()
	{
		if (active == true) {
			anim.SetBool ("appear", true);
		}
		if (active == false) {
			anim.SetBool ("appear", false);
		}
	}
	public void Appear()
	{
		active = true;
	
	}
	public void Disappear()
	{
		active = false;
	}
}
