using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class scr_EncounterButtons : MonoBehaviour
{


    public int encounterNumber;
    public int tier;
    public bool complete = false;
    public bool locked;
    public GameObject nextEncounter;
    public Image infoPanel; 

    private GameObject eventSystem; 


    void Start()
    {
        infoPanel.enabled = false; 
        eventSystem = GameObject.Find("/EventSystem"); 
    }


    void Update()
    {

        if (complete)
        {
            GetComponent<Image>().color = Color.red;
            //GetComponent<Button>().interactable = false; 
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }


        
        if(eventSystem.GetComponent<EventSystem>().currentSelectedGameObject == this.gameObject)
        {
            infoPanel.enabled = true; 

        }
        else
        {
            infoPanel.enabled = false;         
        }
        


    }

    public void SetComplete(bool completed)
    {

    }

    public void GatherInfo(int _encounterNumber, int _tier, bool _complete)
    {
        encounterNumber = _encounterNumber;
        tier = _tier;
        complete = _complete;
    }
}
