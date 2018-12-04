using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_EncounterButtons : MonoBehaviour
{


    public int encounterNumber;
    public int tier;
    public bool complete = false;
    public bool locked;
    public GameObject nextEncounter;


    void Start()
    {

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
