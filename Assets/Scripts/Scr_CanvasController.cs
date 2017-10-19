using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_CanvasController : MonoBehaviour {
	public GameObject[] vObjectList;
	public Text[] vTextList;
	public string vState;
	public float vWarrItem;
	public float vMageItem;
	public Scr_Global cG;
	// Use this for initialization
	void Start () {
		vState = "Normal";
		cG = GameObject.FindGameObjectWithTag("GameController").GetComponent<Scr_Global>();
		RegisterButtons();
		
	}
	
	// Update is called once per frame
	void Update () {
		vWarrItem -= .5f;
		vWarrItem = Mathf.Clamp(vWarrItem,0f,1f);
		vMageItem -= .5f;
		vMageItem = Mathf.Clamp(vMageItem,0f,1f);
		if (vWarrItem >=0f || vMageItem >=0f)
			RegisterButtons();
	}
	public void RegisterButtons(){
		//Debug.Log("Register");
		//Debug.Log(vTextList.GetLength(0));
		//Debug.Log(cG.SkillList.GetLength(0));
		switch (vState){
		case "Normal":
			if (vWarrItem <= 0){

				vTextList[0].text = "Movement";
				vTextList[1].text = cG.SkillList[0];
				vTextList[2].text = cG.SkillList[1];
				vTextList[3].text = cG.SkillList[2];
				vTextList[4].text = cG.SkillList[3];
				vTextList[5].text = "Item";
				vTextList[6].text = "Wait";}
			else {
				vTextList[0].text = "";
				vTextList[1].text = "Health Potion ("+cG.Global_HealthPotion_Count.ToString() +")";
				vTextList[2].text = "Next Target";
				vTextList[3].text = "Teleport Stone";
				vTextList[4].text = "Elixer ("+cG.Global_ElixerPotion_Count.ToString() +")";
				vTextList[5].text = "~Item~";
				vTextList[6].text = "Wait";
			}
			if (vMageItem <= 0){
				vTextList[7].text = "Movement";
				vTextList[8].text = cG.SkillList[4];
				vTextList[9].text = cG.SkillList[5];
				vTextList[10].text = cG.SkillList[6];
				vTextList[11].text = cG.SkillList[7];
				vTextList[12].text = "Item";
				vTextList[13].text = "Wait";}
			else {
				vTextList[7].text = "";
				vTextList[8].text = "Health Potion ("+cG.Global_HealthPotion_Count.ToString() +")";
				vTextList[9].text = "Next Target";
				vTextList[10].text = "Teleport Stone";
				vTextList[11].text = "Elixer ("+cG.Global_ElixerPotion_Count.ToString() +")";
				vTextList[12].text = "~Item~";
				vTextList[13].text = "Wait";
			}
			//GameObject[0] 
			break;
		case "Item":
			

			//GameObject[0] 
		break;



		}

	}
}
