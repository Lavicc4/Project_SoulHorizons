﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Bolt")]
public class scr_BoltAttack : Attack {
	public override Vector2Int ProgressAttack(int xPos, int yPos, ActiveAttack activeAtk)
    {
		Debug.Log((activeAtk == null) ? "Null attack" : "Everything is fine with Active Attack");
		//update the effects
		//ProgressEffects(xPos, yPos, activeAtk.lastPos.x, activeAtk.lastPos.y, activeParticle);

		//check if the Bolt hit an obstacle
		if (activeAtk.entityIsHit && activeAtk.entityHit.type == EntityType.Obstacle)
		{
			//set max increments to -1 to make it stop
			maxIncrements = -1;

			//launch a particle at this point?
		}

		//move in a straight line to the right
        return new Vector2Int(xPos+ 1, yPos); 
    }

    //--Effects Methods--
    public override void LaunchEffects(ActiveAttack activeAttack)
    {
        activeAttack.particle = Instantiate(particles, scr_Grid.GridController.GetWorldLocation(activeAttack.pos.x, activeAttack.pos.y) + particlesOffset, Quaternion.identity);
        activeAttack.particle.sortingOrder = -activeAttack.pos.y;
    }

    public override void ProgressEffects(ActiveAttack activeAttack)
    {
        activeAttack.particle.transform.position = Vector3.Lerp(activeAttack.particle.transform.position, scr_Grid.GridController.GetWorldLocation(activeAttack.lastPos.x,activeAttack.lastPos.y) + activeAttack._attack.particlesOffset, (4.5f) * Time.deltaTime);
    }

	public override void ImpactEffects(int xPos = -1, int yPos = -1)
    {
		Debug.Log("Bolt Impact Effects!!!");
		
    }

    public override void EndEffects(ActiveAttack activeAttack)
    {
        
    }
}
