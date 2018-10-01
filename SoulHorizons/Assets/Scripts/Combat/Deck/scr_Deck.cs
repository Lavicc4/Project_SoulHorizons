//Colin
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

/// <summary>
/// Contains the deck and discard piles. Handles shuffling and drawing.
/// </summary>
public class scr_Deck : MonoBehaviour {

	public int deckSize = 30;
    public int handSize = 5;
    public scr_NameToCard cardMapping; //maps card name to the scriptable object for that card
    //public string DeckList; //the name of the file that contains the deck list
    public TextAsset deckList;
    [HideInInspector] public List<scr_Card> hand = new List<scr_Card>();
    List<scr_Card> deck = new List<scr_Card>();
    List<scr_Card> discard = new List<scr_Card>();

    public void Awake()
    {
        //make sure the deck has the correct number of elements
        for (int i = 0; i < handSize; i++)
        {
            hand.Add(null);
        }
        
        LoadDeckList();

    }

    /// <summary>
    /// Load the deck list from the file, shuffle, then draw a starting hand.
    /// </summary>
    void LoadDeckList()
    {
        //Debug.Log("Loading deck list");
        /*
        //load the list, use cardMapping to get the card object from the name in the list and put the cards in the deck
        StreamReader file = new StreamReader(Path.Combine("Assets/Scripts/Combat/Deck/Deck Lists", DeckList + ".txt"));
        if (file == null)
        {
            Debug.Log("File did not open");
            return;
        }
        */

        StringReader reader = new StringReader(deckList.text);
        string strLine;

        while ((strLine = reader.ReadLine()) != null)
        {
            //string strLine = file.ReadLine();
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
            scr_Card nextCard = cardMapping.ConvertNameToCard(parsedLine[0]);
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

        /*
        Debug.Log("Unshuffled Deck List");
        int j = 1;
        foreach (scr_Card item in deck)
        {
            Debug.Log(j++ + ": \"" + item.cardName + "\"");
        }
         */

        ShuffleHelper<scr_Card>(deck);
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
            ShuffleHelper<scr_Card>(deck);
        }
        else if(list.Equals("discard"))
        {
            ShuffleHelper<scr_Card>(discard);
            return;
        }
        else if(list.Equals("discard into deck"))
        {
            //move discard into deck
            foreach (scr_Card card in discard)
            {
                deck.Add(card);
                discard.Remove(card);
            }
            //shuffle
            ShuffleHelper<scr_Card>(deck);
        }
        else if(list.Equals("all"))
        {
            //move everything into deck
            foreach (scr_Card card in discard)
            {
                deck.Add(card);
                discard.Remove(card);
            }
            foreach (scr_Card card in hand)
            {
                deck.Add(card);
                hand.Remove(card);
            }

            //shuffle
            ShuffleHelper<scr_Card>(deck);
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

            //Debug.Log("Shuffled deck");
            //int i = 1;
            //foreach (scr_Card item in deck)
            //{
            //    Debug.Log(i++ + ": \"" + item.cardName + "\"");
            //}
    }

    /// <summary>
    /// Make sure that the hand size is correct. Draw cards as necessary.
    /// </summary>
    void CheckHandSize()
    {
        /*
        if (hand.Count < handSize)
        {
            int cardsToDraw = handSize - hand.Count;
            for (int i = 0; i < cardsToDraw; i++)
            {
                Draw();
            }
        }
        */
        int i = 0;
        foreach (scr_Card item in hand)
        {
            if(item == null)
            {
                Draw(i);
            }
            i++;
        }
    }

    /// <summary>
    /// Remove the top card from the deck and add it to the hand.
    /// </summary>
    /// <param name="index">The index to put the new card at</param>
    public void Draw(int index)
    {
        if (deck.Count > 0)
        {
            //Debug.Log("Deck size: " + deck.Count);
            //Debug.Log((deck[0] == null) ? ("first deck element is null") : ("Drew " + deck[0].cardName));
            //hand.Add(deck[0]);
            hand[index] = deck[0];
            deck.RemoveAt(0);
        }
        else
        {
            //TODO: What do we do when the deck runs out?
            Shuffle("discard into deck");
        }
    }

    /// <summary>
    /// Play the selected card, then move it to the discard pile.
    /// </summary>
    /// <param name="index"></param>
    public void Activate(int index)
    {

        StartCoroutine(ActivateHelper(index));
    }

    private IEnumerator ActivateHelper(int index)
    {
        scr_Card cardToPlay = hand[index];
        //wait however long is required
        if (cardToPlay.castingTime != 0)
        {
            yield return new WaitForSeconds(cardToPlay.castingTime);
        }
        hand[index].Activate();
        discard.Add(cardToPlay);
        //hand.Remove(cardToPlay);
        hand[index] = null;
        //make sure hand size is correct
        CheckHandSize();
    }
}
