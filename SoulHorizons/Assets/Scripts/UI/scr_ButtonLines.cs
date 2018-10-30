﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_ButtonLines : MonoBehaviour {

    public int previousEncounterNum;
    public int myEncounterNum;
    public Material material; 

    public GameObject prevEncounter;
    private GameObject myEncounter;
    private LineRenderer line;


    // Use this for initialization
    void Start () {
        myEncounter = this.gameObject;
        // Add a Line Renderer to the GameObject
        line = this.gameObject.AddComponent<LineRenderer>();
        // Set the width of the Line Renderer
        line.startWidth = 25;
        // Set the number of vertex fo the Line Renderer
        line.positionCount = 2;
        line.startColor = Color.black;
        line.sortingOrder = 25;
        line.material = material; 

    }
	
	// Update is called once per frame
	void Update () {
        DrawConnection(myEncounter, prevEncounter);
    }

    public void DrawConnection(GameObject endPoint1, GameObject endPoint2)
    {
        if (endPoint1 != null && endPoint2 != null)
        {
            // Update position of the two vertex of the Line Renderer
            line.SetPosition(0, endPoint1.transform.position);
            line.SetPosition(1, endPoint2.transform.position);
        }
        else
        {
            return;
        }
    }
}
