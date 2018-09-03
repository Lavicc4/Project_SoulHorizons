//Colin
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

/// <summary>
/// Contains the deck and discard piles. Handles shuffling and drawing.
/// </summary>
public class Deck : MonoBehaviour {

	public int deckSize = 30;
    public int handSize = 5;
    public NameToCard cardMapping; //maps card name to the scriptable object for that card
    public string DeckList; //the name of the file that contains the deck list
    [HideInInspector] public List<Card> hand = new List<Card>();
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
        Debug.Log("Loading deck list");
        //load the list, use cardMapping to get the card object from the name in the list and put the cards in the deck
        StreamReader file = new StreamReader(Path.Combine("Assets/Scripts/Combat/Deck/Deck Lists", DeckList + ".txt"));
        if (file == null)
        {
            Debug.Log("File did not open");
            return;
        }

        while (!file.EndOfStream)
        {
            string strLine = file.ReadLine();
            string[] parsedLine = strLine.Split( ':');
            //check that there was only one colon in the line
            if (parsedLine.Length != 2)
            {
                Debug.Log("Line had wrong number of colons");
                continue;
            }
            
            //check that the second element is a number
            parsedLine[1] = parsedLine[1].Trim();
            if(!Regex.IsMatch(parsedLine[1], @"^\d+$"))
            {
                Debug.Log("element after colon is not all digits");
                continue;
            }
            int quantity = int.Parse(parsedLine[1]);

            //attempt to retrieve the object reference from cardMapping
            Card nextCard = cardMapping.ConvertNameToCard(parsedLine[0]);
            if (nextCard == null)
            {
                continue;
            }

            //add that card to the list a number of times equal to the quantity
            for(int i = 0; i < quantity; i++)
            {
                deck.Add(nextCard);
            }
        }

        if (deck.Count != deckSize)
        {
            Debug.Log("DeckSize is " + deckSize + ", but " + deck.Count + " cards were added to the deck");
        }

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
