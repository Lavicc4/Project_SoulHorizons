using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Grid : MonoBehaviour{

    public int xsize;
    public int ysize;
    public float center_x;
    public float center_y;
    public GameObject[,] grid;
    public GameObject tile;

    private void Start()
    {
        grid = new GameObject[xsize, ysize];
        for (int i = 0; i < xsize; i++)
        {
            for (int j = 0; j < ysize; j++)
            {
                Debug.Log(i + " " + j);
                GameObject tiletoadd = (GameObject)Instantiate(tile, new Vector3(i + center_x, -j + center_y, 0), Quaternion.identity);
                grid[i, j] = tiletoadd;
            }
        }
    }

    /*public void Initialize()
    {
        scr_Tile[,] grid = new scr_Tile[xsize, ysize];
        for (int i = 0; i < xsize; i++)
        {
            for (int j = 0; j < ysize; j++)
            {
                //GameObject tile = (GameObject)Instantiate(); ;
                grid[i, j] = new scr_Tile(i, j);
            }
        }
        Debug.Log("CENTER: " + grid[0, 0].center.x);
    }*/

    // Update is called once per frame
    void Update () {
        //Debug.Log("CENTER: " + grid[0, 0].transform.position.x);
    }
}
