using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Tile : MonoBehaviour{

    [Header("Combat Colors")]
    public Color primeColor;
    public Color activeColor;
    public Color inactiveColor;

    public float telegraphTime;
    public float activeTime; 
    public bool occupied;
    public bool harmful;
    public bool isPrimed;
    public bool isActive; 
    public Territory territory;
    public enum Territory {Player,Enemy,Neutral,Blocked}
    GameObject gridController;
    scr_Grid grid;
    public int gridPositionX;
    public int gridPositionY;
    

    Vector2 spriteSize = new Vector2 (1,1);
    SpriteRenderer spriteRenderer;
     

    
    void Awake()
    {
        
        harmful = false;
        occupied = false;
        gridController = GameObject.FindGameObjectWithTag("GridController");
        grid = gridController.GetComponent<scr_Grid>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = inactiveColor;
        spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        spriteRenderer.size = spriteSize; 
        //transform.position = center;
    }

    public void SetTerritory(Territory newTer)
    {
        territory = newTer;
       
    }

    public void Prime()
    {
        spriteRenderer.color = primeColor;
        StartCoroutine(PrimeTileWait());
    }
    public void Activate()
    {
        spriteRenderer.color = activeColor;
        scr_Tile _tile = grid.grid[gridPositionX -1 , gridPositionY];                        //get reference to tile 
        //need prime the next tile in the pattern
        grid.PrimeNextTile(_tile);
        StartCoroutine(ActiveTileWait());
        

        
    }

    IEnumerator PrimeTileWait()
    {
        bool _waiting = false;
        if (!_waiting)
        {
            _waiting = true; 
            yield return new WaitForSeconds(telegraphTime);
            Activate(); 
        }
        
    }
    IEnumerator ActiveTileWait()
    {
        bool _waiting = false;
        if (!_waiting)
        {
            _waiting = true;
            yield return new WaitForSeconds(activeTime);
            spriteRenderer.color = inactiveColor;
        }

    }
}
