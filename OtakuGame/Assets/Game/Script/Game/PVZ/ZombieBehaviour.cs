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

    public GameObject dieVFX;
    private PlantBehaviour _currentPlant;
    public Animator animator;
    public Collider col;

    void Start()
    {
        _hp = hpMax;
        _attackTimer = 0;
        _currentPlant = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (_hp <= 0)
        {
            transform.position += Vector3.up * (-0.5f * Time.deltaTime);
            return;
        }

        if (_currentPlant == null)
        {
            Move();
        }
        else
        {
            Attack(_currentPlant);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ZombieWin")
        {
            PvzService.instance.Loose();
        }
        if (other.tag == "Plant")
        {
            _currentPlant = other.GetComponent<PlantBehaviour>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Plant")
        {
            _currentPlant = null;
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
        var hit = Instantiate(dieVFX, transform.position, Quaternion.identity, transform.parent);
        hit.SetActive(true);
        animator.SetTrigger("die");
        Destroy(hit, 2);
        Destroy(gameObject, 3);
        col.enabled = false;
    }
}
