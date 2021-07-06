using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollector : MonoBehaviour
{
    public delegate void EnemyWasKilledAction(int experienceReward);
    public event EnemyWasKilledAction OnEnemyWasKilled;

    [SerializeField] private Portal _portal;

    private Enemy[] _enemies;
    private int _enemiesAmount;

    private void Awake()
    {
        CollectAllEnemies();
        SubscribeToAllEnemies();
    }

    private void OnDestroy()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.OnHealthChanged -= TryToCollectEnemy;
        }
    }

    public void CollectAllEnemiesAndSubscribe()
    {
        CollectAllEnemies();
        SubscribeToAllEnemies();
    }

    private void CollectAllEnemies()
    {
        _enemies = FindObjectsOfType<Enemy>();
        _enemiesAmount = _enemies.Length;
    }

    private void SubscribeToAllEnemies()
    {
        foreach(Enemy enemy in _enemies)
        {
            enemy.OnHealthChanged += TryToCollectEnemy;
        }
    }

    private void TryToCollectEnemy(int enemyHealth, GameObject enemy)
    {
        if (enemyHealth <= 0)
        {
            _enemiesAmount -= 1;
            OnEnemyWasKilled?.Invoke(enemy.GetComponent<Enemy>().ExperienceReward);
            Destroy(enemy);
            TryToCompleteLevel();
        }
    }

    private void TryToCompleteLevel()
    {
        if (_enemiesAmount <= 0)
        {
            _portal.OpenPortal();
        }
    }

}
