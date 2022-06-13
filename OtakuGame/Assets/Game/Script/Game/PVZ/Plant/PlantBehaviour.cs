using UnityEngine;
using System.Collections;

public class PlantBehaviour : MonoBehaviour
{
    public float hpMax;

    private float _hp;
    // Use this for initialization
    void Start()
    {
        _hp = hpMax;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnAttacked(float dmg)
    {
        _hp -= dmg;
        Debug.Log("OnAttacked " + _hp + "/" + hpMax);
        if (_hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("plant Die");
        Destroy(gameObject);
    }
}
