using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Brawler_Attack : MonoBehaviour {
	
	//Have a general cooldown to check in Update, then specific attack cooldowns depending on what the attack does?
	//Want to encourage the player to vary their attacks
	private int meleeDamage = 5;
	private float meleeCooldown = 0.8f; //have these on separate cooldowns, so you can melee attack with the projectile in motion
	private int shoulderDamage = 8;
	private bool dashing = false; //set to true if dashing
	private float moveFrequency = 1f; //the pause between movements in the dash attack; used with a corroutine
	//private float tankDefenseBoost = 0.2f; //need to subtract this from health damage modifier; CANCELED for now
	private int tankShieldGain = 10;

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
		//play any effects

		//check the grid position one over; if it contains an attackable entity, deal damage
		scr_Entity target = scr_Grid.GridController.GetEntityAtPosition(entity._gridPos.x + 1, entity._gridPos.y);

		if (target != null && target.type != EntityType.Player)
		{
			//deal damage
			target.HitByAttack(meleeDamage, entity.type);
		}

	}

	/// <summary>
	/// Start a dash attack.
	/// </summary>
	private void ShoulderDash()
	{
		dashing = true;
		scr_InputManager.disableMovement = true;
	}

	/// <summary>
	/// 
	/// </summary>
	private void TankUp()
	{

	}

	private void HeavySlam()
	{

	}
}
