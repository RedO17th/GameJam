using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderManager : MonoBehaviour
{
    public static int Order { get; private set; }

    //«ахардкоженна€ константа размера типов
    private const int MaxOrder = 4;

    public static event Action OnOrderChanged;

    private void OnEnable()
    {
        EventManager.OnEnemyKilled.AddListener(SetNewOrder);
    }

    public static void SetStartOrder()
    {
        Order = Random.Range(0, MaxOrder);
    }

    private void SetNewOrder(Enemy enemy)
    {
        Order = Random.Range(0, MaxOrder);
        OnOrderChanged?.Invoke();
    }
}
