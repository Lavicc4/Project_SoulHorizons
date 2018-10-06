using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Update the UI, take in user input, and manage information on the available soul transforms
/// </summary>
public class scr_SoulManager : MonoBehaviour {

    public List<SoulTransform> soulTransforms = new List<SoulTransform>();

	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Replace the default list of transforms with passed-in arguments
    /// </summary>
    /// <param name="transforms"></param>
    public void SetSoulTransforms(params SoulTransform[] transforms)
    {
        soulTransforms = new List<SoulTransform>(transforms.Length);


    }

    /// <summary>
    /// Get user input
    /// </summary>
    private void UserInput()
    {

    }
}
