using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_AttackController : MonoBehaviour {

    public ActiveAttack[] activeAttacks = new ActiveAttack[10];
    public int numberOfActiveAttacks = 0; 
    public static scr_AttackController attackController;

    private void Awake()
    {
        attackController = this; 
    }

    public void AddNewAttack(Attack _attack,int xPos, int yPos)
    {

        activeAttacks[numberOfActiveAttacks] = new ActiveAttack(_attack, xPos, yPos);
        numberOfActiveAttacks++; 
    }

    void Update()
    {
        for(int x = 0; x < numberOfActiveAttacks; x++)
        {
            if (activeAttacks[x].CanAttackContinue())
            {
                if (activeAttacks[x].currentIncrement > activeAttacks[x]._attack.maxIncrements)
                {
                    RemoveFromArray(x);
                    return;
                }
                else if (scr_Grid.GridController.LocationOnGrid(activeAttacks[x].pos.x, activeAttacks[x].pos.y) == false)
                {
                    RemoveFromArray(x);
                    return;
                }
                if (activeAttacks[x].currentIncrement != 0)
                    scr_Grid.GridController.DeactivateTile(activeAttacks[x].lastPos.x, activeAttacks[x].lastPos.y);
                activeAttacks[x].lastPos = activeAttacks[x].pos;
                scr_Grid.GridController.AttackPosition(activeAttacks[x].pos.x, activeAttacks[x].pos.y, activeAttacks[x]._attack);
                activeAttacks[x].pos = activeAttacks[x]._attack.ProgressAttack(activeAttacks[x].pos.x, activeAttacks[x].pos.y);
                activeAttacks[x].lastAttackTime = Time.time; 
                activeAttacks[x].currentIncrement++;
                 
            }
        }    
    }
    void RemoveFromArray(int index)
    {
        scr_Grid.GridController.DeactivateTile(activeAttacks[index].lastPos.x, activeAttacks[index].lastPos.y);
        scr_Grid.GridController.DeactivateTile(activeAttacks[index].pos.x, activeAttacks[index].pos.y);
        for (int x = index; x < numberOfActiveAttacks; x++)
        {
            if (x + 1 < activeAttacks.Length && activeAttacks[x + 1]._attack != null)
            {
                activeAttacks[x].Clone(activeAttacks[x + 1]);
            }
            else
            {
                activeAttacks[x] = new ActiveAttack();
            }
        }
        numberOfActiveAttacks--; 
    }

    public Attack AttackType(Vector2Int pos)
    {
        for (int x = 0; x < numberOfActiveAttacks; x++)
        {
            if (activeAttacks[x].pos == pos)
            {
                return activeAttacks[x]._attack;
            }
        }
        
        return null; 
        
    }

    public Attack MoveIntoAttackCheck(Vector2Int pos)
    {
        for (int x = 0; x < numberOfActiveAttacks; x++)
        {
            if (activeAttacks[x].lastPos == pos)
            {
                Attack atk = activeAttacks[x]._attack;
                Debug.Log("REMOVING");
                RemoveFromArray(x);
                return atk;
            }
        }

        return null;
    }

}

[System.Serializable]
public class ActiveAttack
{

    public Attack _attack;
    public Vector2Int pos;
    public Vector2Int lastPos;
    public float lastAttackTime;
    public int currentIncrement = 0;
     
    
    public ActiveAttack(Attack atk, int x, int y)
    {
        _attack = atk;
        pos.x = x;
        pos.y = y; 
        
        lastAttackTime = Time.time;
    }
    public ActiveAttack()
    {
        _attack = null;
        pos = new Vector2Int();
        lastAttackTime = 0;
        currentIncrement = 0; 
    }

    public bool CanAttackContinue()
    {
        if(lastAttackTime + _attack.incrementSpeed <= Time.time)
        {
            return true; 
        }
        return false; 
    }
    public void Clone(ActiveAttack atk)
    {
        _attack = atk._attack;
        pos = atk.pos;
        lastAttackTime = atk.lastAttackTime;
        currentIncrement = atk.currentIncrement;
        lastPos = atk.lastPos; 
        
    }

    
   
}
