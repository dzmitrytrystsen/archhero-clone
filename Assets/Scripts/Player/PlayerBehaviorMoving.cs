using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviorMoving : IPlayerBehavior
{
    public void Enter(Player player)
    {
        player.Animator.SetBool("isMoving", true);
    }

    public void Exit(Player player)
    {
        player.Animator.SetBool("isMoving", false);
    }

    public void FixedUpdate(Player player)
    {
    }

    public void Update(Player player)
    {
        player.Move();
    }
}