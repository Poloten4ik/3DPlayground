using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private int damage = 40;

    private Collider col;
    private void Start()
    {
        col = GetComponent<Collider>();
        col.enabled = false;
    }

    public void AttackStart()
    {
        col.enabled = true;
    }

    public void AttackEnd()
    {
        col.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("trigger" + other.name);
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.DoDamage(damage);
        }
    }
}
