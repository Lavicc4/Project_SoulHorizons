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
[RequireComponent(typeof(AudioSource))]

public class scr_DeckManager : MonoBehaviour {

	public scr_CardUI[] cardUI; //references to the cards UI
    //TextMeshProUGUI[] cardNames; //the card names
    //public Image[] cardArt; //the images
    //Color textColor; //the default text of the color when not in focus
	scr_Deck deck_scr;
    [SerializeField] scr_SoulManager soulManager;
    int currentCard = 0;
    public float doublePressWindow = 0.3f; //the window of time the user has to press the same card again and have it register as a double press

    private bool readyToCast = true;
    //public float cooldown = 0.6f; //the rate at which the player can play cards; could make this a variable in the card instead

    AudioSource CardChange_SFX;
    public AudioClip cardChange_SFX;

    private bool disabled = false;

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
        AudioSource SFX_Source = GetComponent<AudioSource>();
        CardChange_SFX = SFX_Source;
        UpdateGUI();
	}

    void Update()
    {
        if (!disabled)
        {
            UserInput();
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
        /* NOTE: We are not currently scrolling through the hand
        int axis = 0;
        if (!axisPressed)
        {
            //just pressed the joystick
            axis = scr_InputManager.HandScrolling() * -1;
            axisPressed = true;
        }

        if(scr_InputManager.HandScrolling() == 0)
        {
            //joystick is not pressed
            axisPressed = false;
        }
         */

        if (scr_InputManager.PlayCard() != -1 && readyToCast)
        {
            CardInput();
            return true;
        }

        /* NOTE: We are not currently scrolling through the hand
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
            CardChange_SFX.clip = cardChange_SFX;
            CardChange_SFX.Play();
            currentCard = (currentCard + 1) % deck_scr.handSize;
            return true;
        }
         */
        return false;
    }

    //On single press, toggle or switch the area of effect highlight.
    //On double press, play the card and remove any highlighting
    int lastCardPressed = -1; //the last value returned from play card
    float timeSincePressed = 0; //the time since the last card was pressed
    /// <summary>
    /// Handles input for the cards. Determines if a card has been pressed once or double pressed.
    /// </summary>
    void CardInput()
    {
        bool doublePress = timeSincePressed < doublePressWindow;
        int input = scr_InputManager.PlayCard();

        if (input != -1)
        {
            foreach (scr_CardUI card in cardUI)
            {
                card.StartCooldown(deck_scr.hand[input].cooldown);
            }
        }
        //determine what card has been pressed, then decide what to do about it
        switch (input)
        {
            case 0:
                /* NOTE: This section is for double press to play and single press to highlight
                //check for double press
                if (doublePress)
                {
                    //start cooldown to be able to cast another card; could use an argument from the card to get variable cooldowns
                    StartCoroutine(CastCooldown(cooldown));
                    //play this card
                    deck_scr.Activate(0);

                    //reset the time
                    timeSincePressed = 0f;
                }
                else
                {
                    if (lastCardPressed == 0)
                    {
                        //turn off the highlighting
                    }
                    else
                    {
                        //turn on area of effect highlighting
                    }
                }
                 */
                //start cooldown to be able to cast another card
                StartCoroutine(CastCooldown(deck_scr.hand[0].cooldown));
                //charge the soul transform
                soulManager.ChargeSoulTransform(deck_scr.hand[0].element, deck_scr.hand[0].chargeAmount);
                deck_scr.Activate(0);
                break;
            case 1:
                //start cooldown to be able to cast another card
                StartCoroutine(CastCooldown(deck_scr.hand[1].cooldown));
                //charge the soul transform
                soulManager.ChargeSoulTransform(deck_scr.hand[1].element, deck_scr.hand[1].chargeAmount);
                deck_scr.Activate(1);
                break;
            case 2:
                //start cooldown to be able to cast another card
                StartCoroutine(CastCooldown(deck_scr.hand[2].cooldown));
                //charge the soul transform
                soulManager.ChargeSoulTransform(deck_scr.hand[2].element, deck_scr.hand[2].chargeAmount);
                deck_scr.Activate(2);
                break;
            case 3:
                //start cooldown to be able to cast another card
                StartCoroutine(CastCooldown(deck_scr.hand[3].cooldown));
                //charge the soul transform
                soulManager.ChargeSoulTransform(deck_scr.hand[3].element, deck_scr.hand[3].chargeAmount);
                deck_scr.Activate(3);
                break;
            default:
                break;
        }
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
        /* NOTE: We currently do not have a current card, so no selection is performed
        for (int i = 0; i < cardUI.Length; i++)
        {  
            //cardNames[i].color = textColor;
            cardUI[i].SetSelected(false);
        }
        cardUI[currentCard].SetSelected(true);
         */

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
            if (deck_scr.hand[i] != null) //the slot in the hand may not have been refilled if the cooldown is not finished
            {
                cardUI[i].SetName(deck_scr.hand[i].cardName); //set the name
                cardUI[i].SetArt(deck_scr.hand[i].art); //set the card art
                cardUI[i].SetElement(deck_scr.hand[i].element); //set the card element
            }
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

    public void OnDisable()
    {
        
    }
}
