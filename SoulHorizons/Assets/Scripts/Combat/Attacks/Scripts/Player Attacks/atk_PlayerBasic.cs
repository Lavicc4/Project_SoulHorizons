using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Attacks/PlayerBasic")]
public class atk_PlayerBasic : Attack {

    


    public override Vector2Int ProgressAttack(int xPos, int yPos, ActiveAttack activeAtk, SpriteRenderer activeParticle)
    {
        return LinearForward_ProgressAttack(xPos,yPos, activeAtk, activeParticle); 
    }

    Vector2Int LinearForward_ProgressAttack(int xPos, int yPos, ActiveAttack activeAtk, SpriteRenderer activeParticle)
    {
        //move particles
        //ProgressEffects(xPos, yPos, activeAtk.lastPos.x, activeAtk.lastPos.y, activeParticle);

        scr_Grid.GridController.PrimeNextTile(xPos + 1,yPos);
        scr_Grid.GridController.ActivateTile(xPos, yPos); 
        return new Vector2Int(xPos + 1, yPos); 
    }
    public override bool CheckCondition(scr_Entity _ent)
    {

        //return Random.Range(0, 100) < chanceToAttack;
       
            return true;
            //TODO charging attack 
        
        
    }

    public override void ProgressEffects(ActiveAttack activeAttack)
    {
        activeAttack.particle.transform.position = Vector3.Lerp(activeAttack.particle.transform.position, scr_Grid.GridController.GetWorldLocation(activeAttack.lastPos.x,activeAttack.lastPos.y) + activeAttack._attack.particlesOffset, (4.5f) * Time.deltaTime);
    }
}
