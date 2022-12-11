using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    [SerializeField] private Transform _armaLaunchPosition;
    [SerializeField] private BaseArmaProjectile _armaProjectilePrefab;

    private BasePlayer _player = null;

    private List<BaseArmament> _armaments = new List<BaseArmament>();
    private BaseArmament _currentArmament = null;

    public void Initialize(BasePlayer player)
    {
        _player = player;
        _player.OnUseArmament += UseArmament;

        _armaments.Add(new MidasHand());
        _currentArmament = _armaments[0];
    }

    public void Deinitialize()
    {
        _armaments.Clear();
        _currentArmament = null;

        _player.OnUseArmament -= UseArmament;
        _player = null;
    }

    private void UseArmament()
    {
        _currentArmament?.Use();

        var prjectile = Instantiate(_armaProjectilePrefab, _armaLaunchPosition.position, _armaLaunchPosition.rotation);
            prjectile.Launch();
    }



}
