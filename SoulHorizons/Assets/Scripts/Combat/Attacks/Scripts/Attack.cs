using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[System.Serializable]
public abstract class Attack : ScriptableObject {

    public float incrementSpeed;
    public int maxIncrements = 1;
    public int damage;
    [Header("Where the attack is coming from")]
    public scr_Tile.Territory territory;
    public bool piercing;
    public SpriteRenderer particles;
    public Vector3 particlesOffset; 

	public virtual Vector2Int BeginAttack()
    {
        return new Vector2Int(); 
    }
    public virtual Vector2Int ProgressAttack(int xPos, int yPos, ActiveAttack activeAtk)
    {
        return new Vector2Int(); 
    }
    public virtual bool CheckCondition(scr_Entity _ent)
    {
        return false; 
    }

    /// <summary>
    /// This is called whenever the attack hits something.
    /// Use to launch particles and/or sounds.
    /// </summary>
    public virtual void ImpactEffects(int xPos = -1, int yPos = -1)
    {
        
    }

    /// <summary>
    /// Moves the particles however they are supposed to move. Called in ProgressAttack
    /// </summary>
    public abstract void ProgressEffects(ActiveAttack activeAttack);
}
