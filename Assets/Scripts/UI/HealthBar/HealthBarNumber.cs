using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarNumber : MonoBehaviour
{
    private Player _player;
    private Text _healthNumberText;
    private int _currentPlayerHealth;
    private int _maxPlayerHealth;

    private void Start()
    {
        _healthNumberText = GetComponent<Text>();
        _player = GetComponentInParent<Player>();
        _maxPlayerHealth = _player.MaxHealth;
        _currentPlayerHealth = _player.MaxHealth;
        _player.OnHealthChanged += UpdateCurrentPlayerHealth;
        _player.OnHealthIncreased += UpdatePlayerMaxHealth;
        _player.OnHealed += UpdateHealth;

        _healthNumberText.text = _currentPlayerHealth + " / " + _maxPlayerHealth;
    }

    private void UpdateCurrentPlayerHealth(int playerHealthLeft, GameObject player)
    {
        _currentPlayerHealth = playerHealthLeft;
        _healthNumberText.text = _currentPlayerHealth + " / " + _maxPlayerHealth;
    }

    private void UpdatePlayerMaxHealth(int newMaxHealthValue)
    {
        _maxPlayerHealth = _player.MaxHealth;
        _healthNumberText.text = _currentPlayerHealth + " / " + _maxPlayerHealth;
    }

    private void UpdateHealth(int playerHealth)
    {
        _currentPlayerHealth = playerHealth;
        _healthNumberText.text = _currentPlayerHealth + " / " + _maxPlayerHealth;
    }
}

