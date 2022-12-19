using Newtonsoft.Json.Linq;
using UnityEngine;

public class CoinHeap : MonoBehaviour
{
    private int _goldCount;

    private void OnTriggerEnter(Collider other)
    {
        //Не использовать на практике Теги
        if (other.CompareTag("Player"))
        {
            //EventManager.SendGoldChanged(_goldCount);

            //other.GetComponent<BasePlayer>().AddGold(_goldCount);
            BasePlayer.AddGold(_goldCount);
            Destroy(gameObject);
        }
    }

    public void SetGoldCount(int value) => _goldCount = value;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Player"))
    //    {
    //        EventManager.SendGoldChanged(_goldCount);

    //        Destroy(gameObject);
    //    }
    //}
}
