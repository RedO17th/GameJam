using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    [SerializeField] Enemy Enemy;

    private void OnTriggerEnter(Collider other)
    {
        Enemy.TakeDamage();
    }
}
