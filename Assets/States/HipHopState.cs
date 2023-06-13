using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipHopState : PlayerBaseState
{
    private int HipHopParam = Animator.StringToHash("HipHop");
    
    public HipHopState(PlayerControl playerControl, PlayerStateManager playerStateManager) : base(playerControl, playerStateManager)
    {
    }
    
    public override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetKey(KeyCode.H))
        {
            playerControl.TriggerAnimation(HipHopParam);
            playerStateManager.ChangeState(playerControl.hipHopState);
        }
            
            
    }
}
