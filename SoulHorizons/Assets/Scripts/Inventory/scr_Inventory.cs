using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class scr_Inventory{

    public static int dustNum = 0; //How much dust you have
    public static List<KeyValuePair<scr_Card, int>> cardInv; //Your list of cards
    public static List<scr_Deck> deckList; //Your decks
    public static int deckIndex = 0; //Index of currently equipped deck

    public static void addDeck(scr_Deck deck)
    {
        Debug.Log("DECK ADDED");
        deckList.Add(deck);
    }

    public static void addCard(scr_Card card, int quantity)
    {
        foreach(KeyValuePair<scr_Card, int> pair in cardInv)
        {
            if(pair.Key.cardName == card.cardName)
            {
                int prevNum = pair.Value;
                cardInv.Remove(pair);
                cardInv.Add(new KeyValuePair<scr_Card, int>(card, quantity + prevNum));
                return;
            }
        }
        cardInv.Add(new KeyValuePair<scr_Card, int>(card, quantity));
    }
}
