using UnityEngine.Events;
public static class EventManager
{
    public static readonly UnityEvent<Enemy> OnEnemyKilled = new UnityEvent<Enemy>();
    public static readonly UnityEvent OnGameOver = new UnityEvent();
    public static readonly UnityEvent<int> OnGoldChanged = new UnityEvent<int>();
    public static readonly UnityEvent<AbilityType> OnAbilityChanged = new UnityEvent<AbilityType>();

    public static void SendEnemyKilled(Enemy enemy) => OnEnemyKilled?.Invoke(enemy);

    public static void SendGameOver() => OnGameOver?.Invoke();

    public static void SendGoldChanged(int value) => OnGoldChanged?.Invoke(value);
    public static void SendAbilityChanged(AbilityType type) => OnAbilityChanged?.Invoke(type);


}

