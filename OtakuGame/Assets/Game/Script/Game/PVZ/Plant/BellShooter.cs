using UnityEngine;
using System.Collections;

public class BellShooter : MonoBehaviour
{
    public GameObject bullet;

    public Transform muzzle;
    public Animator animator;

    [Range(0.25f, 2f)]
    public float attackRate = 1;
    private float _attackTimer;

    private void Start()
    {
        _attackTimer = attackRate;
    }

    public void Fire()
    {
        com.SoundService.instance.Play("shootBullet");
        var bulletGo = Instantiate(bullet, muzzle.position, muzzle.rotation, transform.parent);
        bulletGo.SetActive(true);
    }

    private void Attack()
    {
        if (_attackTimer <= 0)
        {
            animator.SetTrigger("fire");
            _attackTimer = attackRate;
            return;
        }

        _attackTimer -= Time.deltaTime;
    }

    private void Update()
    {
        Attack();
    }
}
