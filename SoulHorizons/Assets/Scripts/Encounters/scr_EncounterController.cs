using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

//Cameron Made this 

public class scr_EncounterController : MonoBehaviour {

    public static scr_EncounterController globalEncounterController; 
    public scr_SceneManager sceneManager;
    public Button[] buttons = new Button[10];
	public int activeScenarios;
    public int totalButtons; 
    public Encounter[] tier1Encounters = new Encounter[10];
    public Encounter[] tier2Encounters = new Encounter[5];
    public Encounter[] tier3Encounters = new Encounter[3];
   
    private Button[] listeners;
 

	void Start () {

        if (globalEncounterController != null && globalEncounterController != this)
        {
            Destroy(gameObject);
        }
        else
        {
            globalEncounterController = this;
            DontDestroyOnLoad(this.gameObject);
        }

                


        /*
        listeners = new Button[totalButtons];

        //for Loop to deactivate all of the buttons
        for (int i = 0; i < buttons.Length; i++) {
			buttons [i].gameObject.SetActive(false); 
		}


        for(int i = 0; i < buttons.Length; i++)
        {
            listeners[i] = buttons[i].GetComponent<Button>();
            //need to make sure we dont pick the same Encounter 2x. 
            //here is where we will decide if the button gets to be a t1, t2 or t3 encounter.
            int num = Random.Range(0, tier1Encounters.Length);
            
            //THIS STATEMENT BELOW WILL PICK A RANDOM TIER 1 ENCOUNTER IN ADDITION TO WHATEVER WE ATTACHED TO THE BUTTON 
            //listeners[i].onClick.AddListener(delegate { GoToEncounter(tier1Encounters[num]); });

        }
        */

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
		for (int i = 0; i < activeScenarios; i++) {							//Will run as many times as you want Active Scenarios
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

    public void GoToEncounter(Encounter encounterName)
    {
        //Here is where we will put all of our info about the encounter
        //SceneManager.LoadScene or whatever (encounterName.Scene); 
        string nameOfEncounter = encounterName.name;
        Debug.Log(nameOfEncounter);
        scr_SceneManager.globalSceneManager.currentEncounter = encounterName;
        scr_SceneManager.globalSceneManager.ChangeScene(encounterName.sceneName);  
    }


}




