using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Upgrade", menuName = "ScriptableObjects/Upgrades/ActiveUpgrades/Heal")]
public class Heal : ActiveUpgrade
{
    public override void Activate(object data)
    {
        Player player = (Player)data;
        player.Heal();
    }
}