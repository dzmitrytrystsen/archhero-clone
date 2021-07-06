using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GeneralProjectile : MonoBehaviour
{
    public int SetDamage { set { _damage = value; } } // SetSMTH назыввают обычно методы а не проперти. Так что лучше это сделать методом и добавить валидацию данных. Чтобы там не появился урон меньше 0 и прочее

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
