using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingState : AnimationState
{

    private int TalkParam = Animator.StringToHash("Talk");
    public TalkingState(PlayerControl playerControl, PlayerStateManager playerStateManager) : base(playerControl, playerStateManager)
    {
    }
    public override void Enter()
    {
        base.Enter();
        playerControl.TriggerAnimation(TalkParam);

    }
    
    
}
