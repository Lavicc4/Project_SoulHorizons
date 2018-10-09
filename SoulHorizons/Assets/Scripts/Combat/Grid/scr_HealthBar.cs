using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_HealthBar : MonoBehaviour {

    float _health;
    float _maxHp;
    public GameObject pivot; 

	void Start () {
        _health = GetComponentInParent<scr_Entity>()._health.hp; 
	}
	
	void Update () {
        _health = GetComponentInParent<scr_Entity>()._health.hp;
        _maxHp = GetComponentInParent<scr_Entity>()._health.max_hp;
        pivot.transform.localScale = new Vector3(_health/_maxHp, 1,1);
    }
}
