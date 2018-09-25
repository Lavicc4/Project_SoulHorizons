﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Entity : MonoBehaviour
{

    public Vector2Int _gridPos = new Vector2Int();
    public Health _health = new Health();
    public scr_EntityAI _ai;
    public scr_Tile.Territory entityTerritory;
    public SpriteRenderer spr;
    public GameObject manager_object;
    public scr_StateManager manager;

    public void Start()
    {
        manager_object = GameObject.Find("StateManager");
        if(manager_object != null)
        {
            manager = manager_object.GetComponent<scr_StateManager>();
        }
        else
        {
            Debug.Log("MANAGER OBJECT COULD NOT BE FOUND");
        }
    }
    public void Update()
    {
        _ai.UpdateAI();
    }


    public void InitPosition(int x, int y)
    {

        //scr_Grid.GridController.SetTileOccupied(false, x, y);
        _gridPos = new Vector2Int(x, y);
        transform.position = new Vector3(scr_Grid.GridController.grid[x, y].transform.position.x, scr_Grid.GridController.grid[x, y].transform.position.y, 0);
        scr_Grid.GridController.SetTileOccupied(true, x, y, this);
    }

    public void SetTransform(int x, int y)
    {
        if (_gridPos == new Vector2Int(x, y))                                                                                                          //if we set transform, and we havent moved
            return;                                                                                                                                    //return

        scr_Grid.GridController.SetTileOccupied(false, _gridPos.x, _gridPos.y, this);
        _gridPos = new Vector2Int(x, y);
        transform.position = new Vector3(scr_Grid.GridController.grid[x, y].transform.position.x, scr_Grid.GridController.grid[x, y].transform.position.y, 0);                    //move to the new space 
        scr_Grid.GridController.SetTileOccupied(true, _gridPos.x, _gridPos.y,this);
        spr.sortingOrder = -_gridPos.y;
        Attack atk = scr_AttackController.attackController.MoveIntoAttackCheck(_gridPos);
        if(atk != null)
        {
            HitByAttack(atk); 
        }
        
    }

    public void HitByAttack(Attack _attack)
    {
        if(_attack.territory != entityTerritory)
        {
            _health.TakeDamage(_attack.damage);
            manager.UpdateHealth(_health.hp);
        }
    }
}
[System.Serializable]
public class Health{

    public int hp = 10; 

    public void TakeDamage(int damage)
    {
        hp -= damage;
    }
}





    


