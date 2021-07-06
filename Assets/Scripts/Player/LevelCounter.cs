using UnityEngine;
using UnityEngine.UI;

public class LevelCounter : MonoBehaviour
{
    public delegate void LevelIncreasedAction();
    public event LevelIncreasedAction OnLevelIncreased;

    [SerializeField] private EnemyCollector _enemyCollector;
    [SerializeField] private LevelProgressBar _levelProgressBar;

    private Player _player;

    private int _currentLevel;
    private int _currentXP;

    private static readonly int EXPERIENCE_BEFORE_NEXT_LEVEL = 150;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _enemyCollector.OnEnemyWasKilled += TryToIncreaseLevel;
    }

    private void Start()
    {
        _currentLevel = _player.Level;
        _currentXP = 0;

        _levelProgressBar.UpdateLevelProgressBar(CalculateXPFillPercent(), _currentLevel);
    }

    private void OnDestroy()
    {
        _enemyCollector.OnEnemyWasKilled -= TryToIncreaseLevel;
    }

    private float CalculateXPFillPercent()
    {
        return (float)_currentXP / (float)EXPERIENCE_BEFORE_NEXT_LEVEL;
    }

    private void TryToIncreaseLevel(int xpReward)
    {
        _currentXP += xpReward;

        if (_currentXP >= EXPERIENCE_BEFORE_NEXT_LEVEL)
        {
            int experienceLeft = _currentXP - EXPERIENCE_BEFORE_NEXT_LEVEL;
            IncreaseLevel(experienceLeft);
        }
        else
            _levelProgressBar.UpdateLevelProgressBar(CalculateXPFillPercent(), _currentLevel);
    }

    private void IncreaseLevel(int xpLeft)
    {
        _currentXP = xpLeft;
        _currentLevel += 1;
        _levelProgressBar.UpdateLevelProgressBar(CalculateXPFillPercent(), _currentLevel);
        _player.Level = _currentLevel;

        OnLevelIncreased?.Invoke();
    }
}
