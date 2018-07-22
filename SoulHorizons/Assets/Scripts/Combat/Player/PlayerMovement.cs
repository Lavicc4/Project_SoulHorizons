using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    struct Coords
    {
        public int x;
        public int y;

        public Coords(int a, int b)
        {
            x = a;
            y = b;
        }

    }

    public int startx;
    public int starty;
    public int xsize;
    public int ysize;
    Coords gridpos;

    Vector3 pos;                                // For movement
    float speed = 5.0f;                         // Speed of movement

    // Use this for initialization
    void Start()
    {

        int[,] grid = new int[xsize, ysize];
        pos = transform.position;          // Take the initial position
        gridpos = new Coords(startx, starty);

    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A) && transform.position == pos && gridpos.x - 1 >= 0)
        {        // Left
            pos += Vector3.left;
            gridpos.x -= 1;
        }
        if (Input.GetKey(KeyCode.D) && transform.position == pos && gridpos.x + 1 < xsize)
        {        // Right
            pos += Vector3.right;
            gridpos.x += 1;
        }
        if (Input.GetKey(KeyCode.W) && transform.position == pos && gridpos.y - 1 >= 0)
        {        // Up
            pos += Vector3.up;
            gridpos.y -= 1;
        }
        if (Input.GetKey(KeyCode.S) && transform.position == pos && gridpos.y + 1 < ysize)
        {        // Down
            pos += Vector3.down;
            gridpos.y += 1;
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);    // Move there
        Debug.Log(gridpos.x + " " + gridpos.y);
    }

}
