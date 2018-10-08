﻿//Colin
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

	public scr_CardUI[] cardUI; //references to the cards UI
    //TextMeshProUGUI[] cardNames; //the card names
    //public Image[] cardArt; //the images
    //Color textColor; //the default text of the color when not in focus
	scr_Deck deck_scr;
    int currentCard = 0;

    private bool readyToCast = true;
    public float cooldown = 0.6f; //the rate at which the player can play cards; could make this a variable in the card instead

    void Awake()
    {
        //get references
        deck_scr = GetComponent<scr_Deck>();
        /* Removed since the CardUI script was added
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
         */
    }

	void Start ()
    {
        UpdateGUI();
	}

    void Update()
    {
        UserInput();
        UpdateGUI();
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

        if (scr_InputManager.PlayCard() && readyToCast)
        {
            //start cooldown to be able to cast another card; could use an argument from the card to get variable cooldowns
            StartCoroutine(CastCooldown(cooldown));
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

        //highlight the current card
        for (int i = 0; i < cardUI.Length; i++)
        {  
            //cardNames[i].color = textColor;
            cardUI[i].SetSelected(false);
        }
        cardUI[currentCard].SetSelected(true);

        SetCardGraphics();
        //TODO: need to check if the UI matches the current hand. If not, need to start a fade out animation for the out of date cards, followed by a fade in for their replacement
    }

    /// <summary>
    /// Gets all graphical info from the card object and sets the UI accordingly
    /// </summary>
    void SetCardGraphics()
    {
        for (int i = 0; i < cardUI.Length; i++)
        {
            cardUI[i].SetName(deck_scr.hand[i].cardName); //set the name
            cardUI[i].SetArt(deck_scr.hand[i].art); //set the card art
            cardUI[i].SetElement(deck_scr.hand[i].element); //set the card element
        }
    }

    /// <summary>
	/// Called when casting a card to give a cooldown until another card can be cast
	/// </summary>
	/// <returns></returns>
	private IEnumerator CastCooldown(float cooldown)
	{
        Debug.Log("Hello, I am on cooldown");
		readyToCast = false;
		yield return new WaitForSeconds(cooldown);
        Debug.Log("Hello, I am off of cooldown");
        readyToCast = true;
	}
}
