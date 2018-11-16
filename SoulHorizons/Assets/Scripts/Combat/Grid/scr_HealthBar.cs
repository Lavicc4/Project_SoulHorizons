using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 


public class scr_HealthBar : MonoBehaviour {

    float _health;
    float _maxHp;
    float _shield;
    public GameObject pivot;
    public GameObject bluePivot;
    public TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI shieldDisplay; 
    

	void Start () {
        _health = GetComponentInParent<scr_Entity>()._health.hp;
        _shield = GetComponentInParent<scr_Entity>()._health.shield; 
	}
	
	void Update () {
        _health = GetComponentInParent<scr_Entity>()._health.hp;
        _maxHp = GetComponentInParent<scr_Entity>()._health.max_hp;
        _shield = GetComponentInParent<scr_Entity>()._health.shield;
        pivot.transform.localScale = new Vector3(_health/_maxHp, 1,1);

        if(bluePivot != null)
        {
            bluePivot.transform.localScale = new Vector3(_shield / _maxHp, 1, 1);
            healthDisplay.text = _health.ToString();
            shieldDisplay.text = _shield.ToString();
            if(_shield == 0)
            {
                shieldDisplay.enabled = false; 
            }
            else
            {
                shieldDisplay.enabled = true; 
            }
        }
        
    }
}
