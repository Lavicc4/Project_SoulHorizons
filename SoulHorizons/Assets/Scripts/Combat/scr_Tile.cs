using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Tile : MonoBehaviour{

    [Header("Combat Colors")]
    public Color primeColor;
    public Color activeColor;
    public Color inactiveColor;

    
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
    public int queuedAttacks = 0;
    public scr_Entity entityOnTile;
    

    Vector2 spriteSize = new Vector2 (1f,.85f);
    SpriteRenderer spriteRenderer;
     

    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = inactiveColor;
        spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        spriteRenderer.size = spriteSize;
        isPrimed = false;
        isActive = false; 
        harmful = false;
        occupied = false;
        gridController = GameObject.FindGameObjectWithTag("GridController");
        grid = gridController.GetComponent<scr_Grid>();
        entityOnTile = null;
         
        
    }
    private void Update()
    {
        if (isActive && entityOnTile != null)
        {
            //Debug.Log("OW!!!!");
            //TEMPORARY HARDCODED VALUE, GET ATTACK ASSOCIATED WITH ACTIVATED TILE AND GET DAMAGE FROM THAT
            //entityOnTile._health.TakeDamage(1);
            isActive = false; //So it only hits once and not every frame, can change if it's multi hit, add that functionality later
        }
    }

    public void InitalizeTile()
    {


    }

    public void SetTerritory(Territory newTer)
    {
        territory = newTer;
       
    }

    public void Prime()
    {
        isPrimed = true; 
        if(!isActive)
        spriteRenderer.color = primeColor;
        
    }
    public void Activate()
    {
        queuedAttacks++; 
        isPrimed = false; 
        isActive = true; 
        spriteRenderer.color = activeColor;
    }
    
    public void Deactivate()
    {
        queuedAttacks--; 
        if(queuedAttacks == 0)
        {
            /*if (isPrimed)
            {
                Prime(); 
            }
            else
            {*/
                isActive = false;
                spriteRenderer.color = inactiveColor;
            //}
            
        }
        
         
    }
    
   
}
