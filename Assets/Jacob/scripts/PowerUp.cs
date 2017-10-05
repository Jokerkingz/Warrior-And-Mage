using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	public GameObject Spell;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Mage")
			{
				other.gameObject.GetComponent<Inventory>().AddNewSpell (Spell);
			}
	}
}
