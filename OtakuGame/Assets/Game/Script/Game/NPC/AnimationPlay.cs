using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AnimationPlay : MonoBehaviour
{
    public Animator snorlax;
    public Animator bell;

    public void OnClickSleep()
    {
        snorlax.SetBool("sleep", true);
    }

    public void OnClickNoSleep()
    {
        snorlax.SetBool("sleep", false);
    }

    public void OnClickFire()
    {
        bell.SetTrigger("fire");
    }
}
