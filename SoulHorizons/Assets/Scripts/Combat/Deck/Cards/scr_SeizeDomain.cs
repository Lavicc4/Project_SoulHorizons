using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/SeizeDomain")]
public class scr_SeizeDomain : scr_Card
{
    bool colFound;
    public float duration;
    public Color newColor;
    public override void Activate()
    {
        colFound = false;
        ActivateEffects();
        Debug.Log("SEIZE!");
        for(int i = 0; i < scr_Grid.GridController.xSizeMax; i++)
        {
           
            for(int j = 0; j < scr_Grid.GridController.ySizeMax; j++)
            {
                //Debug.Log(scr_Grid.GridController.grid[i, j].territory.name);
                if (scr_Grid.GridController.grid[i, j].territory.name != TerrName.Player)
                {
                    //Debug.Log("Column: " + i);
                    colFound = true;
                    if (!scr_Grid.GridController.grid[i, j].occupied)
                    {
                        //Debug.Log("SEIZING!");
                        scr_Grid.GridController.SetTileTerritory(i, j, TerrName.Player, newColor);
                    }
                }
            }
            //Debug.Log(colFound);
            if (colFound)
            {
                break;
            }
            
           
        }
       

    }

    public override void StartCastingEffects()
    {

    }

    protected override void ActivateEffects()
    {
        //put start effects here
    }
}
