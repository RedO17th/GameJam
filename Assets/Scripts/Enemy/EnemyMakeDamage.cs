using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMakeDamage : MonoBehaviour
{
    private int _damage;

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        //тут будет нанесение урона
    }
}
