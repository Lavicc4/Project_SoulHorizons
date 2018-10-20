using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/SeizeDomain")]
public class scr_SeizeDomain : scr_Card
{
    public float duration;
    public Color newColor;
    public override void Activate()
    {
      
        ActivateEffects();
        Debug.Log("SEIZE!");
        scr_Grid.GridController.seizeColumn(TerrName.Player);
        
       

    }

    public override void StartCastingEffects()
    {

    }

    protected override void ActivateEffects()
    {
        //put start effects here
    }
}
