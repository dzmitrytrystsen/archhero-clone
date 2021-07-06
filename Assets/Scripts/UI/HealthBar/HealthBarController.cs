using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] protected Image _healthBar;

    protected Unit _unit;
    protected int _unitCurrentHealth;
    protected int _unitMaxHealth;

    protected virtual void Start()
    {
        _unit = transform.GetComponentInParent<Unit>();

        _unitMaxHealth = _unit.Health;
        _unitCurrentHealth = _unitMaxHealth;

        _unit.OnHealthChanged += UpdateHealthBar;
    }

    protected virtual void OnDestroy()
    {
        _unit.OnHealthChanged -= UpdateHealthBar;
    }

    protected virtual void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }

    protected virtual void UpdateHealthBar(int unitHealth, GameObject unit)
    {
        _unitCurrentHealth = unitHealth;
        _healthBar.fillAmount = (float)_unitCurrentHealth / _unitMaxHealth;
    }
}
