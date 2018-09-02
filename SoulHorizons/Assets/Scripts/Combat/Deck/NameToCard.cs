//Colin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Connects the data loaded from the save file to the object that corresponds to that card. Holds a list of all the card objects.
/// </summary>
public class NameToCard : ScriptableObject {

    public List<Card> cards = new List<Card>();

    /// <summary>
    /// Takes a card name and returns the object corresponding to that name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns>Returns the scriptable object or NULL.</returns>
    public Card ConvertNameToCard(string name)
    {
        return null;
    }
}
