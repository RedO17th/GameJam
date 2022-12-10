using UnityEngine.Events;
public static class EventManager
{
    public static readonly UnityEvent<Enemy> OnEnemyKilled = new UnityEvent<Enemy>();

    public static void SendEnemyKilled(Enemy enemy)
    {
        if (OnEnemyKilled != null) OnEnemyKilled.Invoke(enemy);
    }

}

