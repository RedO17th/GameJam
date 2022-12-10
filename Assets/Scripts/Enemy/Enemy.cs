using System.Collections;
using UnityEngine;

public enum EnemyType
{
    TypeA,
    TypeB,
    TypeC
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType _type;
    [SerializeField] private int _weigth;
    [SerializeField] private int _price;

    [Space]
    [SerializeField] private int _damage;
    [SerializeField] private EnemyMakeDamage _makeDamage;
    [Space]
    [SerializeField] private float _invulnerableTime = 0.5f;
    [SerializeField] private float _speedBoostPercent = 0.15f;

    public EnemyType Type => _type;
    public int Weight => Weight;

    private EnemyMove _enemyMove;
    private bool _invulnerable;

    private void Awake()
    {
        _enemyMove = GetComponent<EnemyMove>();
        _makeDamage.SetDamage(_damage);
    }

    public void SetTarget(Transform targetTransform)
    {
        _enemyMove.SetTarget(targetTransform);
    }

    [ContextMenu("TakeDamage")]
    public void TakeDamage()
    {
        if (_invulnerable)
            return;

        if (Order.TargetType == (int)Type)
        {
            Die();
        }
        else
        {
            Boost();
        }
    }

    private void Die()
    {
        StartCoroutine(DieCoroutine());
    }

    IEnumerator DieCoroutine()
    {
        yield return null;
    }

    private void Boost()
    {
        StartCoroutine(BoostCoroutine());
    }

    IEnumerator BoostCoroutine()
    {
        _invulnerable = true;

        yield return new WaitForSeconds(_invulnerableTime);

        _enemyMove.RaiseSpeed(_speedBoostPercent);

        _invulnerable = false;
    }
}
