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

    void Start () {

        enemyGridController = GameObject.FindGameObjectWithTag("EnemyGridController");
        grid = enemyGridController.GetComponent<scr_Grid>();

        gridpos = new Coords(spawnX, spawnY);
        transform.position = new Vector3(grid.grid[spawnX, spawnY].transform.position.x, grid.grid[spawnX, spawnY].transform.position.y, 0);

    }
	
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Movement();
        }
	}




    void Attack1()
    {


    }

    void Attack2()
    {

    }

    void Movement()
    {
        int temp = Random.Range(0, 2);                          //Pick a number between 0 and 1
        if(temp == 0)                                           //if that number == 0, then we're moving vertically 
        {
            int _y = PickYCoord();
        }
        else if(temp == 1)
        {
            int _x = PickXCoord();
        }
        
         
        //yield return new WaitForSeconds(movementInterval);
        //SetTransform(_x, _y); 
    }
    
    int PickXCoord()
    {
        return 0; 
        
    }
    int PickYCoord()
    {
        int _y;
        _y = Random.Range(0, 3);
        return _y;
    }

    void SetTransform(int x, int y)
    {
        transform.position = new Vector3(grid.grid[x, y].transform.position.x, grid.grid[x, y].transform.position.y, 0);
    }


}
