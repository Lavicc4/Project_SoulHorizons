//Colin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains the deck and discard piles. Handles shuffling and drawing.
/// </summary>
public class Deck : MonoBehaviour {

	public int deckSize = 30;
    public int handSize = 5;
    List<Card> hand = new List<Card>();
    List<Card> deck = new List<Card>();
    List<Card> discard = new List<Card>();

    public void Awake()
    {
        
        LoadDeckList();
    }

    /// <summary>
    /// Load the deck list from the file, shuffle, then draw a starting hand.
    /// </summary>
    void LoadDeckList()
    {
        //TODO: load the list into card objects and put the objects in the deck

        ShuffleHelper<Card>(deck);
        CheckHandSize();
    }

    /// <summary>
    /// Pass a string telling the method what list to shuffle. Options are "deck", "discard", "discard into deck", and "all".
    /// </summary>
    /// <param name="list"></param>
    public void Shuffle(string list)
    {
        if (list.Equals("deck"))
        {
            ShuffleHelper<Card>(deck);
        }
        else if(list.Equals("discard"))
        {
            ShuffleHelper<Card>(discard);
        }
        else if(list.Equals("discard into deck"))
        {
            //move discard into deck
            foreach (Card card in discard)
            {
                deck.Add(card);
                discard.Remove(card);
            }
            //shuffle
            ShuffleHelper<Card>(deck);
        }
        else if(list.Equals("all"))
        {
            //move everything into deck
            foreach (Card card in discard)
            {
                deck.Add(card);
                discard.Remove(card);
            }
            foreach (Card card in hand)
            {
                deck.Add(card);
                hand.Remove(card);
            }

            //shuffle
            ShuffleHelper<Card>(deck);

            //draw a new hand
            CheckHandSize();
        }
    }

    /// <summary>
    /// Shuffles the list provided.
    /// </summary>
    /// <param name="list"></param>
    void ShuffleHelper<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    /// <summary>
    /// Make sure that the hand size is correct. Draw cards as necessary.
    /// </summary>
    void CheckHandSize()
    {
        if (hand.Count < handSize)
        {
            for (int i = 0; i < (handSize - hand.Count); i++)
            {
                Draw();
            }
        }
    }

    /// <summary>
    /// Remove the top card from the deck and add it to the hand.
    /// </summary>
    public void Draw()
    {
        if (deck.Count > 0)
        {
            hand.Add(deck[0]);
            deck.RemoveAt(0);
        }
        else
        {
            //TODO: What do we do when the deck runs out?
        }
    }

    /// <summary>
    /// Play the selected card, then move it to the discard pile.
    /// </summary>
    /// <param name="index"></param>
    public void Activate(int index)
    {
        Card cardToPlay = hand[index];
        hand[index].Activate();
        discard.Add(cardToPlay);
        hand.Remove(cardToPlay);
        //make sure hand size is correct
        CheckHandSize();
    }
}
