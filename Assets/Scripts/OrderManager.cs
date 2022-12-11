using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderManager : MonoBehaviour
{
    public static int Order { get; private set; }

    //�������������� ��������� ������� �����
    private const int MaxOrder = 3;

    public event Action<int> OnOrderChanged;

    //��� �����
    public event Action OnEnemyDeath;

    private void Awake()
    {
        SetNewOrder();
    }

    void Start()
    {
        OnEnemyDeath += SetNewOrder;
    }

    
    void Update()
    {
        //��� �����
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            OnEnemyDeath?.Invoke();
            Debug.Log(Order);
        }
    }


    private void SetNewOrder()
    {
        Order = Random.Range(0, MaxOrder);
        OnOrderChanged?.Invoke(Order);
    }
}
