using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class statBarFade : MonoBehaviour {

	private float alpha;
	private CanvasGroup cg;

	//YES, CHRIST, THESE ARE TIMERS, NOT A COROUTINE HAHAHAHA TRY TO STOP ME
	private bool active;
	private float atimer = 0f;
	private float atime = 1f;

	void Start () {
		cg = this.gameObject.GetComponent<CanvasGroup> ();
		active = false;
	}

	void Update () {

		if (active == false) {
			cg.alpha = alpha -= 0.5f * Time.deltaTime;
		}

		if (alpha <=0f)
		{
			active = false;
			alpha=0f;
		}

		if (active == true) {
			atimer += Time.deltaTime;
		}
		if (atimer >= atime) {
			active = false;
			atimer = 0f;
		}

	}

	public void Appear()
	{
		cg.alpha = alpha = 1f;
		active = true;

	}
}
