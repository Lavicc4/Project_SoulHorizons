﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Attacks/LinearForward")]
public class atk_LinearForward : Attack {

    public float chanceToAttack = 30f;


    public override Vector2Int ProgressAttack(int xPos, int yPos, ActiveAttack activeAtk)
    {
        return LinearForward_ProgressAttack(xPos,yPos, activeAtk); 
    }

    Vector2Int LinearForward_ProgressAttack(int xPos, int yPos, ActiveAttack activeAtk)
    {


        scr_Grid.GridController.PrimeNextTile(xPos - 1,yPos);
        scr_Grid.GridController.ActivateTile(xPos, yPos); 
        return new Vector2Int(xPos - 1, yPos); 
    }
    public override bool CheckCondition(scr_Entity _ent)
    {
        return Random.Range(0, 100) < chanceToAttack; 
    }


    public override void ProgressEffects(ActiveAttack activeAttack)
    {
        activeAttack.particle.transform.position = Vector3.Lerp(activeAttack.particle.transform.position, scr_Grid.GridController.GetWorldLocation(activeAttack.lastPos.x,activeAttack.lastPos.y) + activeAttack._attack.particlesOffset, (4.5f) * Time.deltaTime);
    }
}