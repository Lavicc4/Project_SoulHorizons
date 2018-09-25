using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scr_StateManager : MonoBehaviour {

    public Text rewardMessage;
    public Text PlayerHealth;
    bool endCombat = false;
	// Use this for initialization
	void Start () {
        UpdateHealth(10); //CHANGE THIS LATER TO DETECT STARTING PLAYER HEALTH
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
            //INSERT CODE TO STOP ENEMY AND PLAYER MOVEMENT HERE
            if (Input.GetKey(KeyCode.V))
            {
                Debug.Log("Switching Scenes");
                SceneManager.LoadScene("sn_WorldMap");
            }
        }
	}

    public void UpdateHealth(int hp)
    {
        PlayerHealth.text = "Health: " + hp;
        if(hp <= 0)
        {
            rewardMessage.text = "Oh no you died! Press V to return to the World Map";
            rewardMessage.enabled = true;
            endCombat = true;
        }
    }
}
