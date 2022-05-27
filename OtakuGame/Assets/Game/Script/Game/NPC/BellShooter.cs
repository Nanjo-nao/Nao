using UnityEngine;
using System.Collections;

public class BellShooter : MonoBehaviour
{
    public GameObject bullet;

    public Transform muzzle;

    [Range(0.25f, 2f)]
    public float attackRate = 1;

    public void Fire()
    {
        var bulletGo = Instantiate(bullet, muzzle.position, muzzle.rotation, transform.parent);
        bulletGo.SetActive(true);
    }
}
