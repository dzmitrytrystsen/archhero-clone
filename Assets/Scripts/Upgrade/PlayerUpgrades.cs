using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : Singleton<PlayerUpgrades>
{
    public Upgrade RandomUpgrade { get { return GetRandomUpgrade(); } }

    public List<Upgrade> AllUpgrades { get { return _allUpgrades; } }

    [SerializeField] private List<Upgrade> _allUpgrades;

    private Upgrade GetRandomUpgrade()
    {
        Upgrade randomUpgrade = _allUpgrades[Random.Range(0, _allUpgrades.Count)];
        return randomUpgrade;
    }
}