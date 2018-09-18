using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class scr_SceneManager : MonoBehaviour {

    public Encounter currentEncounter;
    public static scr_SceneManager globalSceneManager; 

	
	void Start () {

        if(globalSceneManager != null && globalSceneManager != this)
        {
            Destroy(gameObject); 
        }
        else
        {
            globalSceneManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
       

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeScene(string sceneName){
		SceneManager.LoadScene (sceneName);
	}

    public void EnableSettings()
    {
        Debug.Log("SETTINGS ARE OPEN");

    }
    public void DisableSettings()
    {
        Debug.Log("settings are closed");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quit"); 
    }
}
