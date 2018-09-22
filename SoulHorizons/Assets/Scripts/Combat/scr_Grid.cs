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
    private Sprite[] tile_sprites;
    private int spriteTracker = 0;


    private void Start()
    {
        xSizeMax = scr_SceneManager.globalSceneManager.currentEncounter.xWidth;
        ySizeMax = scr_SceneManager.globalSceneManager.currentEncounter.yHeight;
        //calling in awake as a debug, should be called in Encounter
        SetNewGrid(xSizeMax, ySizeMax); 
    }


    private void BuildGrid()
    {
        tile_sprites = Resources.LoadAll<Sprite>("tiles_spritesheet");
        grid = new scr_Tile[xSizeMax, ySizeMax];
   
        //LOAD PLAYER SIDE
        for (int j = 0; j < ySizeMax; j++)
        {
            for (int i = 0; i < xSizeMax/2; i++)
            {
                Debug.Log(i + " " + j);
                scr_Tile tileToAdd = (scr_Tile)Instantiate(tile, new Vector3((float)i / (1f + .05f *j) + xOffset + .1f*j, (float)j / (2.25f + .15f * j) + yOffset, 0), Quaternion.identity);
                tileToAdd.territory = scr_SceneManager.globalSceneManager.currentEncounter.territoryColumn[i].territoryRow[j];
                tileToAdd.gridPositionX = i;
                tileToAdd.gridPositionY = j;

                spriteR = tileToAdd.GetComponent<SpriteRenderer>();
                spriteR.sprite = tile_sprites[spriteTracker];
                spriteR.color = Color.white;
                if (tile_sprites[spriteTracker] == null) Debug.Log("MISSING SPRITE");

                grid[i, j] = tileToAdd;

                spriteTracker++;
            }
        }
        spriteTracker = 0;
        //LOAD ENEMY SIDE
        for (int j = 0; j < ySizeMax; j++)
        {
            for (int i = xSizeMax-1; i >= xSizeMax/2; i--)
            {
                scr_Tile tileToAdd = (scr_Tile)Instantiate(tile, new Vector3((float)i / (1f + .05f * j) + xOffset + .1f * j, (float)j / (2.25f + .15f * j) + yOffset, 0), Quaternion.identity);
                tileToAdd.territory = scr_SceneManager.globalSceneManager.currentEncounter.territoryColumn[i].territoryRow[j];
                tileToAdd.gridPositionX = i;
                tileToAdd.gridPositionY = j;

                spriteR = tileToAdd.GetComponent<SpriteRenderer>();
                spriteR.sprite = tile_sprites[spriteTracker];
                spriteR.flipX = true; 
                spriteR.color = Color.white;
                if (tile_sprites[spriteTracker] == null) Debug.Log("MISSING SPRITE");

                grid[i, j] = tileToAdd;

                spriteTracker++;
            }
        }

            }


    public void SetNewGrid(int new_xSizeMax, int new_ySizeMax)
    {
        xSizeMax = new_xSizeMax;
        ySizeMax = new_ySizeMax;
        BuildGrid(); 

    }

    // Update is called once per frame
    void Update () {
        //Debug.Log("CENTER: " + grid[0, 0].transform.position.x);
    }

    public void PrimeNextTile(scr_Tile _tile)
    {
        //Need to check if the next tile in the pattern is on the grid 
        if(_tile.gridPositionX >= 0)
        {
            Debug.Log("hit"); 
        }
    }
}
