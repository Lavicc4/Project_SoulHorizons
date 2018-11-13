using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scr_statemanager : MonoBehaviour {

    public Text RewardMessage;
    public Image rewardPanel; 
    //public Text PlayerHealth;
    public Text Shield;
    public Text EffectText;
    public Text StaminaText;
    bool endCombat = false;
    bool showEffect = false;
    string EffectString;
    GameObject player;
    scr_Entity playerEntity;
    scr_PlayerMovement playerMovement;
    // Use this for initialization
    void Start () {
        rewardPanel.enabled = false; 
        player = GameObject.FindGameObjectWithTag("Player");
        EffectText.enabled = false;
        if (player != null)
        {
            playerEntity = player.GetComponent<scr_Entity>();
            playerMovement = player.GetComponent<scr_PlayerMovement>();
            //load the health from the GameState
            int hp = SaveLoad.currentGame.GetPlayerHealth();
            if (hp > 0) //make sure the health has been set previously
            {
                playerEntity._health.hp = hp;
            }

            //make sure that movement is enabled
            scr_InputManager.disableInput = false;
            scr_InputManager.disableMovement = false;

            //load the stamina from the player
            StaminaText.text = "Stamina: " + playerMovement.GetStaminaCharges();
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
		if(!endCombat && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            //Debug.Log("NO ENEMIES");
            //scr_InputManager.disableInput = true;
            RewardMessage.enabled = true;
            rewardPanel.enabled = true; 
            endCombat = true;

            //GIVE REWARDS
            scr_Inventory.dustNum += 50;

            //save health
            SaveLoad.currentGame.SetPlayerHealth(playerEntity._health.hp);
            Debug.Log("DUST AMOUNT: " + SaveLoad.currentGame.GetDustAmount());
        }
        if (endCombat)
        {
            //INSERT CODE TO STOP ENEMY AND PLAYER MOVEMENT HERE
            if (Input.GetButton("Menu_Select") || Input.GetButton("Menu_Back"))
            {
                Debug.Log("Switching Scenes");
                SaveLoad.Save();
                SceneManager.LoadScene("sn_LocalMap");
            }
        }
	}

    public void UpdateHealth()
    {
        if (playerEntity._health.shield > 0)
        {
            Shield.enabled = true;
            Shield.color = Color.yellow;
        }
        else Shield.enabled = false;
        Shield.text = "(+" + playerEntity._health.shield + ")";
        //PlayerHealth.text = "Health: " + playerEntity._health.hp;
        StaminaText.text = "Stamina: " + playerMovement.GetStaminaCharges();
        if(playerEntity._health.hp <= 0)
        {
            scr_InputManager.disableInput = true;
            RewardMessage.text = "Oh no you died! Press V to return to the Local Map";
            RewardMessage.enabled = true;
            rewardPanel.enabled = true; 
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
