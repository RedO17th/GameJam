using System;
using System.Collections;
using System.Collections.Generic;
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
        SetNewOrder();
    }

    void Start()
    {
        UIManager.OnEnemyDeath += SetNewOrder;
    }

    
    void Update()
    {

    }


    private void SetNewOrder()
    {
        Order = Random.Range(0, MaxOrder);
        OnOrderChanged?.Invoke(Order);
    }
}
