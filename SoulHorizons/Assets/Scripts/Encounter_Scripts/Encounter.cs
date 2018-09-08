using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Encounter", menuName = "Encounter")]
public class Encounter : ScriptableObject {

	public new string name;  
	public bool completed; 
	public bool active;
	public GameObject[] enemies = new GameObject[5];

	 



}
