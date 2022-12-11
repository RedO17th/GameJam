using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;

public enum AbilityType { None = -1, MidasHand, CloseCombat }

public class AbilitySystem : MonoBehaviour
{
    [SerializeField] private AbilityType _standartAbility = AbilityType.None;

    [SerializeField] private List<BaseAbility> _abilities;

    private BasePlayer _player = null;

    private BaseAbility _currentAbility = null;

    public void Initialize(BasePlayer player)
    {
        _player = player;
        _player.OnUseArmament += UseArmament;

        InitializeAbility();
        SetStandartAbility();
    }

    private void InitializeAbility()
    {
        foreach (var a in _abilities)
            a.Initialize(this);
    }
    private void SetStandartAbility()
    {
        _currentAbility = GetAbilityByType(_standartAbility);
    }

    private BaseAbility GetAbilityByType(AbilityType type)
    {
        return (BaseAbility)_abilities.Where(a => a.AbilityType == type);
    }

    public void Deinitialize()
    {
        _currentAbility = null;
        _abilities.Clear();

        _player.OnUseArmament -= UseArmament;
        _player = null;
    }

    private void UseArmament() => _currentAbility?.Use();



}
