using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_FloorBreakable : MonoBehaviour {
	public bool vActive = false;
	public Scr_BreakPiece[] vPieces;
	private string vStatus;
	private string vNextStatus;
	private Scr_SharedEnviro cSE;
	public GameObject vPit;
	public int vTimer;
	public bool vIHaveToAct = false;
	// Use this for initialization
	void Start () {
		cSE = GetComponent<Scr_SharedEnviro>();
		vPieces = gameObject.GetComponentsInChildren<Scr_BreakPiece>();
		vStatus = "Idle";
		vNextStatus = "Idle";
		//vEvent = "Idle";
	}
	
	// Update is called once per frame
	void Update () {
		if (vStatus != vNextStatus)
			vIHaveToAct = true;
		else {
			vIHaveToAct = false;
		}
		if (cSE.vEvent == "StartActing"){
			cSE.vEvent = "Idle";
			if (vIHaveToAct){
				switch (vNextStatus){
				case "StartBreak":
					//foreach (Scr_BreakPiece tThis in vPieces){
					//	tThis.Break(transform.position); }
					vStatus = "StartBreak";
					vNextStatus = "BreakNext";
				break;
				case "BreakNext":
					foreach (Scr_BreakPiece tThis in vPieces){
						tThis.Break(transform.position); }
					vStatus = "Broken";
					vPit.SetActive(true);
					vNextStatus = "StartTimer";
				break;
				case "StartTimer":
					vStatus = "Timer";
					vTimer = 5;
					vNextStatus = "WaitTimer";
					break;
				case "WaitTimer":
					vStatus = "Timer";
					vTimer -= 1;
					vNextStatus = "WaitTimer";
					if (vTimer <= 0)
						vNextStatus = "Repair";
					break;
				case "Repair":
					vActive = false;
					vPit.SetActive(false);
					foreach (Scr_BreakPiece tThis in vPieces){
						tThis.Assemble(); }
					vStatus = "Idle";
					vNextStatus = "Idle";
					break;

				}
				//Vector3 tSpot = this.transform.position;
			}
		}
	}
	public void Activate(){
		if (!vActive && vNextStatus == "Idle"){
			vActive = true;
			vNextStatus = "StartBreak";
			}
	}

	void Die(){
		Destroy(this.gameObject);
	}
}
