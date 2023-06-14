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
        playerControl.IsSitting = true;
        Debug.Log(playerControl.IsSitting);
        playerControl.charController.enabled = false;
        playerControl.transform.position = Vector3.Lerp(playerControl.transform.position,
            playerControl.chairTransform.GetChild(0).transform.position, 5f);
        playerControl.SetAnimationBool(playerControl.sitParam,true);
        
    }

    public override void Exit()
    {
        base.Exit();
        playerControl.FillChair = false;
        playerControl.IsSitting = false;
        playerControl.chairTransform.GetComponent<Chair>().DealSittingRpc(playerControl.FillChair);
        playerControl.SetAnimationBool(playerControl.sitParam,false);
        
    }
}
