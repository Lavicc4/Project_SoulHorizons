using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Attacks/ChargedForward")]
public class atk_ChargedForward : Attack {

    public override Vector2Int ProgressAttack(int xPos, int yPos, ActiveAttack activeAtk, SpriteRenderer activeParticle)
    {
        return LinearForward_ProgressAttack(xPos, yPos, activeAtk, activeParticle);
    }

    Vector2Int LinearForward_ProgressAttack(int xPos, int yPos, ActiveAttack activeAtk, SpriteRenderer activeParticle)
    {
        scr_Grid.GridController.PrimeNextTile(xPos - 1, yPos);
        scr_Grid.GridController.ActivateTile(xPos, yPos);
        return new Vector2Int(xPos - 1, yPos);
    }
    public override bool CheckCondition(scr_Entity _ent)
    {
        return true;
    }


    public override void ProgressEffects(ActiveAttack activeAttack)
    {
        activeAttack.particle.transform.position = Vector3.Lerp(activeAttack.particle.transform.position, scr_Grid.GridController.GetWorldLocation(activeAttack.lastPos.x, activeAttack.lastPos.y) + activeAttack._attack.particlesOffset, (4.5f) * Time.deltaTime);
    }
}
