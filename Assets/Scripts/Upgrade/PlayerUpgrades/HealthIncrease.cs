using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Upgrade", menuName = "ScriptableObjects/Upgrades/ActiveUpgrades/HealthIncrease")]
public class HealthIncrease : ActiveUpgrade
{
    public int HealthAmountToIncrease = 50;

    public override void Activate(object data)
    {
        Player player = (Player)data;
        player.IncreaseMaxHealth(HealthAmountToIncrease);
    }
}