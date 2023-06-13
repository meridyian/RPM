using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SillyDanceState : PlayerBaseState
{
    private int SillyDanceParam = Animator.StringToHash("Silly");
    
    public SillyDanceState(PlayerControl playerControl, PlayerStateManager playerStateManager) : base(playerControl, playerStateManager)
    {
    }
    public override void Enter()
    {
        base.Enter();
        playerControl.TriggerAnimation(SillyDanceParam);
        playerStateManager.ChangeState(playerControl.movement);
    }
    

}
