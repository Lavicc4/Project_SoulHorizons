using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Cards/LightningBolt")]
public class scr_LightningBolt : scr_Card {

    public float damage = 6f;

    public override void Activate()
    {
        ActivateEffects();

        //implement functionality here
        Debug.Log(name + ": Zap!");
    }

    public override void StartCastingEffects()
    {
        
    }

    protected override void ActivateEffects()
    {
        //put start effects here
    }
}
