using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Attacks/PlayerBlaster")]
public class atk_PlayerBlaster : Attack {

    


    public override Vector2Int ProgressAttack(int xPos, int yPos, ActiveAttack activeAtk, SpriteRenderer activeParticle)
    {
        return LinearForward_ProgressAttack(xPos, yPos, activeAtk, activeParticle);
    }

    Vector2Int LinearForward_ProgressAttack(int xPos, int yPos, ActiveAttack activeAtk, SpriteRenderer activeParticle)
    {
        //move particles
        //ProgressEffects(xPos, yPos, activeAtk.lastPos.x, activeAtk.lastPos.y, activeParticle);

        scr_Grid.GridController.PrimeNextTile(xPos + 1, yPos);
        scr_Grid.GridController.ActivateTile(xPos, yPos);
        return new Vector2Int(xPos + 1, yPos);
    }
    public override bool CheckCondition(scr_Entity _ent)
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
        return true; // to make it happy 
    }

    public override void ProgressEffects(ActiveAttack activeAttack)
    {
        activeAttack.particle.transform.position = Vector3.Lerp(activeAttack.particle.transform.position, scr_Grid.GridController.GetWorldLocation(activeAttack.lastPos.x,activeAttack.lastPos.y) + activeAttack._attack.particlesOffset, (4.5f) * Time.deltaTime);
    }
}
