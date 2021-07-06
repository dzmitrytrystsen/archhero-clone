using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviorLive : IGameBehavior
{
    public void Enter(GameStateMachine gameStateMachine)
    {
        gameStateMachine.joystick.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Exit(GameStateMachine gameStateMachine)
    {
        
    }
}
