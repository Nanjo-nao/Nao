using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1, 10)]
    public float speed = 5;

    public float lifeTime;

    public float damage;

    public GameObject dieVFX;

    void Update()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag =="Zombie")
        {
            var zombie = collision.transform.GetComponent<ZombieBehaviour>();
            zombie.OnAttacked(damage);

            Instantiate(dieVFX, transform.position, Quaternion.identity, transform.parent);
            Destroy(gameObject);
        }
    }
}
