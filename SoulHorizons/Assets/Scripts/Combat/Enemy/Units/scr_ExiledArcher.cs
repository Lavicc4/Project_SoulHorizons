using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_ExiledArcher : scr_EntityAI {


    public Attack hunterShot;
    public float hSChargeTime;
    private bool hSOnCD = false;   //On COoldown 
    private float hSCooldownTime = 3f; 



    public Attack arrowRain;
    public float aRChargeTime;



    public override void Move()
    {
        
    }
    public override void Attack()
    {
        
    }
    public override void UpdateAI()
    {
        if(!hSOnCD  && HunterShotCheck())
        {
            hSOnCD = true;
            StartCoroutine(HunterShot());
        }
    }

    bool HunterShotCheck()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        int playerY = player.GetComponent<scr_Entity>()._gridPos.y;
        if(entity._gridPos.y == playerY)
        {
            return true;
        }
        return false; 
    }

    private IEnumerator HunterShot()
    {
        yield return new WaitForSecondsRealtime(hSChargeTime);
        scr_AttackController.attackController.AddNewAttack(hunterShot, entity._gridPos.x, entity._gridPos.y, entity);
        yield return new WaitForSecondsRealtime(hSCooldownTime);
        hSOnCD = false; 
    }
    
}
