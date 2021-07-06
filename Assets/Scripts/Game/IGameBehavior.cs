using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameBehavior
{
    void Enter(GameStateMachine gameStateMachine);
    void Exit(GameStateMachine gameStateMachine);
}
