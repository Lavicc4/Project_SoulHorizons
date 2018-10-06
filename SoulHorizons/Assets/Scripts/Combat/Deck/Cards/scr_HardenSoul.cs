using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/HardenSoul")]
public class scr_HardenSoul : scr_Card {

    public int Shield_hp;
    public override void Activate()
    {
        ActivateEffects();

        scr_Entity player = GameObject.FindGameObjectWithTag("Player").GetComponent<scr_Entity>();
        player._health.temp_hp += Shield_hp;
        
    }

    public override void StartCastingEffects()
    {

    }

    protected override void ActivateEffects()
    {
        //put start effects here
    }
}
