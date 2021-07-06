using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    [SerializeField] public Joystick joystick;

    private Dictionary<Type, IGameBehavior> _behaviorsMap;
    private IGameBehavior _currentBehavior;

    private void Awake()
    {
        InitBehaviors();
        SetBehaviorByDefault();
    }

    public void SetGameOnPause()
    {
        SetBehaviorPause();
    }

    public void SetGameLive()
    {
        SetBehaviorLive();
    }

    private void InitBehaviors()
    {
        _behaviorsMap = new Dictionary<Type, IGameBehavior>();

        _behaviorsMap[typeof(GameBehaviorLive)] = new GameBehaviorLive();
        _behaviorsMap[typeof(GameBehaviorPause)] = new GameBehaviorPause();
    }

    private void SetBehavior(IGameBehavior newBehavior)
    {
        if (_currentBehavior != null)
            _currentBehavior.Exit(this);

        _currentBehavior = newBehavior;
        _currentBehavior.Enter(this);
    }

    private void SetBehaviorByDefault()
    {
        SetBehaviorLive();
    }

    private IGameBehavior GetBehavior<T>() where T : IGameBehavior
    {
        var type = typeof(T);
        return _behaviorsMap[type];
    }

    private void SetBehaviorLive()
    {
        var behavior = GetBehavior<GameBehaviorLive>();
        SetBehavior(behavior);
    }

    private void SetBehaviorPause()
    {
        var behavior = GetBehavior<GameBehaviorPause>();
        SetBehavior(behavior);
    }
}
