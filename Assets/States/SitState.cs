using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitState : AnimationState
{
    public SitState(PlayerControl playerControl, PlayerStateManager playerStateManager) : base(playerControl, playerStateManager)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // send rpc to say chairs are full
        PlayerControl.Local.chairTransform.GetComponent<Chair>().DealSittingRpc(PlayerControl.Local.IsSitting);
        playerControl.charController.enabled = false;
        playerControl.transform.position = Vector3.Lerp(playerControl.transform.position,
            PlayerControl.Local.chairTransform.GetChild(0).transform.position, 5f);
        playerControl.SetAnimationBool(playerControl.sitParam,true);
        
    }

    public override void Exit()
    {
        
        base.Exit();
        playerControl.IsSitting = false;
        var FullChair = false;
        playerControl.chairTransform.GetComponent<Chair>().DealSittingRpc(FullChair);
        playerControl.SetAnimationBool(playerControl.sitParam,false);

        
    }
}
