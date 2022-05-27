using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1, 10)]
    public float speed = 5;

    void Update()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
    }
}
