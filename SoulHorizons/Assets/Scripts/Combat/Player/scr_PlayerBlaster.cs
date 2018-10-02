﻿//Colin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the player blaster functionality. Receives user input and passes information to the projectile when firing.
/// </summary>
public class scr_PlayerBlaster : MonoBehaviour {

	public float baseDamage = 5f;
	public float baseSpeed = 10f; //the speed at which the projectile moves
	public float chargeTime1 = 1.2f; //time in seconds for the blast to reach charge level 1
	public float damageIncrease1 = 8f; //the amount that damage dealt increases when charge level 1 is increased
	public float damageIncreaseRate = 3f; //how much damage increases per second. This increase is independent of the charge level damage increase. Set this to 0 to have only the charge level increase.

	private float timePressed = 0f; //the time the fire button has been held
	private bool pressed;
	//private scr_ObjectPool objectPool_scr;

	public Attack attack; //the attack that will be launched
	private scr_Entity playerEntity;

	public SpriteRenderer baseProjectile;
	public SpriteRenderer projectile1;
	

	void Awake()
	{
		//objectPool_scr = GetComponent<scr_ObjectPool>();
		playerEntity = GetComponent<scr_Entity>();
	}
	
	void Start () {
		pressed = false;
	}
	
	
	void Update () {

		if (pressed)
		{
			timePressed += Time.deltaTime;
			//TODO:need to calculate charge level here for visual indicators that you have increased the charge level
		}

		if (scr_InputManager.Blast_Down())
		{
			pressed = true;
		}

		if(scr_InputManager.Blast_Up())
		{
			//scr_PlayerProjectile proj = objectPool_scr.CreateObject(transform.position, transform.rotation).GetComponent<scr_PlayerProjectile>();
			float damage = baseDamage + damageIncreaseRate * timePressed;
			//check if charged
			if (timePressed < chargeTime1)
			{
				//fire a normal shot
				//proj.Fire(damage, 0, baseSpeed);
				//set the damage for the attack
				attack.damage = (int) Mathf.Round(damage);
				//set the projectile sprite
				attack.particles = baseProjectile;
				scr_AttackController.attackController.AddNewAttack(attack, playerEntity._gridPos.x, playerEntity._gridPos.y, playerEntity);
			}
			else
			{
				//fire a shot at charge level 1
				damage += damageIncrease1;
				attack.damage = (int) Mathf.Round(damage);
				//set the projectile sprite
				attack.particles = projectile1;
				//proj.Fire(damage, 1, baseSpeed);
				scr_AttackController.attackController.AddNewAttack(attack, playerEntity._gridPos.x, playerEntity._gridPos.y, playerEntity);

			}

			timePressed = 0f;
			pressed = false;
		}
	}//end Update
}
