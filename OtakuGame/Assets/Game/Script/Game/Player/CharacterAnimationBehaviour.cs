using UnityEngine;
using System.Collections;

public class CharacterAnimationBehaviour : MonoBehaviour
{
    public Animator animator;

    public void StartWalk()
    {
        if (!animator.GetBool("walk"))
        {
            Debug.Log("StartWalk");
            animator.SetBool("walk", true);
        }
    }

    public void StopWalk()
    {
        if (animator.GetBool("walk"))
        {
            Debug.Log("StopWalk");
            animator.SetBool("walk", false);
        }
    }

    public void StartKnee()
    {
        animator.SetTrigger("knee");
    }

    public void StopKnee()
    {
        animator.SetTrigger("unknee");
    }

    public void Happy()
    {
        animator.SetTrigger("happy");
    }

    public void Unhappy()
    {
        animator.SetTrigger("unhappy");
    }
}
