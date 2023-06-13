using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SillyDanceState : AnimationState
{
    private int SillyDanceParam = Animator.StringToHash("Silly");
    
    public SillyDanceState(PlayerControl playerControl, PlayerStateManager playerStateManager) : base(playerControl, playerStateManager)
    {
    }
    public override void Enter()
    {
        base.Enter();
        playerControl.TriggerAnimation(SillyDanceParam);

    }
    

}
