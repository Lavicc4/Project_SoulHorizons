﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Brawler_Attack : MonoBehaviour {
	//--Art assets--
	public GameObject particle_heavySlam;	
	public GameObject particle_furySwipe;
	private float swipe_yOffset = 0.2f; //an offset to move the effect higher
	
	//Have a general cooldown to check in Update, then specific attack cooldowns depending on what the attack does?
	//Want to encourage the player to vary their attacks
	private bool busy = false; //use this to indicate that the script is mid-attack, so don't do another attack (Is this necessary?)
                               /*Fury Swipe */
    private int meleeDamage = 8;
	private float meleeCooldown = 0.4f; //have these on separate cooldowns, so you can melee attack with the projectile in motion
	private bool meleeReady = true;

	/*Shoulder Dash */
	private int shoulderDamage = 15;
	private bool dashing = false; //set to true if dashing
	private float moveFrequency = 0.35f; //the pause between movements in the dash attack; used with a corroutine
	Vector2Int startPos = new Vector2Int(); //the point the dash starts at

	/*Tank Up */
	//private float tankDefenseBoost = 0.2f; //need to subtract this from health damage modifier; CANCELED for now
	private int tankShieldGain = 10;
	private float tankCooldown = 8f;
	private bool tankReady = true; //whether the tank move can be used currently

	/*Heavy Slam */
	private int slamDamage = 30; //the starting amount of damage dealt
    private int slamDamageMax; //Max Slam Damage Reference
    private int slamDamageDeprecation = 10; //Slam deprecation per column movement
	private float slamCooldown = 8f;    //Player Slam CD
	private bool slamReady = true;
	private float slamMoveCooldown  = 0.004f;

	//references
	scr_Entity playerEntity; //use to get position

	void Awake()
	{
		playerEntity = GetComponent<scr_Entity>();
	}

	void Start () {
        Debug.Log("Brawler attack added");
        slamDamageMax = slamDamage;
    }
	
	void Update () {
		int input = scr_InputManager.PlayCard();

		switch (input)
		{
		    case 0:
				ShoulderDash();
			break;
		    case 1:
		    	TankUp();
		    	break;
		    case 2:
		    	FurySwipe();
		    	break;
		    case 3:
				HeavySlam();
		    	break;
		}
	}

	/// <summary>
	/// Attack the space in front of the player.
	/// </summary>
	private void FurySwipe()
	{
		if (!meleeReady)
		{
			return;
		}

		//play any effects
		Instantiate(particle_furySwipe, scr_Grid.GridController.GetWorldLocation(playerEntity._gridPos.x + 1, playerEntity._gridPos.y), particle_furySwipe.transform.rotation);
        scr_Grid.GridController.BriefActivateTile(playerEntity._gridPos.x + 1, playerEntity._gridPos.y, 0.1f);

		//check the grid position one over; if it contains an attackable entity, deal damage; note this will return null if the player is at the far right of the grid
		scr_Entity target = scr_Grid.GridController.GetEntityAtPosition(playerEntity._gridPos.x + 1, playerEntity._gridPos.y);

		if (target != null && target.type != EntityType.Player)
		{
			//deal damage
			target.HitByAttack(meleeDamage, playerEntity.type);
		}

		//start the cooldown
		StartCoroutine(FurySwipeCooldown());

	}

		private IEnumerator FurySwipeCooldown()
	{
		meleeReady = false;
		yield return new WaitForSeconds(meleeCooldown);
		meleeReady = true;
	}

	/// <summary>
	/// Start a dash attack.
	/// </summary>
	private void ShoulderDash()
	{
		if (dashing)
		{
			return;
		}

		dashing = true;
		meleeCooldown -= 0.3f; //speed up the attack rate while dashing
		scr_InputManager.disableMovement = true;
		startPos.x = playerEntity._gridPos.x; 
		startPos.y = playerEntity._gridPos.y;
		StartCoroutine(Dash());
	}

	private IEnumerator Dash()
	{
		while (dashing && scr_Grid.GridController.LocationOnGrid(playerEntity._gridPos.x + 1, playerEntity._gridPos.y))
		{
			//move any enemies out of the way
			Push();

			//move
			playerEntity.SetTransform(playerEntity._gridPos.x + 1, playerEntity._gridPos.y);
			//yield
			yield return new WaitForSeconds(moveFrequency);
		}

		dashing = false;
		meleeCooldown += 0.3f; //slow the attack rate back to normal
		scr_InputManager.disableMovement = false;
		playerEntity.SetTransform(startPos.x, startPos.y);
	}

	/// <summary>
	/// Checks in front of the player and tries to move an enemy out of the way if there is one
	/// </summary>
	private void Push()
	{
		scr_Entity target = scr_Grid.GridController.GetEntityAtPosition(playerEntity._gridPos.x + 1, playerEntity._gridPos.y);
		int enemyX = playerEntity._gridPos.x + 1;
		int enemyY = playerEntity._gridPos.y;
		if (target != null && target.type == EntityType.Enemy)
		{
			Debug.Log("Found an enemy to push");
            //deal damage
            target.HitByAttack(shoulderDamage, playerEntity.type);
            //push the enemy out of the way if possible
            if (MoveIfOpen(target, enemyX+1, enemyY+1)) //check below and to the right the enemy
			{
				return;
			}

			if (MoveIfOpen(target, enemyX+1, enemyY-1)) //check above and to the right the enemy
			{
				return;
			}

			if (MoveIfOpen(target, enemyX, enemyY+1)) //check below the enemy
			{
				return;
			}

			if (MoveIfOpen(target, enemyX, enemyY-1)) //check above the enemy
			{
				return;
			}

			//concede defeat if we get to here
		}
	}

	/// <summary>
	/// Move the target to the location if it is open and unoccupied; returns success or failure
	/// </summary>
	/// <param name="target"></param>
	/// <param name="x"></param>
	/// <param name="y"></param>
	private bool MoveIfOpen(scr_Entity target, int x, int y)
	{
			if (scr_Grid.GridController.IsTileUnoccupied(x, y))
			{
				//move the enemy
				Debug.Log("Moving the enemy");
				target.SetTransform(x, y);
				return true;
			}
			return false;
	}

	/// <summary>
	/// Adds shield to the player.
	/// </summary>
	private void TankUp()
	{
		if (!tankReady)
		{
			return;
		}
		playerEntity._health.shield += tankShieldGain;
		
		//start the cooldown
		StartCoroutine(TankCooldown());
	}

	private IEnumerator TankCooldown()
	{
		tankReady = false;
		yield return new WaitForSeconds(tankCooldown);
		tankReady = true;
	}

	/// <summary>
	/// Hits all of the columns left to right.
	/// </summary>
	private void HeavySlam()
	{
		if (!slamReady)
		{
			return;
		}
		//start a coroutine which hits a column every __ seconds
		StartCoroutine(HeavySlamRoutine());
	}

	private IEnumerator HeavySlamRoutine()
	{
		int column = playerEntity._gridPos.x + 1;
		while (slamDamage > 0 && scr_Grid.GridController.LocationOnGrid(column, 0)) //while the damage has not reduced to zero and we haven't gone off the edge of the grid
		{
			//check each column and see if it has enemies
			bool enemySpaceFound = false; //only yield if we actually attacked an enemy space
			//iterate through the tiles in this column
			for (int i = 0; i < scr_Grid.GridController.ySizeMax; i++)
			{
				if (slamDamage > 0)
				{
                    //create the attack effect at this space
                    GameObject particle =  Instantiate(particle_heavySlam, scr_Grid.GridController.GetWorldLocation(column, i), particle_heavySlam.transform.rotation);
                    if (slamDamage == slamDamageMax - slamDamageDeprecation)
                    {
                        //second row
                        particle.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    }
                    else if (slamDamage == slamDamageMax - 2 * slamDamageDeprecation)
                    {
                        //third row
                        particle.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    }
                    scr_Grid.GridController.BriefActivateTile(column, i, 0.28f);

                }

				if (scr_Grid.GridController.grid[column,i].territory.name == TerrName.Enemy)
				{
					enemySpaceFound = true;
					//play some effect on this tile to indicate it was attacked

					//attack an enemy if there is one here
					scr_Entity target = scr_Grid.GridController.GetEntityAtPosition(column, i);
					if (target != null)
					{
						//deal damage
						target.HitByAttack(slamDamage, EntityType.Player);
						Debug.Log("Heavy Slam hit something for: " + slamDamage + "\n Player column position is: " + column);
					}
				}
			}

			//yield if we attacked an enemy space
			if (slamDamage > 0)
			{
				slamDamage -= slamDamageDeprecation; //heavy slam does less damage as it moves right
                //Debug.Log("Slam damage is: " + slamDamage + "\n Column position is: " + playerEntity._gridPos.y);
                yield return new WaitForSeconds(slamMoveCooldown);
		    }
			//move to the next column
			column++;
		}

		//end of  heavy slam
		slamDamage = slamDamageMax;
		//start main cooldown
		StartCoroutine(SlamCooldown());
	}

	private IEnumerator SlamCooldown()
	{
		slamReady = false;
		yield return new WaitForSeconds(slamCooldown);
		slamReady = true;
	}
}
