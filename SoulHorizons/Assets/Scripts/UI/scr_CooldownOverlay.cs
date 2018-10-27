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
        StartCoroutine(CR_StartCooldown());
    }

    public IEnumerator CR_StartCooldown()
    {
        if(onCooldown == true)
        {
            yield return 0; 
        }

        overlay.fillAmount = 1;
        onCooldown = true;
        while (overlay.fillAmount > 0)
        {
            overlay.fillAmount -= rate;
            yield return new WaitForFixedUpdate();
        }
        if(overlay.fillAmount <= 0)
        {
            overlay.fillAmount = 0; 
            onCooldown = false;
            
        }
    }
}
