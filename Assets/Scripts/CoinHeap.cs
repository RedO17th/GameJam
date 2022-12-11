using UnityEngine;
using static ToonyColorsPro.ShaderGenerator.Enums;

public class CoinHeap : MonoBehaviour
{
    private int _goldCount;

    public int GoldCount => _goldCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.SendGoldChanged(_goldCount);

            Destroy(gameObject);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Player"))
    //    {
    //        EventManager.SendGoldChanged(_goldCount);

    //        Destroy(gameObject);
    //    }
    //}
}
