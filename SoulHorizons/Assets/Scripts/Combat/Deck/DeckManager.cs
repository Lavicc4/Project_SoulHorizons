using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains references to all parts of the deck system. Anything outside the deck system should use the public functions in this class for deck information.
/// </summary>
public class DeckManager : MonoBehaviour {

	public GameObject[] cardUI; //references to the cards UI
	Deck deck_scr;
	Spells spells_scr;

	void Start () {
		
	}
	
	void Update () {
		
	}
}
