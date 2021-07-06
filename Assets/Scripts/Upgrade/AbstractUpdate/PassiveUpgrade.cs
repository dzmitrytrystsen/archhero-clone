using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveUpgrade : Upgrade
{
    public GameObject UpgradePrefabToActivate { get { return _upgradePrefabToActivate; } }

    protected GameObject _upgradePrefabToActivate;
}
