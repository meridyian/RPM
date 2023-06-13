using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HipHopState : AnimationState
{
    private int HipHopParam = Animator.StringToHash("HipHop");
    
    public HipHopState(PlayerControl playerControl, PlayerStateManager playerStateManager) : base(playerControl, playerStateManager)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerControl.TriggerAnimation(HipHopParam);
    }

    
}
