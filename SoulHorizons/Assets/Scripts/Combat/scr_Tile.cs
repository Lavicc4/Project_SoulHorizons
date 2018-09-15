using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Tile : MonoBehaviour{


    public bool occupied;
    public bool harmful;

    // Use this for initialization
    void Awake()
    {
        harmful = false;
        occupied = false;
        //transform.position = center;
    }

}
