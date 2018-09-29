using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class scr_PlayerMovement : scr_EntityAI
{
    AudioSource SFX_Source;
    public AudioClip[] movements_SFX;
    private AudioClip movement_SFX;


    public override void Move()
    {

    }
    public override void Attack()
    {

    }
    public override void UpdateAI()
    {
        MovementCheck();
    }



    void Start()
    {


    }

    bool axisPressed = false; //used to get "OnJoystickDown"
    void MovementCheck()
    {
        int _x = entity._gridPos.x;
        int _y = entity._gridPos.y; 


        if (!axisPressed)
        {
            //just pressed the joystick
            _x += scr_InputManager.MainHorizontal();
            if(scr_InputManager.MainHorizontal() == 0)
            {
                _y += scr_InputManager.MainVertical();
            }
            axisPressed = true;
        }

        if(scr_InputManager.MainHorizontal() == 0 && scr_InputManager.MainVertical() == 0)
        {
            //joystick is not pressed
            axisPressed = false;
        }

        /*
        if (Input.GetKeyDown(KeyCode.W))
        {
            //move up 
            _y ++;
            
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            //move left
            _x --;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _y--;
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            _x++; 
        }
         */

        if (scr_Grid.GridController.LocationOnGrid(_x, _y) &&  scr_Grid.GridController.ReturnTerritory(_x,_y) == entity.entityTerritory)
        {
            entity.SetTransform(_x, _y); 
        }
        
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
    
