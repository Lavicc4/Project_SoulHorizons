using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Critter : scr_EntityAI {

    public float movementInterval;
    bool waiting = false;
    bool moving = false; 
    

    public override void Move()
    {

        

    }
    public override void Attack()
    {
        
    }
    public override void UpdateAI()
    {
        /*
        int _x = entity._gridPos.x;
        int _y = entity._gridPos.y;
        scr_Grid.GridController.SetTileOccupied(true, _x, _y, entity);
        */
        if (!moving)
        {
            moving = true;
            StartCoroutine(Movement(movementInterval));
        }
         
    }

    public override void Die()
    {
        Debug.Log("ARGHH");
        entity.Death();
    }




    int GenerateCoord(int lowerLim,int upperLim)
    {
        int _x = Random.Range(lowerLim, upperLim);
        return _x; 
    }





    private IEnumerator Movement(float _movementInterval)
    {
        int _x = GenerateCoord(3, 6);
        int _y = GenerateCoord(0, 3);
        if(_x == entity._gridPos.x  &&  _y == entity._gridPos.y)
        {
            StartCoroutine(Movement(movementInterval));
            yield break; 
        }
        else
        {
            
            yield return new WaitForSecondsRealtime(_movementInterval);
            if (!scr_Grid.GridController.CheckIfOccupied(_x, _y))
            {
                scr_Grid.GridController.SetTileOccupied(true, _x, _y, entity);
                entity.SetTransform(_x, _y);
                waiting = false;
                moving = false;
            }
            else
            {
                StartCoroutine(Movement(movementInterval));
                Debug.Log("hit"); 
                yield break; 
            }

                 
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
