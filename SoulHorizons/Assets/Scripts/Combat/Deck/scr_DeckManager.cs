//Colin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Contains references to all parts of the deck system. Anything outside the deck system should use the public functions in this class for deck information.
/// </summary>
[RequireComponent(typeof(scr_Deck))]
public class scr_DeckManager : MonoBehaviour {

	public GameObject[] cardUI; //references to the cards UI
    TextMeshProUGUI[] cardNames; //the card names
    public Image[] cardArt; //the images
    Color textColor; //the default text of the color when not in focus
	scr_Deck deck_scr;
    int currentCard = 0;

    void Awake()
    {
        //get references
        deck_scr = GetComponent<scr_Deck>();
        cardNames = new TextMeshProUGUI[cardUI.Length];
        int i = 0;
        foreach (GameObject card in cardUI)
        {
            cardNames[i++] = card.GetComponentInChildren<TextMeshProUGUI>();//card.GetComponent<TextMeshPro>();
            if (cardNames[i-1] == null)
            {
                Debug.Log("Did not find component");
            }
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

    bool axisPressed = false;
    /// <summary>
    /// Gets user input.
    /// </summary>
    /// <returns>true if any input was detected, false otherwise.</returns>
    bool UserInput()
    {
        int axis = 0;
        if (!axisPressed)
        {
            //just pressed the joystick
            axis = scr_InputManager.HandScrolling();
            axisPressed = true;
        }

        if(scr_InputManager.HandScrolling() == 0)
        {
            //joystick is not pressed
            axisPressed = false;
        }

        if (scr_InputManager.PlayCard())
        {
            //play the current card
            deck_scr.Activate(currentCard);
            return true;
        }
        else if (axis < 0)
        {
            currentCard--;
            if(currentCard < 0)
            {
                currentCard = deck_scr.handSize - 1;
            }
            return true;
        }
        else if (axis > 0)
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
        //Debug.Log("Currently selected Card: \"" + deck_scr.hand[currentCard].cardName + "\" at index " + currentCard);

        //highlight the current card
        for (int i = 0; i < cardNames.Length; i++)
        {  
            cardNames[i].color = textColor;
        }
        cardNames[currentCard].color = Color.yellow;

        SetCardGraphics();
        //TODO: need to check if the UI matches the current hand. If not, need to start a fade out animation for the out of date cards, followed by a fade in for their replacement
    }

    void SetCardGraphics()
    {
        for (int i = 0; i < cardNames.Length; i++)
        {
            cardNames[i].text = deck_scr.hand[i].cardName; //set the name
            cardArt[i].sprite = deck_scr.hand[i].art; //set the card art
        }
    }
}
