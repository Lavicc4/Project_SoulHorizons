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

    //Build Grid Tiles
    private void BuildGrid()
    {
        //tile_sprites = Resources.LoadAll<Sprite>("tiles_spritesheet");
        grid = new scr_Tile[xSizeMax, ySizeMax];

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
        //Set movement to true
        scr_InputManager.disableInput = false;
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void ActivateTile(int x, int y)
    {
        if (LocationOnGrid(x, y))
        {
            grid[x, y].Activate();
            
        }
    }
    public void ActivateTile(int x, int y, ActiveAttack activeAttack)
    {
        if (LocationOnGrid(x, y))
        {
            grid[x, y].Activate(activeAttack);

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
    public void DePrimeTile(int x, int y)
    {
        if (LocationOnGrid(x, y))
            grid[x, y].DePrime();
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

    public void SetTileTerritory(int x, int y, TerrName newName, Color newColor)
    {
        grid[x, y].SetTerritory(newName, newColor);
   
    }

    public Territory ReturnTerritory(int x, int y)
    {
        return grid[x, y].territory;
    }
    
    /// <summary>
    /// Check if an active entity is at the attack's position to be hit by the attack. Change the attack based on the results and return it.
    /// </summary>
    /// <param name="attack"></param>
    /// <returns></returns>
    public ActiveAttack AttackPosition(ActiveAttack attack)
    {
        for(int i=0; i < activeEntities.Length; i++)
        {
            if (activeEntities[i].gameObject.activeSelf)
            {
                //Why is this not using activeEntities[i]._gridPos.Equals(attack.pos)? Why create a new object? - Colin
                if (activeEntities[i]._gridPos == new Vector2Int(attack.pos.x, attack.pos.y))
                {
                    //Debug.Log(activeEntities[i].entityTerritory.name + " " + attack.entity.entityTerritory.name);
                    if (activeEntities[i].type != attack.entity.type)
                    {
                        Debug.Log("ACTIVE ENTITY HIT!");
                        //Check if entity is invincible and assigns iframes accordingly
                        if (!activeEntities[i].isInvincible())
                        {
                            activeEntities[i].HitByAttack(attack._attack);
                            if (activeEntities[i].has_iframes)
                            {
                                //Activate invincibility frames
                                activeEntities[i].setInvincible(true, activeEntities[i].invulnTime);

                            }
                        }
                        attack.entityIsHit = true;
                        attack.entityHit = activeEntities[i];
                        attack._attack.ImpactEffects();
                    }
                    return attack;
                }
            }
          
        }
        return attack; 
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Vector3 GetWorldLocation(int x, int y)
    {
        if (LocationOnGrid(x, y))
            return new Vector3(grid[x, y].transform.position.x, grid[x, y].transform.position.y, 0);

        else
            return new Vector3(-100,-100,-100); // will def be off the grid 

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public Vector3 GetWorldLocation(Vector2Int pos)
    {
        if (LocationOnGrid(pos.x,pos.y))
            return new Vector3(grid[pos.x, pos.y].transform.position.x, grid[pos.x, pos.y].transform.position.y, 0);

        else
            return new Vector3(-100, -100, -100); // will def be off the grid 

    }

    public void RemoveEntity(scr_Entity entity)
    {
        float tempID = entity.gameObject.GetInstanceID();
        for (int i = 0; i < activeEntities.Length; i++){
            if (activeEntities[i].gameObject.GetInstanceID() == tempID)
            {
                Debug.Log("help me");
                scr_Entity[] temporaryEntities = new scr_Entity[activeEntities.Length - 1];
                for(int j = 0; j < activeEntities.Length; j++)
                {
                    if (j >= i)
                    {
                        temporaryEntities[j] = activeEntities[j + 1];
                        
                    }
                    else if(j < i)
                    {
                        temporaryEntities[j] = activeEntities[j];
                    }
                }
                Debug.Log(temporaryEntities);
                activeEntities = temporaryEntities;
                Destroy(entity.gameObject); 

            }
            else
            {
                Debug.Log("else"); 
                return;
            }
        }
        
    }

}
