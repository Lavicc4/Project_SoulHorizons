using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Grid : MonoBehaviour{

    public int xsize;
    public int ysize;
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
                GameObject tiletoadd = (GameObject)Instantiate(tile, new Vector3(gameObject.transform.position.x + i, gameObject.transform.position.y - j, 0), Quaternion.identity);
                grid[i, j] = tiletoadd;
            }
        }
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log("CENTER: " + grid[0, 0].transform.position.x);
    }
}
