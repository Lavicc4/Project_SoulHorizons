﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Cards/Mend")]
[RequireComponent(typeof(AudioSource))]

public class scr_Mend : scr_Card
{

    public int Mend_hp;
    private AudioSource PlayCardSFX;
    public AudioClip MendSFX;
    public override void Activate()
    {
        ActivateEffects();
        PlayCardSFX = GameObject.Find("DeckManager").GetComponent<AudioSource>();
        PlayCardSFX.clip = MendSFX;
        PlayCardSFX.Play();
        scr_Entity player = GameObject.FindGameObjectWithTag("Player").GetComponent<scr_Entity>();
        player._health.hp += Mend_hp;
        if(player._health.hp > player._health.max_hp)
        {
            player._health.hp = player._health.max_hp;
        }

    }

    public override void StartCastingEffects()
    {

    }

    protected override void ActivateEffects()
    {
        //put start effects here
    }
}