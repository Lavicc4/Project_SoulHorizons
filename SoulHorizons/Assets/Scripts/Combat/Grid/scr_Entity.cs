﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public enum EntityType
{
    Enemy,
    Player,
    Obstacle
}

[RequireComponent(typeof(AudioSource))]

public class scr_Entity : MonoBehaviour
{
    public EntityType type;

    public Vector2Int _gridPos = new Vector2Int();
    public Health _health = new Health();
    public scr_EntityAI _ai;
    public Territory entityTerritory;
    public SpriteRenderer spr;
    Color baseColor;
    public float lerpSpeed;
    public bool has_iframes;
    public bool invincible = false;
    public float invulnTime;
    float invulnCounter = 0f;

    AudioSource Hurt_SFX;
    AudioSource Die_SFX;
    public AudioClip[] hurts_SFX;
    private AudioClip hurt_SFX;
    public AudioClip[] dies_SFX;
    private AudioClip die_SFX;

    public void Start()
    {
        baseColor = spr.color;
        AudioSource[] SFX_Sources = GetComponents<AudioSource>();
        Hurt_SFX = SFX_Sources[2];
        Die_SFX = SFX_Sources[3];
        _health.max_hp = _health.hp;
    }
    public void Update()
    {
        if(gameObject.activeSelf)_ai.UpdateAI();
        if (_health.hp <= 0)
        {
            _ai.Die();
        }
        
        transform.position = Vector3.Lerp(transform.position, scr_Grid.GridController.GetWorldLocation(_gridPos.x, _gridPos.y), (lerpSpeed*Time.deltaTime));       
        //Counts down iframes
        if(invulnCounter > 0)
        {
            invulnCounter -= Time.deltaTime;
            if(invulnCounter <= 0)
            {
                setInvincible(false, 0f);
            }
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
        Attack atk = scr_AttackController.attackController.MoveIntoAttackCheck(_gridPos, this);
        if(atk != null)
        {
            if (!invincible)
            {
                Debug.Log("I'M HIT");
                HitByAttack(atk);
                if (has_iframes)
                {
                    //Activate invincibility frames
                    setInvincible(true, invulnTime);
                }
            }
        }
        
    }

    public void HitByAttack(Attack _attack)
    {

        /*
        if (_attack.territory.name != entityTerritory.name)
        {
            _health.TakeDamage(_attack.damage);
            StartCoroutine(HitClock(.5f));
        }
         */
        if (_attack.type != type)
        {
            int index = Random.Range(0, hurts_SFX.Length);
            hurt_SFX = hurts_SFX[index];
            Hurt_SFX.clip = hurt_SFX;
            Hurt_SFX.Play();

            _health.TakeDamage(_attack.damage);
            StartCoroutine(HitClock(.3f));
            if (type == EntityType.Player)
            {
                //camera shake
                CameraShaker.Instance.ShakeOnce(2f, 2f, 0.2f, 0.2f);
            }
        }

    }

    public bool isInvincible()
    {
        return invincible;
    }

    //makes the entity invincible for a time
    public void setInvincible(bool inv, float time)
    {
        invincible = inv;
        if (inv)
        {
            //Debug.Log("I'M INVINCIBLE");
            invulnCounter = time;
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

    public void Death()
    {
        /*
        int index = Random.Range(0, dies_SFX.Length);
        die_SFX = dies_SFX[index];
        Die_SFX.clip = die_SFX;
        Die_SFX.Play();
        */
        Debug.Log("I AM DEAD");
        scr_Grid.GridController.SetTileOccupied(false, _gridPos.x, _gridPos.y, this);
        gameObject.SetActive(false); 
        //scr_Grid.GridController.RemoveEntity(this);  
    }
   
    IEnumerator HitClock(float hitTime)
    {
        spr.color = Color.red;
        Debug.Log("I'M RED");
        yield return new WaitForSecondsRealtime(hitTime);
        spr.color = baseColor;
        Debug.Log("NOT RED");
    }

}
[System.Serializable]
public class Health{

    public int hp = 10;
    public int temp_hp = 0;
    public int max_hp;


    public void TakeDamage(int damage)
    {
        if (temp_hp > 0)
        {
            temp_hp -= damage;
            if(temp_hp < 0)
            {
                //Carry over extra damage to normal hp
                hp += temp_hp;
                temp_hp = 0;
            }
        }
        else
        {
            hp -= damage;
        }
        if (hp <= 0)
        {
            hp = 0;
            
        }
        Debug.Log("MY HP: " + hp);

    }

}





    


