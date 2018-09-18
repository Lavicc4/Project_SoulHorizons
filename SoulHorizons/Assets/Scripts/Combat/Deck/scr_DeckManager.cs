//Colin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contains references to all parts of the deck system. Anything outside the deck system should use the public functions in this class for deck information.
/// </summary>
[RequireComponent(typeof(scr_Deck))]
public class scr_DeckManager : MonoBehaviour {

	public GameObject[] cardUI; //references to the cards UI
    Text[] cardNames; //the card names
    Color textColor; //the default text of the color when not in focus
	scr_Deck deck_scr;
    int currentCard = 0;

    void Awake()
    {
        //get references
        deck_scr = GetComponent<scr_Deck>();
        cardNames = new Text[cardUI.Length];
        int i = 0;
        foreach (GameObject card in cardUI)
        {
            cardNames[i++] = card.GetComponent<Text>();
        }
    }

	void Start ()
    {
        textColor = cardNames[0].color;
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
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentCard--;
            if(currentCard < 0)
            {
                currentCard = deck_scr.handSize - 1;
            }
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentCard = (currentCard + 1) % deck_scr.handSize;
            return true;
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
        Debug.Log("Currently selected Card: \"" + deck_scr.hand[currentCard].cardName + "\" at index " + currentCard);

        //highlight the current card
        for (int i = 0; i < cardNames.Length; i++)
        {  
            cardNames[i].color = textColor;
        }
        cardNames[currentCard].color = Color.white;

        SetCardNames();
        //TODO: need to check if the UI matches the current hand. If not, need to start a fade out animation for the out of date cards, followed by a fade in for their replacement
    }

    void SetCardNames()
    {
        for (int i = 0; i < cardNames.Length; i++)
        {
            cardNames[i].text = deck_scr.hand[i].cardName;
        }
    }
}
