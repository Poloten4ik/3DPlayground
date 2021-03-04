using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int health = 100;

    Animator anim;
    Damageable damageable;

    private void Start()
    {
        anim = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();

        damageable.OnRecieveDamage += ReciveDamage;
    }

    private void ReciveDamage(int damage)
    {
        anim.SetTrigger("Hit");
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
