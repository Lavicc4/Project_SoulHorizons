using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class scr_Card : ScriptableObject {
    public enum Element
    {
        Earth,
        Soul,
        Sun,
        Void,
        Wind
    }


    public List<string> keywords;
    public string cardName;
    public Element element;
    public float castingTime = 1f;
    [Multiline]
    public string description;
    public Sprite art;
    public abstract void Activate();
}
