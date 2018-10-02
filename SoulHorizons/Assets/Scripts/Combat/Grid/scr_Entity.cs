using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType
{
    Enemy,
    Player,
    Obstacle
}

public class scr_Entity : MonoBehaviour
{
    public EntityType type;

    public Vector2Int _gridPos = new Vector2Int();
    public Health _health = new Health();
    public scr_EntityAI _ai;
    public scr_Tile.Territory entityTerritory;
    public SpriteRenderer spr;
    Color baseColor;
    public float lerpSpeed;
    public bool has_iframes;
    bool invincible = false;
    public float invulnTime;
    float invulnCounter = 0f;

    public void Start()
    {
        baseColor = spr.color;
    }
    public void Update()
    {
        _ai.UpdateAI();
        transform.position = Vector3.Lerp(transform.position, scr_Grid.GridController.GetWorldLocation(_gridPos.x, _gridPos.y), (lerpSpeed*Time.deltaTime));       
        //Counts down iframes
        if(invulnCounter > 0)
        {
            invulnCounter -= Time.deltaTime;
        }
        else
        {
            setInvincible(false);
        }
    }


    public void InitPosition(int x, int y)
    {

        //scr_Grid.GridController.SetTileOccupied(false, x, y);
        _gridPos = new Vector2Int(x, y);
        transform.position = scr_Grid.GridController.GetWorldLocation(_gridPos.x, _gridPos.y); 
        scr_Grid.GridController.SetTileOccupied(true, x, y, this);
        spr.sortingOrder = -_gridPos.y;
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
            if (!invincible)
            {
                Debug.Log("I'M HIT");
                HitByAttack(atk);
                if (has_iframes)
                {
                    //Activate invincibility frames
                    setInvincible(true);
                }
            }
        }
        
    }

    public void HitByAttack(Attack _attack)
    {
        if(_attack.territory != entityTerritory)
        {
            _health.TakeDamage(_attack.damage);
          
        }
    }

    public bool isInvincible()
    {
        return invincible;
    }

    public void setInvincible(bool inv)
    {
        invincible = inv;
        if (inv)
        {
            //Debug.Log("I'M INVINCIBLE");
            invulnCounter = invulnTime;
            spr.color = Color.gray;
        }
        else
        {
            //Debug.Log("NOT INVINCIBLE");
            invulnCounter = 0f;
            invincible = false;
            spr.color = baseColor;
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





    


