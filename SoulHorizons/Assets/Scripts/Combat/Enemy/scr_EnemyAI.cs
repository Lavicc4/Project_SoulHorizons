using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_EnemyAI : MonoBehaviour {


    [SerializeField]
    float movementInterval;

    int spawnX;
    int spawnY;
    scr_Grid grid;
    GameObject enemyGridController; 


	void Awake () {

        enemyGridController = GameObject.FindGameObjectWithTag("EnemyGridController");
        grid = enemyGridController.GetComponent<scr_Grid>(); 

	}
	
	
	void Update () {
        
	}




    void Attack1()
    {


    }

    void Attack2()
    {

    }

    IEnumerator Movement()
    {
        yield return new WaitForSeconds(movementInterval);
    }
}
