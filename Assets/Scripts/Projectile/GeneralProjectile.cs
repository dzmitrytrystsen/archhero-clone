using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GeneralProjectile : MonoBehaviour
{
    public int SetDamage { set { _damage = value; } }

    public delegate void ReadyToReturnToThePoolAction(GameObject projectile);
    public event ReadyToReturnToThePoolAction OnReadyToReturnToThePool;

    protected int _damage;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.Attack(_damage);
            ReadyToReturnToThePool();
        }
    }

    protected void ReadyToReturnToThePool()
    {
        OnReadyToReturnToThePool?.Invoke(gameObject);
    }
}
