//Colin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains references to all parts of the deck system. Anything outside the deck system should use the public functions in this class for deck information.
/// </summary>
public class DeckManager : MonoBehaviour {

	public GameObject[] cardUI; //references to the cards UI
	Deck deck_scr;
    int currentCard = 0;

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

    }
}
