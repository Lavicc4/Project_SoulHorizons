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
        scr_PlayerMovement player_coords = GameObject.FindGameObjectWithTag("Player").GetComponent<scr_PlayerMovement>();
        for (int i = 0; i < xsize; i++)
        {
            for (int j = 0; j < ysize; j++)
            {
                Debug.Log(i + " " + j);
                GameObject tiletoadd = (GameObject)Instantiate(tile, new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x + i, GameObject.FindGameObjectWithTag("Player").transform.position.y - j, 0), Quaternion.identity);
                grid[i, j] = tiletoadd;
            }
        }
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log("CENTER: " + grid[0, 0].transform.position.x);
    }
}
