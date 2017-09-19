using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signTrigger : MonoBehaviour {

	[Header("Floats")]
	public float distanceWar;
	public float distanceMag;
	public float range;

	[Header("References")]
	public GameObject warrior;
	public GameObject mage;

	public GameObject sign;

	void Start()
	{
		warrior = GameObject.FindGameObjectWithTag ("Warrior");
		mage = GameObject.FindGameObjectWithTag ("Mage");
	}

	void Update()
	{
		distanceWar = Vector3.Distance (transform.position, warrior.transform.position);
		distanceMag = Vector3.Distance (transform.position, mage.transform.position);

		if (distanceWar >= range) {
			sign.SendMessage("Disappear");
			if (distanceMag <= range) {
				sign.SendMessage ("Appear");
			}

		}

		if (distanceWar <= range) {
			sign.SendMessage ("Appear");
		}

		if (distanceMag>= range) {
			sign.SendMessage("Disappear");
			if (distanceWar <= range) {
				sign.SendMessage ("Appear");
			}
		}

		if (distanceMag <= range) {
			sign.SendMessage ("Appear");
		}
	}
}