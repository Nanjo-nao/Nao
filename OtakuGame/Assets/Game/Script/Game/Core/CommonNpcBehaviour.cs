using UnityEngine;

public class CommonNpcBehaviour : MonoBehaviour
{
    public Animator animator;
    public float speed = 4f;

    Vector3 dest;
    bool isWalking;

    public void GoTo(Vector3 d)
    {
        animator.SetBool("walk", true);
        dest = d;
        isWalking = true;
    }

    private void Update()
    {
        if (isWalking)
        {
            var dir = dest - transform.position;
            dir.y = 0;
            if (dir.magnitude < 0.1f)
            {
                Arrive();
                return;
            }
            RotateTo(dir);
            var s = dir.normalized * Time.deltaTime * speed;
            transform.position += s;
        }
    }

    void Arrive()
    {
        animator.SetBool("walk", false);
        isWalking = false;
    }

    void RotateTo(Vector3 dir)
    {
        transform.rotation = Quaternion.LookRotation(dir);
    }
}
