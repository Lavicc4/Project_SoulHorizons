using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulTransform : ScriptableObject {

    //script references
    MonoBehaviour movement;
    MonoBehaviour basicAttack;

    //common attributes
    int shield; //the amount of shield to add when performing this transform
    int shieldDrainRate; //the shield loss per second that this transform inflicts
}
