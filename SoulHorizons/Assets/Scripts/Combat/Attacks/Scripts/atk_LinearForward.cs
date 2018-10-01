﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Attacks/LinearForward")]
public class atk_LinearForward : Attack {

    public float chanceToAttack = 30f;


    public override Vector2Int ProgressAttack(int xPos, int yPos, ActiveAttack activeAtk, SpriteRenderer activeParticle)
    {
        return LinearForward_ProgressAttack(xPos,yPos, activeAtk, activeParticle); 
    }

    Vector2Int LinearForward_ProgressAttack(int xPos, int yPos, ActiveAttack activeAtk, SpriteRenderer activeParticle)
    {
        //move particles
        ProgressEffects(xPos, yPos, activeParticle);

        scr_Grid.GridController.PrimeNextTile(xPos - 1,yPos);
        scr_Grid.GridController.ActivateTile(xPos, yPos); 
        return new Vector2Int(xPos - 1, yPos); 
    }
    public override bool CheckCondition(scr_Entity _ent)
    {
        return Random.Range(0, 100) < chanceToAttack; 
    }


    public override void ProgressEffects(int xPos, int yPos, SpriteRenderer activeParticle)
    {
        activeParticle.transform.position = Vector3.Lerp(activeParticle.transform.position, scr_Grid.GridController.GetWorldLocation(xPos, yPos) + particlesOffset, (4.5f) * Time.deltaTime);
    }
}
