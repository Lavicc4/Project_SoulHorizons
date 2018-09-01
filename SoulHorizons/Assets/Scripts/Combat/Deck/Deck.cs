using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Card
{
	int num;
	string name;
}

/// <summary>
/// Contains the deck and discard piles. Handles shuffling and drawing.
/// </summary>
public class Deck : MonoBehaviour {

	public int deckSize = 30;
	List<Card> deck;

	void Start () {
		deck = new List<Card>();
	}

	/// <summary>
	/// Loads the list of cards in the deck from a save file
	/// </summary>
	void LoadDeckList() {
		
	}

	/// <summary>
	/// Shuffles the deck. By default this does not include the hand.
	/// </summary>
	/// <param name="shuffleHand"></param>
	void Shuffle(bool shuffleHand = false) {

	}
	
}
