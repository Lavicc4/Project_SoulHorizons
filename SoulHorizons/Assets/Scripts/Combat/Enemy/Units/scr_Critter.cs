using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Critter : scr_EntityAI {


    

    public override void Move()
    {
        
    }
    public override void Attack()
    {
        
    }
    public override void UpdateAI()
    {
        int _x = entity._gridPos.x;
        int _y = entity._gridPos.y;
        scr_Grid.GridController.SetTileOccupied(true, _x, _y, entity);
    }
}
