using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scr_statemanager : MonoBehaviour {

    public Text RewardMessage;
    public Text PlayerHealth;
    public Text TempHealth;
    public Text EffectText;
    bool endCombat = false;
    bool showEffect = false;
    string EffectString;
    GameObject player;
    scr_Entity playerEntity;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        EffectText.enabled = false;
        if (player != null)
        {
            playerEntity = player.GetComponent<scr_Entity>();
            //load the health from the GameState
            int hp = SaveLoad.currentGame.GetPlayerHealth();
            if (hp > 0) //make sure the health has been set previously
            {
                playerEntity._health.hp = hp;
            }
        }
        else
        {
            Debug.Log("PLAYER NOT FOUND");
        }
    }
	
	// Update is called once per frame
	void Update () {
        UpdateHealth();
        UpdateEffects();
        //END OF ENCOUNTER - NO MORE ENEMIES
		if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            //Debug.Log("NO ENEMIES");
            //scr_InputManager.disableInput = true;
            RewardMessage.enabled = true;
            endCombat = true;

            //GIVE REWARDS
            scr_Inventory.dustNum += 50;

            //save health
            SaveLoad.currentGame.SetPlayerHealth(playerEntity._health.hp);
            SaveLoad.Save();
        }
        if (endCombat)
        {
            //INSERT CODE TO STOP ENEMY AND PLAYER MOVEMENT HERE
            if (Input.GetButton("Menu_Select") || Input.GetButton("Menu_Back"))
            {
                Debug.Log("Switching Scenes");
                SceneManager.LoadScene("sn_LocalMap");
            }
        }
	}

    public void UpdateHealth()
    {
        if (playerEntity._health.temp_hp > 0)
        {
            TempHealth.enabled = true;
            TempHealth.color = Color.yellow;
        }
        else TempHealth.enabled = false;
        TempHealth.text = "(+" + playerEntity._health.temp_hp + ")";
        PlayerHealth.text = "Health: " + playerEntity._health.hp;
        if(playerEntity._health.hp <= 0)
        {
            scr_InputManager.disableInput = true;
            RewardMessage.text = "Oh no you died! Press V to return to the Local Map";
            RewardMessage.enabled = true;
            endCombat = true;
        }
    }

    public void UpdateEffects()
    {
        if (showEffect)
        {
            EffectText.text = EffectString;
            EffectText.enabled = true;
        }
        else EffectText.enabled = false;
    }

    public void ChangeEffects(string text, float duration)
    {
        Debug.Log("NEW EFFECT");
        showEffect = true;
        EffectString = text;
        StartCoroutine(EffectTime(duration));
    }

    private IEnumerator EffectTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        showEffect = false;
    }
}
