using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_AnimController : MonoBehaviour {

    public Animator anim;
    //ANIMATION METHODS
    public void fadeIn()
    {
        if (anim != null)
        {
            anim.SetInteger("Movement", -1);
        }
    }

    public void backToIdle()
    {
        if (anim != null)
        {
            anim.SetInteger("Movement", 0);
        }
    }
}
