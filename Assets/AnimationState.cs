using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationState : PlayerBaseState
{
    private int movementParam = Animator.StringToHash("movementParam");

    public AnimationState(PlayerControl playerControl, PlayerStateManager playerStateManager) : base(playerControl, playerStateManager)
    {
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        var verticalInput = Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");
        if (verticalInput != 0 || horizontalInput != 0)
        {
            playerStateManager.ChangeState(playerControl.movement);
        }
    }
    
    public override void Exit()
    {
        base.Exit();
        playerControl.SetAnimationBool(playerControl.dancetalkParam, false);
        playerControl.TriggerAnimation(movementParam);
    }

    
}
