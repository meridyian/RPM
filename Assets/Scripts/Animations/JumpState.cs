using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class JumpState : PlayerBaseState
{
    

    public JumpState(PlayerControl playerControl, PlayerStateManager playerStateManager) : base(playerControl, playerStateManager)
    {
    }

    public override void Exit()
    {
        playerStateManager.ChangeState(playerControl.movement);
    }

   
}
