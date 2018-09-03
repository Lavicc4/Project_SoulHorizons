//Colin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains references to all parts of the deck system. Anything outside the deck system should use the public functions in this class for deck information.
/// </summary>
[RequireComponent(typeof(Deck))]
public class DeckManager : MonoBehaviour {

	public GameObject[] cardUI; //references to the cards UI
	Deck deck_scr;
    int currentCard = 0;

    void Awake()
    {
        //get references
        deck_scr = GetComponent<Deck>();
    }

	void Start ()
    {
        UpdateGUI();
	}

    void Update()
    {
        if (UserInput())
        {
            UpdateGUI();
        }
    }

    /// <summary>
    /// Gets user input.
    /// </summary>
    /// <returns>true if any input was detected, false otherwise.</returns>
    bool UserInput()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //play the current card
            deck_scr.Activate(currentCard);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentCard--;
            if(currentCard < 0)
            {
                currentCard = deck_scr.handSize;
            }
        }
        return false;
    }

    /// <summary>
    /// Update the deck system GUI.
    /// </summary>
    void UpdateGUI()
    {
        //TODO: interact with the UI components
        //change selection highlight
        //start an animation on the currently selected card if it was played
        //shift cards if one was played
        //give the UI element the information for a newly drawn card; play animation for loading
        Debug.Log("Currently selected Card: " + deck_scr.hand[currentCard]);

    }
}
