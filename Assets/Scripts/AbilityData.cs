using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityData
{
    private AbilityType _abilityType;

    private float cooldown;

    public AbilityData(AbilityType abilityType, float cooldown)
    {
        this._abilityType = abilityType;
        this.cooldown = cooldown;
    }

    public AbilityType GetAbilityType()
    {
        return _abilityType;
    }

    public float GetCooldown()
    {
        return cooldown;
    }
}

//public enum AbilityType
//{
//    Range,
//    Melee,
//    Grenade
//}
