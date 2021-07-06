using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : GeneralProjectile
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        ReadyToReturnToThePool();
    }
}