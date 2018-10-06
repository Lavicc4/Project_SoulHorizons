﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Attacks/PlayerBlaster")]
public class atk_PlayerBlaster : Attack {

    
    public override ActiveAttack BeginAttack(ActiveAttack activeAtk)
    {
        activeAtk.lastAttackTime -= activeAtk._attack.incrementSpeed;
        return activeAtk; 
    } 

    public override Vector2Int ProgressAttack(int xPos, int yPos, ActiveAttack activeAtk)
    {
        return LinearForward_ProgressAttack(xPos, yPos, activeAtk);
    }

    Vector2Int LinearForward_ProgressAttack(int xPos, int yPos, ActiveAttack activeAtk)
    {
        //move particles
        //ProgressEffects(xPos, yPos, activeAtk.lastPos.x, activeAtk.lastPos.y, activeParticle);

        scr_Grid.GridController.PrimeNextTile(xPos + 1, yPos);
        scr_Grid.GridController.ActivateTile(xPos, yPos);
        return new Vector2Int(xPos + 1, yPos);
    }
    public override bool CheckCondition(scr_Entity _ent)
    {
        return true; // to make it happy 
    }

    //--Effects Methods--
    public override void LaunchEffects(ActiveAttack activeAttack)
    {
        if (activeAttack == null)
        {
            Debug.Log("PlayerBlaster: Active Attack is null");
        }

        if (particles == null)
        {
            Debug.Log("PlayerBlaster: Active Attack is null");
        }

        if (activeAttack.pos == null)
        {
            Debug.Log("PlayerBlaster: Active Attack is null");
        }

            activeAttack.particle = Instantiate(particles, scr_Grid.GridController.GetWorldLocation(activeAttack.pos.x, activeAttack.pos.y) + particlesOffset, Quaternion.identity);
            activeAttack.particle.sortingOrder = -activeAttack.pos.y;
    }

    public override void ProgressEffects(ActiveAttack activeAttack)
    {
        activeAttack.particle.transform.position = Vector3.Lerp(activeAttack.particle.transform.position, scr_Grid.GridController.GetWorldLocation(activeAttack.lastPos.x,activeAttack.lastPos.y) + activeAttack._attack.particlesOffset, (4.5f) * Time.deltaTime);
    }

    public override void ImpactEffects(int xPos = -1, int yPos = -1)
    {

    }

    public override void EndEffects(ActiveAttack activeAttack)
    {

    }
}
