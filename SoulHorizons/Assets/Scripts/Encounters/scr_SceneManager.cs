using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class scr_SceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject); 
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
