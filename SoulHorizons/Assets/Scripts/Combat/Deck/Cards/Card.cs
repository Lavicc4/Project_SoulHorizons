using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : ScriptableObject {

    public List<string> keywords;
    public string cardName;
    public float castingTime = 1f;

    public abstract void Activate();
}
