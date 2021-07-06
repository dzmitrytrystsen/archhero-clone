using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviorPause : IGameBehavior
{
    public void Enter(GameStateMachine gameStateMachine)
    {
        gameStateMachine.joystick.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    public void Exit(GameStateMachine gameStateMachine)
    {
        
    }
}
