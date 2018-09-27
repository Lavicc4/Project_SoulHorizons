using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Entity : MonoBehaviour
{

    public Vector2Int _gridPos = new Vector2Int();
    public Health _health = new Health();
    public scr_EntityAI _ai;
    public scr_Tile.Territory entityTerritory;
    public SpriteRenderer spr;
    public float lerpSpeed; 

    public void Start()
    {
      
    }
    public void Update()
    {
        _ai.UpdateAI();
        transform.position = Vector3.Lerp(transform.position, scr_Grid.GridController.GetWorldLocation(_gridPos.x, _gridPos.y), (lerpSpeed*Time.deltaTime));              
    }


    public void InitPosition(int x, int y)
    {

        //scr_Grid.GridController.SetTileOccupied(false, x, y);
        _gridPos = new Vector2Int(x, y);
        transform.position = scr_Grid.GridController.GetWorldLocation(_gridPos.x, _gridPos.y); 
        scr_Grid.GridController.SetTileOccupied(true, x, y, this);
    }

    //Tells entity to move to new coordinates
    public void SetTransform(int x, int y)
    {
        if (_gridPos == new Vector2Int(x, y))                                                                                                          //if we set transform, and we havent moved
            return;                                                                                                                                    //return

        scr_Grid.GridController.SetTileOccupied(false, _gridPos.x, _gridPos.y, this);
        _gridPos = new Vector2Int(x, y);
        
        scr_Grid.GridController.SetTileOccupied(true, _gridPos.x, _gridPos.y,this);
        spr.sortingOrder = -_gridPos.y;
        Attack atk = scr_AttackController.attackController.MoveIntoAttackCheck(_gridPos);
        if(atk != null)
        {
            //Debug.Log("I'M HIT");
            HitByAttack(atk); 
        }
        
    }

    public void HitByAttack(Attack _attack)
    {
        if(_attack.territory != entityTerritory)
        {
            _health.TakeDamage(_attack.damage);
          
        }
    }
}
[System.Serializable]
public class Health{

    public int hp = 10; 

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            hp = 0;
        }
    }
}





    


