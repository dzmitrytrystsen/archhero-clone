using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviorIdle : IPlayerBehavior
{
    public void Enter(Player player)
    {
        player.Animator.SetBool("isIdle", true);
    }

    public void Exit(Player player)
    {
        player.Animator.SetBool("isIdle", false);
    }

    public void FixedUpdate(Player player)
    {
    }

    public void Update(Player player)
    {
        
    }
}
