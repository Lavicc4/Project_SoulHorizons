using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

//Cameron Made this 

public class scr_EncounterController : MonoBehaviour {

	public Button[] buttons = new Button[10];
	public int numberOfActiveScenarios; 

	void Start () {

		//for Loop to deactivate all of the buttons
		for (int i = 0; i < buttons.Length; i++) {
			buttons [i].gameObject.SetActive(false); 
		}
	}
	

	void Update () {

		if (Input.GetKeyDown (KeyCode.J)) {	
			CycleEvents (); 
		}

	}

	void CycleEvents(){

		//for Loop to deactivate all of the buttons - Here we are doing it to cycle the events.  We'll remove this loop later.
		for (int i = 0; i < buttons.Length; i++) {
			buttons [i].gameObject.SetActive(false); 
		} 

		//Loop to Randomize which buttons are on/off.  Also probably not exactly what we want, but will do for now. - Plus it dont even work right 
		for (int i = 0; i < numberOfActiveScenarios; i++) {							//Will run as many times as you want Active Scenarios
			bool good = false; 														//temp bool for incoming while loop
			int tries = 0; 															// This is not necessary, but a precaution.  If numberOfActiveScenariosn > the total number of buttons in the array, Unity dies.  This prevents it from running more than x times.  <see while loop> 
			while (!good  && tries < 50) {
				int num = Random.Range (0, buttons.Length); 						//generates a random number between 0 and (total number of buttons - 1) *since there is a button[0]
				if (!buttons [num].gameObject.activeSelf) {							//if this random button is not active 
					buttons [num].gameObject.SetActive (true); 						//turn on this random button
					good = buttons [num].gameObject.activeSelf;						// good = the return value of whether or not the button is active

				} 
				else {																//Else
					tries++; 														//try again
				}

			}
		}
	}
}
