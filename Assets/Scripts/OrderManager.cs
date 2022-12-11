using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderManager : MonoBehaviour
{
    public static int Order { get; private set; }

    //«ахардкоженна€ константа размера типов
    private const int MaxOrder = 3;

    public static event Action<int> OnOrderChanged;

    private void Awake()
    {
        SetStartOrder();
    }

    private void OnEnable()
    {
        EventManager.OnEnemyKilled.AddListener(SetNewOrder);
    }

    //void Start()
    //{
    //    UIManager.OnEnemyDeath += SetNewOrder;
    //}

    private void SetStartOrder()
    {
        Order = Random.Range(0, MaxOrder);
        OnOrderChanged?.Invoke(Order);
    }

    private void SetNewOrder(Enemy enemy)
    {
        Order = Random.Range(0, MaxOrder);
        OnOrderChanged?.Invoke(Order);
    }
}
