using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

//Cameron Made this 

public class scr_EncounterController : MonoBehaviour {

    public static scr_EncounterController globalEncounterController; 
    public scr_SceneManager sceneManager;

    public EncounterSave[] encounterArray;




    
    public int totalButtons;
    public Button[] buttons = new Button[10];

    public GameObject buttonPrefab; 

    
    public Encounter[] tier1Encounters = new Encounter[1];
    public Encounter[] tier2Encounters = new Encounter[1];
    public Encounter[] tier3Encounters = new Encounter[1];
   
    
 
    
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

        encounterArray = new EncounterSave[totalButtons];

        if(SaveLoad.currentGame.encounterSaves == null)  //might be null, check if length is 0?
        {
            BuildMap();
            GenerateButtons();
            

        }
        else
        {
            GetSaveData(SaveLoad.currentGame.encounterSaves);
        }

        

    }
	

	void Update () {

        
      
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

    public void BuildMap()
    {

        for(int i = 0; i < buttons.Length; i++)
        {
            
            //need to make sure we dont pick the same Encounter 2x. 
            //here is where we will decide if the button gets to be a t1, t2 or t3 encounter.
            if(encounterArray[i].tier == 1)
            {
                int num = Random.Range(0, tier1Encounters.Length);
                encounterArray[i].encounterNumber = num;
            }
            else if (encounterArray[i].tier == 1)
            {
                int num = Random.Range(0, tier2Encounters.Length);
                encounterArray[i].encounterNumber = num;
            }
            else if (encounterArray[i].tier == 1)
            {
                int num = Random.Range(0, tier3Encounters.Length);
                encounterArray[i].encounterNumber = num;
            }


            //THIS STATEMENT BELOW WILL PICK A RANDOM TIER 1 ENCOUNTER IN ADDITION TO WHATEVER WE ATTACHED TO THE BUTTON 
            //listeners[i].onClick.AddListener(delegate { GoToEncounter(tier1Encounters[num]); });





        }
        



    }

    public void GetSaveData(EncounterSave[] _encounters)
    {

        encounterArray = new EncounterSave[_encounters.Length];


        for (int i = 0; i < _encounters.Length; i++)
        {
            encounterArray[i].encounterNumber = _encounters[i].encounterNumber;
            encounterArray[i].tier = _encounters[i].tier;
        }
        GenerateButtons(); 

    }

    public void GenerateButtons()
    {
        GameObject encounterCanvas = GameObject.FindWithTag("EncounterCanvas");

        for (int i = 0; i < totalButtons - 1; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab);
            newButton.transform.parent = encounterCanvas.transform;
            if (encounterArray[i].tier == 1)
            {
                newButton.GetComponent<Button>().onClick.AddListener(delegate { GoToEncounter(tier1Encounters[encounterArray[i].encounterNumber]); });
            }
            else if(encounterArray[i].tier == 2)
            {
                newButton.GetComponent<Button>().onClick.AddListener(delegate { GoToEncounter(tier2Encounters[encounterArray[i].encounterNumber]); });
            }
            else if (encounterArray[i].tier == 3)
            {
                newButton.GetComponent<Button>().onClick.AddListener(delegate { GoToEncounter(tier3Encounters[encounterArray[i].encounterNumber]); });
            }


        }
    }













    /*
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
     * 
     * */
}




