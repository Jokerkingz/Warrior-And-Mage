using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Scrolls : MonoBehaviour {
	private Scr_Global cG;
	public string vSkillName;
	public Scr_CanvasController cCn;
	public int vArray;
	public GameObject vModel;
	private float vAngle;
	// Use this for initialization
	void Start () {
		cG = GameObject.FindGameObjectWithTag("GameController").GetComponent<Scr_Global>();
	}
	
	// Update is called once per frame
	void Update () {
		vAngle += Time.deltaTime*45f;
		vModel.transform.eulerAngles = new Vector3 (-90f, vAngle, 0f);
	}
	void OnTriggerEnter(Collider tOther){
		if (tOther.tag == "Warrior" || tOther.tag == "Mage"){
			Debug.Log("Trigs");
			cG.SkillList[vArray] = vSkillName;
			cCn.RegisterButtons();
			Destroy(this.gameObject);
		}
	}
}
