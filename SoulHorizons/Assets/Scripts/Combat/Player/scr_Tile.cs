using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Tile {


    bool occupied;
    bool harmful;
    public int center_x;
    public int center_y;

    public scr_Tile(int a, int b)
    {
        harmful = false;
        occupied = false;
        center_x = a;
        center_y = b;
    }

    
}
