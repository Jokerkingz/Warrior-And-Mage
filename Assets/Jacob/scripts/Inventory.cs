using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public List<GameObject> SpellInventory = new List <GameObject> ();

	public void AddNewSpell(GameObject newSpell)
	{
		for(int i = 0; i <SpellInventory.Count; ++i)
		{
			if (SpellInventory [i].name == newSpell.name)
				return;
		}
		SpellInventory.Add(newSpell);
	}


}
