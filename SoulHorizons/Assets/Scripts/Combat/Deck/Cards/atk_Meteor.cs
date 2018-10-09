﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Attacks/Meteor")]
public class atk_Meteor : Attack {


    public override Vector2Int BeginAttack(int xPos, int yPos, ActiveAttack activeAtk)
    {
       
        for(int i = 0; i < scr_Grid.GridController.ySizeMax; i++)
        {
            scr_Grid.GridController.PrimeNextTile(xPos, i);
            activeAtk.particles[i] = Instantiate(particles, scr_Grid.GridController.GetWorldLocation(activeAtk.entity._gridPos.x, activeAtk.entity._gridPos.y) + new Vector3(0,2.5f,0), Quaternion.Euler(new Vector3(0,0,33)));
        }
        return new Vector2Int(xPos, yPos); 
    }

    public override Vector2Int ProgressAttack(int xPos, int yPos, ActiveAttack activeAtk)
    {
        return LinearForward_ProgressAttack(xPos, yPos, activeAtk);
    }

    Vector2Int LinearForward_ProgressAttack(int xPos, int yPos, ActiveAttack activeAtk)
    {

        switch (activeAtk.currentIncrement)
        {
            case 0:
                scr_Grid.GridController.ActivateTile(xPos, yPos, activeAtk);
                return new Vector2Int(xPos, yPos + 1);

            case 1:
                scr_Grid.GridController.ActivateTile(xPos, yPos, activeAtk);
                return new Vector2Int(xPos, yPos - 2);

            case 2:
                scr_Grid.GridController.ActivateTile(xPos, yPos, activeAtk);
                return new Vector2Int(xPos, yPos);


        }
        return new Vector2Int(xPos, yPos);
    }
    public override bool CheckCondition(scr_Entity _ent)
    {
        return true; 
    }

    //--Effects Methods--
    
    public override void LaunchEffects(ActiveAttack activeAttack)
    {
        //activeAttack.particle = Instantiate(particles, scr_Grid.GridController.GetWorldLocation(activeAttack.pos.x, activeAttack.pos.y) + particlesOffset, Quaternion.identity);
        //activeAttack.particle.sortingOrder = -activeAttack.pos.y;
    }

    public override void ProgressEffects(ActiveAttack activeAttack)
    {

        //activeAttack.particle.transform.position = Vector3.Lerp(activeAttack.particle.transform.position, scr_Grid.GridController.GetWorldLocation(activeAttack.lastPos.x, activeAttack.lastPos.y) + activeAttack._attack.particlesOffset, (4.5f) * Time.deltaTime);
        switch (activeAttack.currentIncrement)
        {
            case 0:
                /*
                activeAttack.particles[0].sortingOrder = -1;
                activeAttack.particles[1].sortingOrder = -2;
                activeAttack.particles[2].sortingOrder = 0;
                */
                activeAttack.particles[0].transform.position = Vector3.MoveTowards(activeAttack.particles[0].transform.position, scr_Grid.GridController.GetWorldLocation(activeAttack.pos) + activeAttack._attack.particlesOffset, (18f) * Time.deltaTime);
                break; 

            case 1:
                activeAttack.particles[1].transform.position = Vector3.MoveTowards(activeAttack.particles[1].transform.position, scr_Grid.GridController.GetWorldLocation(activeAttack.pos) + activeAttack._attack.particlesOffset, (18f) * Time.deltaTime);
                activeAttack.particles[0].gameObject.SetActive(false); 
                break;
            case 2:
                activeAttack.particles[2].transform.position = Vector3.MoveTowards(activeAttack.particles[2].transform.position, scr_Grid.GridController.GetWorldLocation(activeAttack.pos) + activeAttack._attack.particlesOffset, (18f) * Time.deltaTime);
                activeAttack.particles[1].gameObject.SetActive(false);
                break;
            case 3:
                activeAttack.particles[2].gameObject.SetActive(false);
                break;
        }
    }

    public override void ImpactEffects(int xPos = -1, int yPos = -1)
    {

    }

    public override void EndEffects(ActiveAttack activeAttack)
    {
        Destroy(activeAttack.particles[0].gameObject);
        Destroy(activeAttack.particles[1].gameObject);
        Destroy(activeAttack.particles[2].gameObject);
    }
}
