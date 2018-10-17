using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Cards/Boomerang")]
public class scr_Boomerang : scr_Card
{

    public Attack boomerangAttack;

    public override void Activate()
    {

        ActivateEffects();

        scr_Entity player = GameObject.FindGameObjectWithTag("Player").GetComponent<scr_Entity>();

        //add attack to attack controller script
        //does a check to see if the target col is off the map 
        scr_AttackController.attackController.AddNewAttack(boomerangAttack,0,0, player);
    }

    public override void StartCastingEffects()
    {

    }

    protected override void ActivateEffects()
    {
        //put start effects here
    }
}
