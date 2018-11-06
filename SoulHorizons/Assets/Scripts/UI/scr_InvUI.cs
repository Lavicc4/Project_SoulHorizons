using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_InvUI : MonoBehaviour {

    public scr_CardUI[] cardUI;
    public GameObject invPanel;
    public GameObject deckText;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (invPanel.activeSelf)
        {
            SetCardGraphics();
            //SetDeckText();
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
                cardUI[i].SetBackupName(scr_Inventory.cardInv[i].Value.ToString());
            }
            else
            {
                //MAKE CARD NOT SHOW UP
            }

           
            
        }
    }

}
