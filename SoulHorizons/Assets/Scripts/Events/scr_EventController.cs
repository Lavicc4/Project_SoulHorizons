using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable] 
public class Scenario{

	public string name; 
	public Button thisButton; 
	public bool completed; 
	public bool active;
	public GameObject[] enemies = new GameObject[5];

	public void Init(){ 
		thisButton.onClick.AddListener (ButtonClick);
	} 
	public void DeActivate(){
		active = false;
		thisButton.gameObject.SetActive (false); 
	}

	public bool Activate(){
		
		if (!active && !completed) {
			active = true; 
			thisButton.gameObject.SetActive (true); 
			return true;
		}
		return false; 
	}

	public void ButtonClick(){
		Debug.Log (name); 
	}
}


public class scr_EventController : MonoBehaviour {
	

	public Scenario[] scenarios = new Scenario[10]; 
	public int numberOfActiveScenarios;
	 

	void Start () {



		//for loop to turn off all buttons
		for (int i = 0; i < scenarios.Length; i++) {
			scenarios [i].DeActivate ();
		}


	}
	

	void Update () {


		if (Input.GetKeyDown (KeyCode.J)) {	
			CycleEvents (); 
		}
		if (Input.GetKeyDown (KeyCode.E)) {	
		}



	}

	void CycleEvents(){


		for (int i = 0; i < scenarios.Length; i++) {
			scenarios [i].DeActivate (); 
		} 

		for (int i = 0; i < numberOfActiveScenarios; i++) {
			bool good = false; 
			int tries = 0; 
			while (!good  && tries < 50) {
				int num = Random.Range (0, scenarios.Length); 
				good = scenarios [num].Activate (); 
				tries++; 
			}
		}
	}

		

}
