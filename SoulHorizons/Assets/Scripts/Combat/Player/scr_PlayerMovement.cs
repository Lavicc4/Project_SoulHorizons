using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class scr_PlayerMovement : scr_EntityAI
{
    public Attack basicAttack; 

    AudioSource Footsteps_SFX;
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
        if (Input.GetMouseButtonDown(0)){
            scr_AttackController.attackController.AddNewAttack(basicAttack, entity._gridPos.x, entity._gridPos.y, entity);
        }
    }
    public override void Die()
    {
        //throw new System.NotImplementedException();
    }


    void Start()
    {


    }

    int inputX;
    int inputY;
    bool axisPressed = false; //used to get "OnJoystickDown"
    void MovementCheck()
    {
        int _x = entity._gridPos.x;
        int _y = entity._gridPos.y;


        /*if (!axisPressed)
        {
            //Debug.Log(scr_InputManager.MainHorizontal() + " " + scr_InputManager.MainVertical());
            //just pressed the joystick
            _x += scr_InputManager.MainHorizontal();
            if(scr_InputManager.MainHorizontal() == 0)
            {
                _y += scr_InputManager.MainVertical();
            }
       
            axisPressed = true;
        }*/

        if(inputX != scr_InputManager.MainHorizontal())
        {
            _x += scr_InputManager.MainHorizontal();
            inputX = scr_InputManager.MainHorizontal();
            axisPressed = true;
            if (scr_InputManager.MainHorizontal() == 0)
            {
                AudioSource[] SFX_Sources = GetComponents<AudioSource>();
                Footsteps_SFX = SFX_Sources[0];
                int index = Random.Range(0, movements_SFX.Length);
                movement_SFX = movements_SFX[index];
                Footsteps_SFX.clip = movement_SFX;
                Footsteps_SFX.Play();
            }
        }

        if (inputY != scr_InputManager.MainVertical())
        {
            _y += scr_InputManager.MainVertical();
            inputY = scr_InputManager.MainVertical();
            axisPressed = true;
            if (scr_InputManager.MainVertical() == 0)
            {
                AudioSource[] SFX_Sources = GetComponents<AudioSource>();
                Footsteps_SFX = SFX_Sources[0];
                int index = Random.Range(0, movements_SFX.Length);
                movement_SFX = movements_SFX[index];
                Footsteps_SFX.clip = movement_SFX;
                Footsteps_SFX.Play();
            }
        }

        if (scr_InputManager.MainHorizontal() == 0 && scr_InputManager.MainVertical() == 0)
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

        if (scr_Grid.GridController.LocationOnGrid(_x, _y) &&  scr_Grid.GridController.ReturnTerritory(_x,_y).name == entity.entityTerritory.name)
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
    
