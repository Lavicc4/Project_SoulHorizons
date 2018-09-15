using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Tile : MonoBehaviour{


    bool occupied;
    bool harmful;
    public Vector3 center;

    public scr_Tile()
    {
        harmful = false;
        occupied = false;
    }
    public scr_Tile(int a, int b)
    {
        harmful = false;
        occupied = false;
        center = new Vector3(a, b, 0);
        
    }

    // Use this for initialization
    void Start()
    {
        harmful = false;
        occupied = false;
        //transform.position = center;
    }

}
