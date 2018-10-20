﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Cards/Meteor")]
public class scr_Meteor : scr_Card {

    public Attack meteorAttack;

    public override void Activate()
    {
        
        ActivateEffects();

        scr_Entity player = GameObject.FindGameObjectWithTag("Player").GetComponent<scr_Entity>();

        //add attack to attack controller script
        //does a check to see if the target col is off the map 
        scr_AttackController.attackController.AddNewAttack(meteorAttack, Mathf.Min(player._gridPos.x + 3,(scr_Grid.GridController.xSizeMax-1)), 1, player);
    }

    public override void StartCastingEffects()
    {

    }

    protected override void ActivateEffects()
    {
        //put start effects here
    }
}
