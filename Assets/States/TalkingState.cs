using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingState : PlayerBaseState
{

    private int TalkParam = Animator.StringToHash("Talk");
    public TalkingState(PlayerControl playerControl, PlayerStateManager playerStateManager) : base(playerControl, playerStateManager)
    {
    }
    public override void Enter()
    {
        base.Enter();

        playerControl.TriggerAnimation(TalkParam);
        playerStateManager.ChangeState(playerControl.movement);
    }
    
}
