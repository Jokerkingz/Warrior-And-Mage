using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using UnityEngine.UI;

public class controlSlide : MonoBehaviour {

	public RectTransform current;
	public float speed=1f;
	public float direction =1f; 

	private Vector2 currentMaxOffset;
	private Vector2 currentMinOffset;

	private bool animate = false;
	private float counter = 0f;

	public bool canPush;
	public float pushTimer;
	public float pushTime;

	void Start () {
		canPush = true;
	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space)&&canPush==true) {
			ExecuteButton ();
		}
		if (this.animate) {
			canPush = false;
			float newYValue = Mathf.Lerp (0f, Screen.height, this.counter) *this.direction;
			this.current.offsetMax = new Vector2 (this.currentMaxOffset.x + newYValue, this.currentMaxOffset.y);
			this.current.offsetMin = new Vector2 (this.currentMinOffset.x + newYValue, this.currentMinOffset.y);

			if (this.counter > 1f) {
				this.FinalizeTransition (); 
			} else {
				this.counter += Time.unscaledDeltaTime * this.speed;
			}
		}
		if (canPush == false) {
			pushTimer += Time.deltaTime;
		}
		if (pushTimer >= pushTime) {
			canPush = true;
			pushTimer = 0;
		}
		
	}
	void FinalizeTransition()
	{
		this.animate = false;
		this.counter = 0f; 

		direction = -direction;
	
	}
	public void ExecuteButton()
	{
		//Debug.Log ("Button Executed");

		this.currentMaxOffset = this.current.offsetMax;
		this.currentMinOffset = this.current.offsetMin;

		this.animate = true; 

	}
}
