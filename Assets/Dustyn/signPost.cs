using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signPost : MonoBehaviour {

	[Header("Floats")]
	public float distanceWar;
	public float distanceMag;
	public float range;

	[Header("References")]
	public GameObject warrior;
	public GameObject mage;

	public GameObject sign;

	void Start()
	{}

	void Update()
	{
		distanceWar = Vector3.Distance (transform.position, warrior.transform.position);
		distanceMag = Vector3.Distance (transform.position, mage.transform.position);

		if (distanceWar >= range) {
			//Debug.Log ("cannot read");
		}

		if (distanceWar <= range) {
			sign.SendMessage ("Appear");
			//Debug.Log("Can Read!!");
		}

		if (distanceMag>= range) {
			//Debug.Log ("cannot read");
		}

		if (distanceMag <= range) {
			sign.SendMessage ("Appear");
			//Debug.Log("Can Read!!");
		}
	}
}