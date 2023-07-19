using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceTalkState : AnimationState
{
    
    public DanceTalkState(PlayerControl playerControl, PlayerStateManager playerStateManager) : base(playerControl, playerStateManager)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        playerControl._networkedMechanimAnim.Animator.SetInteger("DanceorTalk", playerControl.danceortalkparam);
    }
    
}
