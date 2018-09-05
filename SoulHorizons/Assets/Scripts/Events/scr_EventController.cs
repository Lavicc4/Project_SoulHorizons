using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class scr_EventController : MonoBehaviour {

	public Button[] buttons = new Button[10]; 
	public int numberOfActiveEvents;
	 

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
		if (Input.GetKeyDown (KeyCode.E)) {	
			Debug.Log(TestingGeneration (0, 10,3)); 
		}



	}

	void CycleEvents(){


		for (int i = 0; i < buttons.Length; i++) {
			buttons [i].gameObject.SetActive (false);
		}

		int[] temp = new int[numberOfActiveEvents];

		temp = TestingGeneration (0, buttons.Length, numberOfActiveEvents);
		for(int i = 0; i < temp.Length;i++){
			
		}

	}

		
	int[] TestingGeneration(int min, int max, int length){
		int[] testArray = new int[length];
		HashSet<int> h = new HashSet<int>();

		while (h.Count < testArray.Length){
			h.Add(Random.Range(min, max));
			h.CopyTo(testArray);
		}
		return testArray;


	}
}
