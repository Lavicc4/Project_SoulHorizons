using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Grid : MonoBehaviour {

    public int xsize;
    public int ysize;
    public scr_Tile[,] grid;

    private void Awake()
    {
        scr_Tile[,] grid = new scr_Tile[xsize, ysize];
        for (int i = 0; i < xsize; i++)
        {
            for (int j = 0; j < ysize; j++)
            {
                grid[i, j] = new scr_Tile(i, j);
            }
        }
    }
    // Use this for initialization
    void Start () {
        
    }

    public void initialize()
    {
        scr_Tile[,] grid = new scr_Tile[xsize, ysize];
        for (int i = 0; i < xsize; i++)
        {
            for (int j = 0; j < ysize; j++)
            {
                grid[i, j] = new scr_Tile(i, j);
            }
        }
        Debug.Log("CENTER: " + grid[0, 0].center_x);
    }

    // Update is called once per frame
    void Update () {
        Debug.Log("CENTER: " + grid[0, 0].center_x);
    }
}
