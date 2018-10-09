using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/InnerStrength")]
public class scr_InnerStrength : scr_Card {


    public override void Activate()
    {
        ActivateEffects();

        scr_Entity player = GameObject.FindGameObjectWithTag("Player").GetComponent<scr_Entity>();
        

    }

    public override void StartCastingEffects()
    {

    }

    protected override void ActivateEffects()
    {
        //put start effects here
    }
}
