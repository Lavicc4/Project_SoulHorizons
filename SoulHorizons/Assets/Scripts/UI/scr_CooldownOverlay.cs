using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class scr_CooldownOverlay : MonoBehaviour {

    private Image overlay;
    private GameObject overlayGameObject;
    private bool onCooldown = false;
    public float rate = 0.01f; 

	// Use this for initialization
	void Start () {
        overlayGameObject = FindGameObjectInChildWithTag(gameObject, "CooldownOverlay");
        overlay = overlayGameObject.GetComponent<Image>(); 
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCooldown();
        }
	}

    public static GameObject FindGameObjectInChildWithTag(GameObject parent, string tag)
    {
        Transform t = parent.transform;

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.tag == tag)
            {
                return t.GetChild(i).gameObject;
            }

        }

        return null;
    }

    public void StartCooldown()
    {
        overlay.fillAmount = 1;
        onCooldown = true;
        while (onCooldown && overlay.fillAmount > 0)
        {
            overlay.fillAmount -= rate; 
        }
        if(overlay.fillAmount <= 0)
        {
            onCooldown = false; 
        }
    }
}
