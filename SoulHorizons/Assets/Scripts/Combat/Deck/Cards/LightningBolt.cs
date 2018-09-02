using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Cards/LightningBolt")]
public class LightningBolt : Card {

    public float damage = 6f;

    public override void Activate()
    {
        //implement functionality here
        Debug.Log("Zap!");
    }
}
