using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_InvUI : MonoBehaviour {

    public scr_CardUI[] cardUI;
    public GameObject invPanel;
    public Text deckText;
    public Font UIFont;
    public float deckTextX = 600;
    public float deckTextY = 400;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (invPanel.activeSelf)
        {
            SetCardGraphics();
            SetDeckText();
        }
    }

    public void DisplayUI()
    {
        if (!invPanel.activeSelf)
        {
            invPanel.SetActive(true);
        }
        else
        {
            invPanel.SetActive(false);
        }
    }

    void SetCardGraphics()
    {
        for (int i = 0; i < cardUI.Length; i++)
        {
            if (i < scr_Inventory.cardInv.Count)
            {
                cardUI[i].SetName(scr_Inventory.cardInv[i].Key.cardName); //set the name
                cardUI[i].SetArt(scr_Inventory.cardInv[i].Key.art); //set the card art
                cardUI[i].SetElement(scr_Inventory.cardInv[i].Key.element); //set the card element

                //Find how many of this card are in your current deck 
                List<KeyValuePair<string, int>> myDeck = scr_Inventory.deckList[scr_Inventory.deckIndex];
                int index = -1;
                for(int j = 0; j < myDeck.Count; j++)
                {
                    if(myDeck[j].Key == scr_Inventory.cardInv[i].Key.cardName)
                    {
                        index = j;
                    }
                }
                if (index < 0) Debug.Log("CARD NOT FOUND");
                cardUI[i].SetBackupName((scr_Inventory.cardInv[i].Value - scr_Inventory.deckList[scr_Inventory.deckIndex][index].Value).ToString()); //Set the card amount currently in inventory
            }
            else
            {
                //MAKE CARD NOT SHOW UP
            }

           
            
        }
    }

    void SetDeckText()
    {
  
        string listText = "";
        foreach (KeyValuePair<string, int> pair in scr_Inventory.deckList[scr_Inventory.deckIndex])
        {

            listText += pair.Key + ": " + pair.Value + "\n";
        }
        deckText.text = listText;
    }

   
}
