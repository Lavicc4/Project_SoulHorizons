using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Brawler_Attack : MonoBehaviour {
	
	//Have a general cooldown to check in Update, then specific attack cooldowns depending on what the attack does?
	//Want to encourage the player to vary their attacks
	private bool busy = false; //use this to indicate that the script is mid-attack, so don't do another attack (Is this necessary?)
	/*Fury Swipe */
	private int meleeDamage = 5;
	private float meleeCooldown = 0.8f; //have these on separate cooldowns, so you can melee attack with the projectile in motion
	private bool meleeReady = true;
	/*Shoulder Dash */
	private int shoulderDamage = 8;
	private bool dashing = false; //set to true if dashing
	private float moveFrequency = 0.9f; //the pause between movements in the dash attack; used with a corroutine
	Vector2Int startPos = new Vector2Int(); //the point the dash starts at
	/*Tank Up */
	//private float tankDefenseBoost = 0.2f; //need to subtract this from health damage modifier; CANCELED for now
	private int tankShieldGain = 10;
	private float tankCooldown = 8f;
	private bool tankReady = true; //whether the tank move can be used currently
	/*Heavy Slam */

	//references
	scr_Entity entity; //use to get position

	void Awake()
	{
		entity = GetComponent<scr_Entity>();
	}

	void Start () {
        Debug.Log("Brawler attack added");
	}
	
	void Update () {
		int input = scr_InputManager.PlayCard();

		switch (input)
		{
		    case 0:
		    	HeavySlam();
			break;
		    case 1:
		    	TankUp();
		    	break;
		    case 2:
		    	ShoulderDash();
		    	break;
		    case 3:
		    	FurySwipe();
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
		Debug.Log("Furry Swipe!!");

		//check the grid position one over; if it contains an attackable entity, deal damage; note this will return null if the player is at the far right of the grid
		scr_Entity target = scr_Grid.GridController.GetEntityAtPosition(entity._gridPos.x + 1, entity._gridPos.y);

		if (target != null && target.type != EntityType.Player)
		{
			//deal damage
			target.HitByAttack(meleeDamage, entity.type);
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
		dashing = true;
		scr_InputManager.disableMovement = true;
		startPos.x = entity._gridPos.x; 
		startPos.y = entity._gridPos.y;
		StartCoroutine(Dash());
	}

	private IEnumerator Dash()
	{
		while (dashing && scr_Grid.GridController.LocationOnGrid(entity._gridPos.x + 1, entity._gridPos.y))
		{
			//move any enemies out of the way
			Push();

			//move
			entity.SetTransform(entity._gridPos.x + 1, entity._gridPos.y);
			//yield
			yield return new WaitForSeconds(moveFrequency);
		}

		dashing = false;
		scr_InputManager.disableMovement = false;
		entity.SetTransform(startPos.x, startPos.y);
	}

	/// <summary>
	/// Checks in front of the player and tries to move an enemy out of the way if there is one
	/// </summary>
	private void Push()
	{
		scr_Entity target = scr_Grid.GridController.GetEntityAtPosition(entity._gridPos.x + 1, entity._gridPos.y);
		int enemyX = entity._gridPos.x + 1;
		int enemyY = entity._gridPos.y;
		if (target != null && target.type == EntityType.Enemy)
		{
			Debug.Log("Found an enemy to push");
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
		entity._health.shield += tankShieldGain;
		
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

	}
}
