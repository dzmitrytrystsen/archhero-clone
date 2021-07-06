using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : HealthBarController
{
    protected Player _player;

    protected override void Start()
    {
        base.Start();

        _player = transform.GetComponentInParent<Player>();
        _player.OnHealthIncreased += RefillHealthBarWithMaxHealth;
        _player.OnHealed += UpdateHealth;
    }

    private void RefillHealthBarWithMaxHealth(int newMaxHealthValue)
    {
        _unitMaxHealth = newMaxHealthValue;
        _healthBar.fillAmount = (float)_unitCurrentHealth / _unitMaxHealth;
    }

    private void UpdateHealth(int playerHealth)
    {
        _unitCurrentHealth = playerHealth;
        _healthBar.fillAmount = (float)_unitCurrentHealth / _unitMaxHealth;
    }
}
