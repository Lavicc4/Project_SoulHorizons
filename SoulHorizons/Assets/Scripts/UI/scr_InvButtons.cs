using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_InvButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void addCard()
    {
        GameObject myCard = gameObject.transform.parent.gameObject;
        myCard.GetComponent<scr_CardUI>();
    }

    public void removeCard()
    {
        GameObject myCard = gameObject.transform.parent.gameObject;
    }
}
