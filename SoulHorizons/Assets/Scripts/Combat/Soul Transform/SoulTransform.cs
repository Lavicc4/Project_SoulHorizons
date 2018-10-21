using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulTransform : ScriptableObject {

    //script references
    public MonoBehaviour movement;
    public MonoBehaviour basicAttack;

    //common attributes
    [SerializeField] int shield; //the amount of shield to add when performing this transform
    [SerializeField] int shieldDrainRate; //the shield loss per second that this transform inflicts

    public int getShield()
    {
        return shield;
    }

    public int getShieldDrainRate()
    {
        return shieldDrainRate;
    }
}
