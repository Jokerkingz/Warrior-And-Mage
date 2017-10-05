using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackTest : MonoBehaviour {

	public float dmg;

	public GameObject player;

	void Start () {
		
	}
	

	void Update () {

		if (Input.GetKeyDown (KeyCode.PageUp)) {
			player.SendMessage ("Damage", dmg);
		}
	}
}
