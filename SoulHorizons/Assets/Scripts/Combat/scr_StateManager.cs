using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scr_StateManager : MonoBehaviour {

    public Text rewardMessage;
    bool endCombat = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            //Debug.Log("NO ENEMIES");
            rewardMessage.enabled = true;
            endCombat = true;
        }
        if (endCombat)
        {
            if (Input.GetKey(KeyCode.V))
            {
                Debug.Log("Switching Scenes");
                SceneManager.LoadScene("sn_WorldMap");
            }
        }
	}
}
