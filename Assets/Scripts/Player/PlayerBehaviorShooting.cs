using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviorShooting : IPlayerBehavior
{
    public void Enter(Player player)
    {
    }

    public void Exit(Player player)
    {
        player.HideTargetPointer();
    }

    public void FixedUpdate(Player player)
    {
    }

    public void Update(Player player)
    {
        player.ShootOnIdle();
    }
}
