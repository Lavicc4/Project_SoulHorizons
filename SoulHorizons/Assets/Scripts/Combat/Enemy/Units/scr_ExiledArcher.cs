using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class scr_ExiledArcher : scr_EntityAI {


    public Attack hunterShot;
    public float hSChargeTime;
    private bool hSOnCD = false;   //On Cooldown 
    private float hSCooldownTime = 1.5f; 



    public Attack arrowRain;
    public float aRInterval;
    private bool canArrowRain = true;

    AudioSource Attack_SFX;
    public AudioClip[] attacks_SFX;
    private AudioClip attack_SFX;

    private void Start()
    {
        AudioSource[] SFX_Sources = GetComponents<AudioSource>();
        Attack_SFX = SFX_Sources[1];
    }

    public override void Move()
    {
        
    }
    public override void Attack()
    {
        
    }
    public override void UpdateAI()
    {
        scr_Grid.GridController.SetTileOccupied(true, entity._gridPos.x, entity._gridPos.y, this.entity); 
        if(!hSOnCD  && HunterShotCheck())
        {
            StartCoroutine(HunterShot());
        }

        /*
        if (canArrowRain)
        {
            StartCoroutine(ArrowRain(aRInterval)); 
        }
        */

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
        int index = Random.Range(0, attacks_SFX.Length);
        attack_SFX = attacks_SFX[index];
        Attack_SFX.clip = attack_SFX;
        Attack_SFX.Play();
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
        scr_AttackController.attackController.AddNewAttack(arrowRain, playerXPos, scr_Grid.GridController.ySizeMax - 1, entity);
        yield return new WaitForSecondsRealtime(_aRInterval);
        canArrowRain = true; 
    }
    
}
