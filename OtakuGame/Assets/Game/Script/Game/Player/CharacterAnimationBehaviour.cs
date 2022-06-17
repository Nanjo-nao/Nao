using UnityEngine;
using System.Collections;

public class CharacterAnimationBehaviour : MonoBehaviour
{
    public Animator animator;

    public void StartWalk()
    {
        animator.SetBool("walk", true);
    }

    public void StopWalk()
    {
        animator.SetBool("walk", false);
    }

    public void StartKnee()
    {
        animator.SetTrigger("knee");
    }

    public void StopKnee()
    {
        animator.SetTrigger("unknee");
    }
}
