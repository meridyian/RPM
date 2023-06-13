using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingState : PlayerBaseState
{

    private int TalkParam = Animator.StringToHash("Talk");
    public TalkingState(PlayerControl playerControl, PlayerStateManager playerStateManager) : base(playerControl, playerStateManager)
    {
    }
    public override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetKey(KeyCode.T))
        {
            playerControl.TriggerAnimation(TalkParam);
            playerStateManager.ChangeState(playerControl.hipHopState);
        }
            
            
    }
}
