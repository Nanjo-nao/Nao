using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1, 10)]
    public float speed = 5;

    public float lifeTime;

    public float damage;

    public GameObject hitVFX;

    void Update()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Zombie")
        {
            var zombie = col.transform.GetComponent<ZombieBehaviour>();
            zombie.OnAttacked(damage);

            var hit = Instantiate(hitVFX, transform.position, Quaternion.identity, transform.parent);
            hit.SetActive(true);
            Destroy(hit, 2);
            Destroy(gameObject);
        }
    }
}
