using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class scr_EventController : MonoBehaviour {

	public Button[] buttons = new Button[10]; 
	public int numberOfActiveEvents;

	//make another array of ints that stores which buttons have been completed
	//when generating events check ^ array, and if you get a hit, randomize again.

	//IDEA:  Multidimentional array [button,bool(completed)] if you can do that?!


	void Start () {


		//for loop to turn off all buttons
		for (int i = 0; i < buttons.Length; i++) {
			buttons [i].gameObject.SetActive (false);
		}


	}
	

	void Update () {


		if (Input.GetKeyDown (KeyCode.J)) {	
			CycleEvents (); 
		}


	}

	void CycleEvents(){

		//for loop to turn off all buttons
		for (int i = 0; i < numberOfActiveEvents; i++) {
			//int picked[] = new int[
			buttons [i].gameObject.SetActive (true);
		}
		/*
		int activate1 = Random.Range (0, buttons.Length);
		buttons [activate1].gameObject.SetActive (true);
		int activate2 = Random.Range (0, buttons.Length);
		buttons [activate2].gameObject.SetActive (true);
		int activate3 = Random.Range (0, buttons.Length);
		buttons [activate3].gameObject.SetActive (true);
		*/
	}
}
