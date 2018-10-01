using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class scr_EnemyAI_1 : scr_EntityAI {

    public float chargeTime; 
    public float movementIntervalLower;
    public float movementIntervalUpper;
    public float chargeAttackChance; 
    bool waiting = false;
    public Attack attack1;
    public Attack chargedAttack; 

    AudioSource Attack_SFX;
    AudioSource Footsteps_SFX;
    public AudioClip[] movements_SFX;
    private AudioClip movement_SFX;
    public AudioClip[] attacks_SFX;
    private AudioClip attack_SFX;
    private bool canMove = true;
    private bool charging = false; 


    public override void Move()
    {

    }
    public override void Attack()
    {
        
    }
    public override void UpdateAI()
    {
        float _movementInterval = Random.Range(movementIntervalLower, movementIntervalUpper);
        StartCoroutine(MovementClock(_movementInterval));
        
    }

    void Movement()
    {
        AudioSource[] SFX_Sources = GetComponents<AudioSource>();
        Footsteps_SFX = SFX_Sources[0];
        Attack_SFX = SFX_Sources[1];
        int index = Random.Range(0, movements_SFX.Length);
        movement_SFX = movements_SFX[index];
        Footsteps_SFX.clip = movement_SFX;
        Footsteps_SFX.Play();

        //Decide if we are moving horiz or vert.
        int _temp = Random.Range(0, 2);                                         //Pick a number between 0 and 1
        int _x = entity._gridPos.x;
        int _y = entity._gridPos.y; 
        if (_temp == 0)                                                          //if that number == 0, then we're moving vertically 
        {
            _y = PickYCoord();

        }
        else if (_temp == 1)                                                     //if that number == 1, we're moving horizonally 
        {
            _x = PickXCoord();

        }
        if (!scr_Grid.GridController.CheckIfOccupied(_x, _y))
        {
            if (canMove)
            {
                entity.SetTransform(_x, _y);
            }

            float attackTypeCheck = Random.Range(0f, 100f); 
            if (attackTypeCheck <= chargeAttackChance)
            {
                if (chargedAttack.CheckCondition(entity))
                {
                    if (!charging)
                    {
                        Debug.Log("Charged Attack Start");
                        StartCoroutine(ChargingClock(chargeTime));
                    }
                     
                    
                }

            }
            else
            {
                if (attack1.CheckCondition(entity))
                {
                    scr_AttackController.attackController.AddNewAttack(attack1, entity._gridPos.x, entity._gridPos.y, entity);

                    int index2 = Random.Range(0, attacks_SFX.Length);
                    attack_SFX = attacks_SFX[index2];
                    Attack_SFX.clip = attack_SFX;
                    Attack_SFX.Play();
                }
            }
        }
        else
        {
            Movement();
            
        }
    }
    IEnumerator MovementClock(float _movementInterval)
    {
        if (!waiting)                                                           //Checking to see if we have started waiting, if not
        {
            waiting = true;                                                     //start waiting
            yield return new WaitForSecondsRealtime(_movementInterval);         //wait for x seconds
            Movement();                                                         //move
            waiting = false;                                                    //not waiting anymore 

        }

    }
    IEnumerator ChargingClock(float _chargeTime)
    {
        if (canMove  && !charging)                                                           //Checking to see if we have started waiting, if not
        {
            charging = true; 
            canMove = false;
            yield return new WaitForSecondsRealtime(_chargeTime);               //wait for x seconds
            Debug.Log("Charge Attack End");
            scr_AttackController.attackController.AddNewAttack(chargedAttack, entity._gridPos.x, entity._gridPos.y, entity);                                                  //not waiting anymore 
            canMove = true;
            charging = false; 
        }

    }


    //Disclaimer, this AI will not move to the first row (x = 0) all of the movement is randomly done based on the current position of the AI, it needs to be streamlined 
    //I know this 



    int PickXCoord()
    {
        if (entity._gridPos.x == 4)                              //AI is on x = 0, so we need to move to 1 (right)                                         
        {
            return 5;

        }
        else if (entity._gridPos.x == 5)                         //AI is on x = 2, so we need to move to 1  (left) 
        {
            return 4;
        }
        else                                            //Should never reach this state, but as a Debug, the AI will move to x = 0
        {
            return 0;
        }

    }

    int PickYCoord()
    {
        if (entity._gridPos.y == 0)                             //AI is on y = 0 and can only move to 1 (down)                             
        {
            return 1;
        }
        else if (entity._gridPos.y == 1)                        //AI is on y = 1 and can move either up or down
        {
            int _temp = Random.Range(0, 2);             //make a random number 0 or 1
            if (_temp == 0)                              //if this number is 0, move to 0 (up)
            {
                return 0;
            }
            else                                        //if this number is 1, move to 1 (down) 
            {
                return 2;
            }
        }
        else                                            //otherwise, the AI is on 2 and can only move to 1 (up)
        {
            return 1;
        }
    }

    
}
