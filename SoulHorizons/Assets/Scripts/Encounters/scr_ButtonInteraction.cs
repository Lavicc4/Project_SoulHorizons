using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_ButtonInteraction : MonoBehaviour {

	Encounter encounter; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GoToEncounter(Encounter encounterName){
		//Here is where we will put all of our info about the encounter
		//SceneManager.LoadScene or whatever (encounterName.Scene); 
		string nameOfEncounter = encounterName.name;
		Debug.Log (nameOfEncounter); 
	}
}
