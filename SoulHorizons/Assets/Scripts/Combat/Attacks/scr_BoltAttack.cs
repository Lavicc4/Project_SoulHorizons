using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_BoltAttack : Attack {
	public override Vector2Int ProgressAttack(int xPos, int yPos, ActiveAttack activeAtk, SpriteRenderer activeParticle)
    {
		//update the effects
		ProgressEffects(xPos, yPos, activeParticle);

		//check if the Bolt hit an obstacle
		if (activeAtk.entityHit.type == EntityType.Obstacle)
		{
			//set max increments to -1 to make it stop
			maxIncrements = -1;

			//launch a particle at this point?
		}

		//move in a straight line to the right
        return new Vector2Int(xPos+ 1, yPos); 
    }

	    public override void ProgressEffects(int xPos, int yPos, SpriteRenderer activeParticle)
    {
        activeParticle.transform.position = Vector3.Lerp(activeParticle.transform.position, scr_Grid.GridController.GetWorldLocation(xPos, yPos) + particlesOffset, (4.5f) * Time.deltaTime);
    }
}
