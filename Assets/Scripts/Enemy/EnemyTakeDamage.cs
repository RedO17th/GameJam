using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    [SerializeField] Enemy Enemy;

    private void OnTriggerEnter(Collider other)
    {
        //Не использовать на практике Теги
        if (other.CompareTag("Bullet"))
            Enemy.TakeDamage();
    }
}
