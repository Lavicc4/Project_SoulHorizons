﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Attacks/LinearForward")]
public class atk_LinearForward : Attack {


    public override Vector2Int ProgressAttack(int xPos, int yPos)
    {
        return LinearForward_ProgressAttack(xPos,yPos); 
    }

    Vector2Int LinearForward_ProgressAttack(int xPos, int yPos)
    {
        scr_Grid.GridController.PrimeNextTile(xPos - 1,yPos);
        scr_Grid.GridController.ActivateTile(xPos, yPos); 
        return new Vector2Int(xPos - 1, yPos); 
    }
}
