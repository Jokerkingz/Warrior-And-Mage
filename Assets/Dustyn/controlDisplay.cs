using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlDisplay : MonoBehaviour {


	public float alpha;
	public float changeRate;
	private CanvasGroup cg;

	public bool active;
	public bool canPress;

	public KeyCode testButton;

	public float startPos;
	public float endPos;

	//public string UIstatus;
	//public float slideFrame;
	//public float gotoX;

	void Start () {
		cg = this.gameObject.GetComponent<CanvasGroup> ();
		active = true;
		canPress = true;
		transform.localPosition = new Vector3 (startPos, -11, 0);
		//UIstatus = "controls";
	
	}

	void Update()
	{

		/*switch (UIstatus) {
		case "controls":
			slideFrame += Time.deltaTime;
			Mathf.Clamp (slideFrame, 0f, 1f);


			break;
		}
		gotoX = Mathf.Lerp (0f, 100f, slideFrame);
		gameObject.transform.localPosition = new Vector3 (gotoX, 0f, 0f);
*/
		if (Input.GetKeyDown (testButton) &&active==false &&canPress==true) {
			active = true;
			canPress = false;
		}

		if (Input.GetKeyDown (testButton) &&active==true &&canPress==true) {
			active = false;
			canPress = false;
		}


		if (active == true) {
			cg.alpha = alpha += changeRate * Time.deltaTime;
			//transform.localPosition = new Vector3.Lerp (startPos, -11f, 0f);
		}
		if (active == false) {
			cg.alpha = alpha -= changeRate * Time.deltaTime;
			//transform.localPosition = new Vector3.Lerp (endPos, -11f, 0f);
		}


		if (alpha >= 1f) 
		{
			canPress = true;
			alpha = 1f;
		}
		if (alpha <=0f)
		{
			canPress = true;
			alpha=0f;
		}

	}

}
