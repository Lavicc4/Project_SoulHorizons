using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scr_statemanager : MonoBehaviour {

    public Text RewardMessage;
    public Text PlayerHealth;
    bool endCombat = false;
    GameObject player;
    scr_Entity playerEntity;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerEntity = player.GetComponent<scr_Entity>();
        }
        else
        {
            Debug.Log("PLAYER NOT FOUND");
        }
    }
	
	// Update is called once per frame
	void Update () {
        UpdateHealth();
		if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            //Debug.Log("NO ENEMIES");
            RewardMessage.enabled = true;
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

    public void UpdateHealth()
    {
       
        PlayerHealth.text = "Health: " + playerEntity._health.hp;
        if(playerEntity._health.hp <= 0)
        {
            RewardMessage.text = "Oh no you died! Press V to return to the World Map";
            RewardMessage.enabled = true;
            endCombat = true;
        }
    }
}
