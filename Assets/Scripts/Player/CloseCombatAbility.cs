using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCombatAbility : BaseAbility
{
    [SerializeField] private GameObject _CombatAtackZone;
    public override void Use()
    {
        _CombatAtackZone.SetActive(true);
    }
}
