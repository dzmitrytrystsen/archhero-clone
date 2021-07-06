using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IAttackable
{
    public int Health { get { return _health; } }

    public delegate void HealthChangedAction(int health, GameObject unit);
    public event HealthChangedAction OnHealthChanged;

    [SerializeField] protected int _health;
    [SerializeField] protected GameObject projectile;

    protected bool _isGrounded;

    public void Attack(int damage)
    {
        _health -= damage;
        OnHealthChanged?.Invoke(_health, gameObject);
    }

    protected virtual void FixedUpdate()
    {

    }
}