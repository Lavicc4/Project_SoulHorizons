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

        //int[,] grid = new int[xsize, ysize];
        //pos = transform.position;          // Take the initial position
        gridpos = new Coords(startx, starty);
        myGrid = GameObject.FindGameObjectWithTag("Player_Grid").GetComponent<scr_Grid>();
        transform.position = new Vector3(myGrid.grid[startx, starty].transform.position.x, myGrid.grid[startx, starty].transform.position.y, 0);
        Debug.Log("XSIZE: " + myGrid.xsize);
        Debug.Log("CENTERX " + myGrid.grid[0, 0].transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && gridpos.x - 1 >= 0)
        {        // Left
            //Debug.Log("CENTERX " + grid.grid[gridpos.x - 1, gridpos.y].transform.position.x);
            gridpos.x -= 1;
            transform.position = new Vector3(myGrid.grid[gridpos.x, gridpos.y].transform.position.x, myGrid.grid[gridpos.x, gridpos.y].transform.position.y, 0);
            Debug.Log(transform.position);
        }
        if (Input.GetKeyDown(KeyCode.D) && gridpos.x + 1 < myGrid.xsize)
        {        // Right

            gridpos.x += 1;
            transform.position = new Vector3(myGrid.grid[gridpos.x, gridpos.y].transform.position.x, myGrid.grid[gridpos.x, gridpos.y].transform.position.y, 0);
        }
        if (Input.GetKeyDown(KeyCode.W) && gridpos.y - 1 >= 0)
        {        // Up

            gridpos.y -= 1;
            transform.position = new Vector3(myGrid.grid[gridpos.x, gridpos.y].transform.position.x, myGrid.grid[gridpos.x, gridpos.y].transform.position.y, 0);
        }
        if (Input.GetKeyDown(KeyCode.S) && gridpos.y + 1 < myGrid.ysize)
        {        // Down

            gridpos.y += 1;
            transform.position = new Vector3(myGrid.grid[gridpos.x, gridpos.y].transform.position.x, myGrid.grid[gridpos.x, gridpos.y].transform.position.y, 0);
        }

    }
    void FixedUpdate()
    {
     
        //transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);    // Move there
        //Debug.Log(gridpos.x + " " + gridpos.y);
    }

}
