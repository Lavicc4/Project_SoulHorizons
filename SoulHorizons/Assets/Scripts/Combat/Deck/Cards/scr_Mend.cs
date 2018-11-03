using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Cards/Mend")]
public class scr_Mend : scr_Card
{

    public int Mend_hp;
    public override void Activate()
    {
        ActivateEffects();

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