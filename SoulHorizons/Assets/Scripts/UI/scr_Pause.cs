//Author:
//Enrique Rodriguez
//Date:
//10/20/2018
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Pause : MonoBehaviour {


    private static bool paused = false;         //Determines if the game is paused. Must be False to work properly

    public GameObject pausePanel;               //Pause Panel when paused

    private void Start()
    {
        //pausePanel = GameObject.FindWithTag("PausePanel");
    }

    void Update () {
        pauseControl();
        togglePanel();
	}

    //Controls pause flow
    void pauseControl()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            togglePause();
        }
    }

    //Toggles pause
    void togglePause()
    {
        //If you hit escape, it pauses
        if (paused)
        {
            //Show pause panel
            Time.timeScale = 0f;
        }

        //If you hit escape again, it unpauses
        else
        {
            //Remove pause panel
            Time.timeScale = 1f;
        }
    }

    //Get paused value
    public bool getPaused()
    {
        return paused;
    }

    //Toggles pause panel
    void togglePanel()
    {
        pausePanel.SetActive(paused);
    }
}
