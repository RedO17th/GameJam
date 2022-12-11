using UnityEngine;

public abstract class BaseAbility : MonoBehaviour
{
    [SerializeField] protected AbilityType _abilityType = AbilityType.None;

    public AbilityType AbilityType => _abilityType;

    protected AbilitySystem _system = null;

    public virtual void Initialize(AbilitySystem system)
    {
        _system = system;
    }

    public abstract void Use();
}
 