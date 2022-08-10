using UnityEngine;

public class CommonNpcBehaviour : MonoBehaviour
{
    public Animator animator;
    public float speed = 4f;
    public float speedFall = 7f;

    Vector3 _dest;
    bool _isWalking;
    bool _isFalling;
    bool _isFallingSuc;
    public ParticleSystem ps;
    bool _fallFailSoundPlayed;
    public void GoTo(Vector3 d)
    {
        animator.SetBool("walk", true);
        _dest = d;
        _isWalking = true;
    }

    public void FallTo(Vector3 d, bool suc)
    {
        Arrive();
        _dest = d;
        _isFalling = true;
        _isFallingSuc = suc;
        _fallFailSoundPlayed = false;

        animator.SetTrigger("jump");
    }

    public void Knee()
    {
        animator.SetTrigger("knee");
    }

    public void Unknee()
    {
        animator.SetTrigger("unknee");
    }

    private void Update()
    {
        if (_isWalking)
        {
            var dir = _dest - transform.position;
            dir.y = 0;
            if (dir.magnitude < 0.05f)
            {
                Arrive();
                return;
            }
            RotateTo(dir);
            var s = dir.normalized * Time.deltaTime * speed;
            transform.position += s;
            return;
        }

        if (_isFalling)
        {
            var dir = _dest - transform.position;
            dir.x = 0;
            dir.z = 0;

            if (!_fallFailSoundPlayed && dir.magnitude < 2.0f && !_isFallingSuc)
            {
                _fallFailSoundPlayed = true;
                com.SoundService.instance.Play("grtFall");
            }

            if (dir.magnitude < 0.05f)
            {
                ArriveFall();
                return;
            }

            var s = dir.normalized * Time.deltaTime * speedFall;
            transform.position += s;
            return;
        }
    }

    void Arrive()
    {
        animator.SetBool("walk", false);
        _isWalking = false;
    }

    void ArriveFall()
    {
        if (_isFallingSuc)
        {
            animator.SetTrigger("unjump");
        }
        else
        {
            animator.SetTrigger("die");
            PlayPs();
        }
        _isFalling = false;
    }

    public void ResetMove()
    {
        animator.SetTrigger("reset");
        _isFalling = false;
        _isWalking = false;
    }

    public void Happy()
    {
        animator.SetTrigger("happy");
    }

    void RotateTo(Vector3 dir)
    {
        transform.rotation = Quaternion.LookRotation(dir);
    }

    public void Face(Vector3 position)
    {
        var dir = position - transform.position;
        dir.y = 0;
        RotateTo(dir);
    }

    public void PlayPs()
    {
        if (ps != null)
            ps.Play();
    }
}
