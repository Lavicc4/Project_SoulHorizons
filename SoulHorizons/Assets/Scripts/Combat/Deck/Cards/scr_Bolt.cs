using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Cards/Bolt")]
public class scr_Bolt : scr_Card {

    public Attack boltAttack;

    public override void Activate()
    {
        Debug.Log("Activating Bolt");
        ActivateEffects();

        scr_Entity player = GameObject.FindGameObjectWithTag("Player").GetComponent<scr_Entity>();

        //add attack to attack controller script
        scr_AttackController.attackController.AddNewAttack(boltAttack, player._gridPos.x, player._gridPos.y, player);
    }

    public override void StartCastingEffects()
    {
        
    }

        protected override void ActivateEffects()
    {
        //put start effects here
    }

}
