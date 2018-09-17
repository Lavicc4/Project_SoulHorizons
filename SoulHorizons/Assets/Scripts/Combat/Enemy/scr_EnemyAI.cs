using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_EnemyAI : MonoBehaviour {

    //Cameron Made this 


    struct Coords
    {
        public int x;
        public int y;

        public Coords(int a, int b)
        {
            x = a;
            y = b;
        }

    }


    

    [SerializeField]
    float movementInterval;

    int spawnX = 1;
    int spawnY = 1;
    scr_Grid grid;
    GameObject enemyGridController;
    Coords gridpos;
    bool waiting = false; 

    void Start () {

        enemyGridController = GameObject.FindGameObjectWithTag("EnemyGridController");
        grid = enemyGridController.GetComponent<scr_Grid>();

        gridpos = new Coords(spawnX, spawnY);
        transform.position = new Vector3(grid.grid[spawnX, spawnY].transform.position.x, grid.grid[spawnX, spawnY].transform.position.y, 0);

    }
	
	
	void Update () {

        StartCoroutine(MovementClock(movementInterval));
        
	}




    void Attack1()
    {


    }

    void Attack2()
    {

    }

    void Movement()
    {                                                            
                                                                                //Decide if we are moving horiz or vert.
        int _temp = Random.Range(0, 2);                                         //Pick a number between 0 and 1
        if(_temp == 0)                                                          //if that number == 0, then we're moving vertically 
        {
            int _y = PickYCoord();
            SetTransform(gridpos.x, _y);                                        //set the transform for the enemy
            gridpos.y = _y;                                                     //associate the new position on the grid
            return; 
        }
        else if(_temp == 1)                                                     //if that number == 1, we're moving horizonally 
        {
            int _x = PickXCoord();
            SetTransform(_x, gridpos.y);                                        //set the transform for the enemy
            gridpos.x = _x;                                                     //associate the new position on the grid
            return; 
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
    

    //Disclaimer, this AI will not move to the first row (x = 0) all of the movement is randomly done based on the current position of the AI, it needs to be streamlined 
    //I know this 


    int PickXCoord()    
    {
        if(gridpos.x == 0)                              //AI is on x = 0, so we need to move to 1 (right)                                         
        {
            return 1;
             
        }
        else if(gridpos.x == 1)                         //AI is on x = 2, so we need to move to 1  (left) 
        {
            return 2; 
        }
        else                                            //Should never reach this state, but as a Debug, the AI will move to x = 0
        {
            return 1; 
        }
        
    }

    int PickYCoord()
    {
        if (gridpos.y == 0)                             //AI is on y = 0 and can only move to 1 (down)                             
        {
            return 1;
        }
        else if (gridpos.y == 1)                        //AI is on y = 1 and can move either up or down
        {   
            int _temp = Random.Range(0, 2);             //make a random number 0 or 1
            if(_temp == 0)                              //if this number is 0, move to 0 (up)
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

    void SetTransform(int x, int y)
    {
        transform.position = new Vector3(grid.grid[x, y].transform.position.x, grid.grid[x, y].transform.position.y, 0);
    }

    void Debugging()
    {
        Debug.Log(gridpos.x + " , " + gridpos.y); 

    }


}
