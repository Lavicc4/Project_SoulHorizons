//Colin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
[RequireComponent(typeof(AudioSource))]

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
	public float fireRate = 0.2f; //how often the player can fire the blaster
	public float chargeCooldown = 0.4f; //we can use this instead of fire rate after a charged shot if we want a longer cooldown for charged shots
	private bool readyToFire = true; //used to indicate if the blaster is ready to fire again
	public Attack attack; //the attack that will be launched
	private scr_Entity playerEntity;

	public SpriteRenderer baseProjectile;
	public SpriteRenderer projectile1;

    AudioSource Blaster_SFX;
    //AudioSource Die_SFX;
    public AudioClip[] blasters_SFX;
    private AudioClip blaster_SFX;
    //public AudioClip[] dies_SFX;
    //private AudioClip die_SFX;


    void Awake()
	{
		//objectPool_scr = GetComponent<scr_ObjectPool>();
		playerEntity = GetComponent<scr_Entity>();
	}
	
	void Start () {
		pressed = false;
        AudioSource[] SFX_Sources = GetComponents<AudioSource>();
        Blaster_SFX = SFX_Sources[4];
        //Die_SFX = SFX_Sources[3];
    }
	
	
	void Update () {

		if (pressed)
		{
			timePressed += Time.deltaTime;
			//TODO:need to calculate charge level here for visual indicators that you have increased the charge level
		}

		if (scr_InputManager.Blast_Down() && readyToFire)
		{
            int index = Random.Range(0, blasters_SFX.Length);
            blaster_SFX = blasters_SFX[index];
            Blaster_SFX.clip = blaster_SFX;
            Blaster_SFX.Play();

            pressed = true;
		}

		if(scr_InputManager.Blast_Up() && readyToFire)
		{
			//scr_PlayerProjectile proj = objectPool_scr.CreateObject(transform.position, transform.rotation).GetComponent<scr_PlayerProjectile>();
			float damage = baseDamage + damageIncreaseRate * timePressed;
			//check if charged
			if (timePressed < chargeTime1)
			{
				//fire a normal shot

				//set the damage for the attack
				attack.damage = (int) Mathf.Round(damage);
				//set the projectile sprite
				attack.particles = baseProjectile;
				scr_AttackController.attackController.AddNewAttack(attack, playerEntity._gridPos.x, playerEntity._gridPos.y, playerEntity);
				StartCoroutine(AttackCooldown(fireRate));
			}
			else
			{
                //fire a shot at charge level 1
                CameraShaker.Instance.ShakeOnce(2f, 2f, 0.2f, 0.2f);
				damage += damageIncrease1;
				attack.damage = (int) Mathf.Round(damage);
				//set the projectile sprite
				attack.particles = projectile1;
				//proj.Fire(damage, 1, baseSpeed);
				scr_AttackController.attackController.AddNewAttack(attack, playerEntity._gridPos.x, playerEntity._gridPos.y, playerEntity);
				StartCoroutine(AttackCooldown(chargeCooldown));
                
			}

			//reset variables
			timePressed = 0f;
			pressed = false;
		}
	}//end Update

	/// <summary>
	/// Called after an attack to disable the blaster for the cooldown time
	/// </summary>
	/// <returns></returns>
	private IEnumerator AttackCooldown(float cooldown)
	{
		readyToFire = false;
		yield return new WaitForSeconds(cooldown);
		readyToFire = true;
	}
}
