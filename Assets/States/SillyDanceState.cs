using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SillyDanceState : PlayerBaseState
{
    private int SillyDanceParam = Animator.StringToHash("Silly");
    
    public SillyDanceState(PlayerControl playerControl, PlayerStateManager playerStateManager) : base(playerControl, playerStateManager)
    {
    }
    public override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetKey(KeyCode.K))
        {
            playerControl.TriggerAnimation(SillyDanceParam);
            playerStateManager.ChangeState(playerControl.hipHopState);
        }
            
            
    }
}
