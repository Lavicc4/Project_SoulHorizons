using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_PlayerMovement : MonoBehaviour
{

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
    
    public int startx;
    public int starty;
    //public int xsize;
    //public int ysize;
    Coords gridpos;
    scr_Grid myGrid;
    GameObject gridController;
    //Vector3 pos;                                // For movement
    //float speed = 5.0f;                         // Speed of movement



    // Use this for initialization
    void Start()
    {


        gridController = GameObject.FindGameObjectWithTag("GridController");
        myGrid = gridController.GetComponent<scr_Grid>();
        gridpos = new Coords(startx, starty);

        PlaceAtStart(startx, starty);
        
        

        //Debug.Log("OCC: " + myGrid.grid[startx, starty].GetComponent<scr_Tile>().occupied);
        //Debug.Log("XSIZE: " + myGrid.xsize);
        //Debug.Log("CENTERX " + myGrid.grid[0, 0].transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Thought of this at 3:26am so im not going to implement, do a check to see the territory of the tile , if player , then move to it 
        if (Input.GetKeyDown(KeyCode.W)  &&  gridpos.y != 2)
        {
            //move up 
            int _y = gridpos.y + 1;
            SetTransform(gridpos.x, _y);
            gridpos.y = _y;
        }
        if(Input.GetKeyDown(KeyCode.A)  &&  gridpos.x != 0)
        {
            //move left
            int _x = gridpos.x - 1;
            SetTransform(_x, gridpos.y);
            gridpos.x = _x;
        }
        if (Input.GetKeyDown(KeyCode.S) && gridpos.y != 0)
        {
            //move down
            int _y = gridpos.y - 1;
            SetTransform(gridpos.x, _y);
            gridpos.y = _y;
        }
        if(Input.GetKeyDown(KeyCode.D)  &&  gridpos.x != 2)
        {
            //move right
            int _x = gridpos.x + 1;
            SetTransform(_x, gridpos.y);
            gridpos.x = _x;
        }
        
        
    }
    void FixedUpdate()
    {
     
        //transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);    // Move there
        //Debug.Log(gridpos.x + " " + gridpos.y);
    }
    


    //This function will set the transform of the player, and occupy/unoccupy appropriate spaces 
    void SetTransform(int x, int y)
    {
        int _x = gridpos.x;
        int _y = gridpos.y;                                                                                                                 //Store current x and y locations on grid
        myGrid.grid[x, y].GetComponent<scr_Tile>().occupied = true;                                                                         //set new space to occupied
        transform.position = new Vector3(myGrid.grid[x, y].transform.position.x, myGrid.grid[x, y].transform.position.y, 0);                //move to the new space 
        myGrid.grid[_x, _y].GetComponent<scr_Tile>().occupied = false;                                                                      //set old location to unoccupied once we move  
        
    }

    void PlaceAtStart(int x, int y)
    {
        myGrid.grid[x, y].GetComponent<scr_Tile>().occupied = true;                                                                         //set new space to occupied
        transform.position = new Vector3(myGrid.grid[x, y].transform.position.x, myGrid.grid[x, y].transform.position.y, 0);                //move to the new space 

    }
    
}




/*
 * if (Input.GetKeyDown(KeyCode.A) && gridpos.x - 1 >= 0)
        {        // Left
            myGrid.grid[gridpos.x, gridpos.y].GetComponent<scr_Tile>().occupied = false;
            gridpos.x -= 1;
            GameObject tile = myGrid.grid[gridpos.x, gridpos.y];
            transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, 0);
            tile.GetComponent<scr_Tile>().occupied = true;
            
            //Debug.Log(transform.position);
        }
        if (Input.GetKeyDown(KeyCode.D) && gridpos.x + 1 < myGrid.xsize)
        {        // Right
            myGrid.grid[gridpos.x, gridpos.y].GetComponent<scr_Tile>().occupied = false;
            gridpos.x += 1;
            GameObject tile = myGrid.grid[gridpos.x, gridpos.y];
            transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, 0);
            tile.GetComponent<scr_Tile>().occupied = true;
        }
        if (Input.GetKeyDown(KeyCode.W) && gridpos.y - 1 >= 0)
        {        // Up
            myGrid.grid[gridpos.x, gridpos.y].GetComponent<scr_Tile>().occupied = false;
            gridpos.y -= 1;
            GameObject tile = myGrid.grid[gridpos.x, gridpos.y];
            transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, 0);
            tile.GetComponent<scr_Tile>().occupied = true;
        }
        if (Input.GetKeyDown(KeyCode.S) && gridpos.y + 1 < myGrid.ysize)
        {        // Down
            myGrid.grid[gridpos.x, gridpos.y].GetComponent<scr_Tile>().occupied = false;
            gridpos.y += 1;
            GameObject tile = myGrid.grid[gridpos.x, gridpos.y];
            transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, 0);
            tile.GetComponent<scr_Tile>().occupied = true;
        }

    */
    
