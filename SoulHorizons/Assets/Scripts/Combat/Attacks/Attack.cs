﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[System.Serializable]
public class Attack : ScriptableObject {

    public float incrementSpeed;
    public int maxIncrements = 1;
    public int damage;
    [Header("Where the attack is coming from")]
    public scr_Tile.Territory territory;
    

	public virtual Vector2Int BeginAttack()
    {
        return new Vector2Int(); 
    }
    public virtual Vector2Int ProgressAttack(int xPos, int yPos)
    {
        return new Vector2Int(); 
    }
    public virtual bool CheckCondition(scr_Entity _ent)
    {
        return false; 
    }
}
