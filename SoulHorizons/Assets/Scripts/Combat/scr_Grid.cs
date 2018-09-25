using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Grid : MonoBehaviour{

    

    public int xSizeMax; 
    public int ySizeMax;
    public float xOffset;
    public float yOffset; 
    public scr_Tile[,] grid;
    public scr_Tile tile;
    private SpriteRenderer spriteR;
    public Sprite[] tile_sprites;
    private int spriteTracker = 0;
    public scr_Entity[] activeEntities; 




    public static scr_Grid GridController;

    private void Awake()
    {
        GridController = this;     
    }


    private void Start()
    {
        InitEncounter(); 
    }


    private void BuildGrid()
    {
        //tile_sprites = Resources.LoadAll<Sprite>("tiles_spritesheet");
        grid = new scr_Tile[xSizeMax, ySizeMax];
   
        //LOAD PLAYER SIDE
        for (int j = 0; j < ySizeMax; j++)
        {
            for (int i = 0; i < xSizeMax; i++)
            {
                scr_Tile tileToAdd = null; 
                if(i < xSizeMax / 2)
                {
                    tileToAdd = (scr_Tile)Instantiate(tile, new Vector3((float)i / (.95f + .05f * j) + xOffset + .1f * j, (float)j / (2.25f + .15f * j) + yOffset, 0), Quaternion.identity);
                
                }
                else
                {
                    tileToAdd = (scr_Tile)Instantiate(tile, new Vector3((float)i / (.95f + .05f * j) + xOffset + .155f * j, (float)j / (2.25f + .15f * j) + yOffset, 0), Quaternion.identity);
                    tileToAdd.GetComponent<SpriteRenderer>().flipX = true;

                }
                tileToAdd.territory = scr_SceneManager.globalSceneManager.currentEncounter.territoryColumn[i].territoryRow[j];
                tileToAdd.gridPositionX = i;
                tileToAdd.gridPositionY = j;

                spriteR = tileToAdd.GetComponent<SpriteRenderer>();
                spriteR.sprite = tile_sprites[spriteTracker];
                
                if (tile_sprites[spriteTracker] == null) Debug.Log("MISSING SPRITE");

                grid[i, j] = tileToAdd;

                spriteTracker++;
            }
        }
        spriteTracker = 0;

    }

    public bool CheckIfOccupied(int x, int y)
    {
        return grid[x, y].occupied;
    }


    public void SetNewGrid(int new_xSizeMax, int new_ySizeMax)
    {
        xSizeMax = new_xSizeMax;
        ySizeMax = new_ySizeMax;
        BuildGrid(); 

    }

    //BUG - AT START TILES DON'T COUNT AS OCCUPIED, AFTER INIT SET TILES TO OCCUPIED FOR INITIALIZED ENTITIES
    public void InitEncounter()
    {
        xSizeMax = scr_SceneManager.globalSceneManager.currentEncounter.xWidth;
        ySizeMax = scr_SceneManager.globalSceneManager.currentEncounter.yHeight;
        //calling in awake as a debug, should be called in Encounter
        SetNewGrid(xSizeMax, ySizeMax);
        activeEntities = new scr_Entity[scr_SceneManager.globalSceneManager.currentEncounter.entities.Length]; 
        for(int x = 0; x < activeEntities.Length; x++)
        {
            scr_Entity _entity = new scr_Entity();
            _entity = (scr_Entity)Instantiate(scr_SceneManager.globalSceneManager.currentEncounter.entities[x]._entity, Vector3.zero, Quaternion.identity);
            _entity.InitPosition(scr_SceneManager.globalSceneManager.currentEncounter.entities[x].x, scr_SceneManager.globalSceneManager.currentEncounter.entities[x].y);
            activeEntities[x] = _entity;
        }
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log("CENTER: " + grid[0, 0].transform.position.x);
    }

    public void PrimeNextTile(int x , int y)
    {
        if(LocationOnGrid(x , y ))
            grid[x, y].Prime(); 
    }
    public void ActivateTile(int x, int y)
    {
        if (LocationOnGrid(x, y))
        {
            grid[x, y].Activate();
            
        }
    }

    public bool LocationOnGrid(int x, int y)
    {
        if(x >= 0 && grid.GetLength(0) > x && grid.GetLength(1) > y && y >=0)
        {
            return true;
        }
        return false;
    }
    
    public void DeactivateTile(int x, int y)
    {
        if (LocationOnGrid(x, y))
            grid[x, y].Deactivate();

    }

    public void SetTileOccupied(bool isOccupied, int x, int y, scr_Entity ent)
    {
        if (LocationOnGrid(x, y))
        {
            grid[x, y].occupied = isOccupied;
            if (isOccupied) grid[x, y].entityOnTile = ent;
            else grid[x, y].entityOnTile = null;
        }
    }

    public scr_Tile.Territory ReturnTerritory(int x, int y)
    {
        return grid[x, y].territory;
    }
    
    public void AttackPosition(int x, int y, Attack _attack)
    {
        for(int i=0; i < activeEntities.Length; i++)
        {
            if(activeEntities[i]._gridPos == new Vector2Int(x, y))
            {
                activeEntities[i].HitByAttack(_attack); 
            }
        }
    }
    
}
