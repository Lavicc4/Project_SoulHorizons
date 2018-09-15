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
    //Vector3 pos;                                // For movement
    //float speed = 5.0f;                         // Speed of movement

    
    // Use this for initialization
    void Start()
    {

        gridpos = new Coords(startx, starty);
        myGrid = GameObject.FindGameObjectWithTag("Player_Grid").GetComponent<scr_Grid>();
        transform.position = new Vector3(myGrid.grid[startx, starty].transform.position.x, myGrid.grid[startx, starty].transform.position.y, 0);
        myGrid.grid[startx, starty].GetComponent<scr_Tile>().occupied = true;
        Debug.Log("OCC: " + myGrid.grid[startx, starty].GetComponent<scr_Tile>().occupied);
        Debug.Log("XSIZE: " + myGrid.xsize);
        Debug.Log("CENTERX " + myGrid.grid[0, 0].transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && gridpos.x - 1 >= 0)
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

    }
    void FixedUpdate()
    {
     
        //transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);    // Move there
        //Debug.Log(gridpos.x + " " + gridpos.y);
    }

}
