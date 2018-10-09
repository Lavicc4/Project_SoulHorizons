using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Cards/InnerStrength")]
public class scr_InnerStrength : scr_Card {

    public float multiplier;
    public float duration;

    public override void Activate()
    {
        ActivateEffects();

        scr_PlayerBlaster blaster = GameObject.FindGameObjectWithTag("Player").GetComponent<scr_PlayerBlaster>();
        scr_statemanager manager = GameObject.FindGameObjectWithTag("StateManager").GetComponent<scr_statemanager>();
        blaster.setMultiplier(multiplier, duration);
        manager.ChangeEffects("Attack Up: " + multiplier, duration);
    }

    public override void StartCastingEffects()
    {

    }

    protected override void ActivateEffects()
    {
        //put start effects here
    }
}
