using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "SoulTransform")]
public class SoulTransform : ScriptableObject {

    public Element element;
    //script references
    public MonoScript basicAttack;
    public MonoScript movement;

    //List<Monobehavior> misc = new List<MonoBehavior<()

    //common attributes
    [Tooltip("The percentage of max hp added to the shield")]
    [SerializeField] int shieldGain; //the amount of shield to add when performing this transform (Percentage of max health?)
    [Tooltip("Shield loss per second")]
    [SerializeField] int shieldDrainRate; //the shield loss per second that this transform inflicts

    public int GetShieldGain()
    {
        return shieldGain;
    }

    public int GetShieldDrainRate()
    {
        return shieldDrainRate;
    }
}
