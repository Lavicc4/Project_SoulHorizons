using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Grid : MonoBehaviour{

    public int xsize;
    public int ysize;
    public bool flipped;
    public GameObject[,] grid;
    public GameObject tile;
    private SpriteRenderer spriteR;
    private Sprite[] tile_sprites;
    private int spriteTracker = 0;

    private void Awake()
    {
        tile_sprites = Resources.LoadAll<Sprite>("tiles_spritesheet");
        grid = new GameObject[xsize, ysize];
        if (!flipped)
        {
            for (int j = ysize - 1; j >= 0; j--)
            {
                for (int i = 0; i < xsize; i++)
                {
                    //Debug.Log(i + " " + j);
                    GameObject tiletoadd = (GameObject)Instantiate(tile, new Vector3(gameObject.transform.position.x + (float)i/1.15f, gameObject.transform.position.y - (float)j/2.25f, 0), Quaternion.identity);
                    spriteR = tiletoadd.GetComponent<SpriteRenderer>();
                    spriteR.sprite = tile_sprites[spriteTracker];
                    spriteR.color = Color.white;
                    spriteR.flipX = flipped;
                    if (tile_sprites[spriteTracker] == null) Debug.Log("MISSING SPRITE");
                    grid[i, j] = tiletoadd;
                    spriteTracker++;
                }
            }
        }
        //FLIPPED LOADING OF SPRITES FOR ENEMY
        else
        {
            for (int j = ysize - 1; j >= 0; j--)
            {
                for (int i = xsize - 1; i >= 0; i--)
                {
                    //Debug.Log(i + " " + j);
                    GameObject tiletoadd = (GameObject)Instantiate(tile, new Vector3(gameObject.transform.position.x + i/1.15f, gameObject.transform.position.y - j/2.25f, 0), Quaternion.identity);
                    spriteR = tiletoadd.GetComponent<SpriteRenderer>();
                    spriteR.sprite = tile_sprites[spriteTracker];
                    spriteR.color = Color.white;
                    spriteR.flipX = flipped;
                    if (tile_sprites[spriteTracker] == null) Debug.Log("MISSING SPRITE");
                    grid[i, j] = tiletoadd;
                    spriteTracker++;
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log("CENTER: " + grid[0, 0].transform.position.x);
    }
}
