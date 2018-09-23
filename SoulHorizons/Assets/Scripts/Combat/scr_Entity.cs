using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Entity : MonoBehaviour
{

    public Vector2Int _gridPos = new Vector2Int();
    public Health _health = new Health();
    public scr_EntityAI _ai;


    public void Update()
    {
        _ai.UpdateAI();
    }


    public void InitPosition(int x, int y)
    {

        //scr_Grid.GridController.SetTileOccupied(false, x, y);
        _gridPos = new Vector2Int(x, y);
        transform.position = new Vector3(scr_Grid.GridController.grid[x, y].transform.position.x, scr_Grid.GridController.grid[x, y].transform.position.y, 0);
        scr_Grid.GridController.SetTileOccupied(true, x, y);
    }

    public void SetTransform(int x, int y)
    {

        scr_Grid.GridController.SetTileOccupied(false, _gridPos.x, _gridPos.y);
        _gridPos = new Vector2Int(x, y);
        transform.position = new Vector3(scr_Grid.GridController.grid[x, y].transform.position.x, scr_Grid.GridController.grid[x, y].transform.position.y, 0);                    //move to the new space 
        scr_Grid.GridController.SetTileOccupied(true, _gridPos.x, _gridPos.y);
    }
}
[System.Serializable]
public class Health{

    public int hp = 10; 
}





    


