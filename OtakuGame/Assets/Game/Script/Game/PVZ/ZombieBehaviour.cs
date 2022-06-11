using UnityEngine;
using System.Collections;

public class ZombieBehaviour : MonoBehaviour
{
    public float speed;
    public float hpMax;
    public float attack;

    private float _hp;

    [Range(0.25f, 2f)]
    public float attackRate = 1;
    private float _attackTimer;

    void Start()
    {
        _hp = hpMax;
        _attackTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var plant = GetCurrentSpotPlant();
        if (plant == null)
        {
            Move();
        }
        else
        {
            Attack(plant);
        }
    }

    private void Move()
    {
        transform.position += -Vector3.right * Time.deltaTime;
    }


    private void Attack(PlantBehaviour plant)
    {
        if (_attackTimer <= 0)
        {
            plant.OnAttacked(attack);
            _attackTimer = attackRate;
            return;
        }

        _attackTimer -= Time.deltaTime;
    }

    private PlantBehaviour GetCurrentSpotPlant()
    {
        return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ZombieWin")
        {
            PvzService.instance.Loose();
        }
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
        Debug.Log("Die");
    }
}
