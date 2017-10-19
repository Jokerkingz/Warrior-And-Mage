using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DieAfter : MonoBehaviour {
	public float vTimer = 2f;
	void Start(){
		Invoke("Die",vTimer);

	}
	void Die(){
		Destroy(this.gameObject);
	}
}
