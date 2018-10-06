using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_ExiledArcher : scr_EntityAI {


    public Attack hunterShot;
    public float hSChargeTime;
    private bool hSOnCD = false;   //On Cooldown 
    private float hSCooldownTime = 3f; 



    public Attack arrowRain;
    public float aRInterval;
    private bool canArrowRain = true;



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
            StartCoroutine(HunterShot());
        }
        if (canArrowRain)
        {
            StartCoroutine(ArrowRain(aRInterval)); 
        }
    }

    public override void Die()
    {
        Debug.Log("ARGHH");
        entity.Death();
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
        hSOnCD = true;
        yield return new WaitForSecondsRealtime(hSChargeTime);
        scr_AttackController.attackController.AddNewAttack(hunterShot, entity._gridPos.x, entity._gridPos.y, entity);
        yield return new WaitForSecondsRealtime(hSCooldownTime);
        hSOnCD = false; 
    }

    private IEnumerator ArrowRain(float _aRInterval)
    {
        //TELEGRAPH 
        canArrowRain = false; 
        yield return new WaitForSecondsRealtime(1f);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        int playerXPos = player.GetComponent<scr_Entity>()._gridPos.x;
        scr_AttackController.attackController.AddNewAttack(arrowRain, playerXPos, 2 /*top of grid */, entity);
        yield return new WaitForSecondsRealtime(_aRInterval);
        canArrowRain = true; 
    }
    
}
