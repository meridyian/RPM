using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitState : AnimationState
{
    private int movementParam = Animator.StringToHash("movementParam");
    public SitState(PlayerControl playerControl, PlayerStateManager playerStateManager) : base(playerControl, playerStateManager)
    {
    }

    public override void Enter()
    {
        base.Enter();
        PlayerControl.playerInstance.chairTransform.GetComponent<Chair>().DealSittingRpc(PlayerControl.playerInstance.IsSitting);
        playerControl.charController.enabled = false;
        playerControl.transform.position = Vector3.Lerp(playerControl.transform.position,
            PlayerControl.playerInstance.chairTransform.GetChild(0).transform.position, 5f);
        playerControl.SetAnimationBool(playerControl.sitParam,true);
        
    }

    public override void Exit()
    {
        base.Exit();
        playerControl.IsSitting = false;
        var FullChair = false;
        playerControl.chairTransform.GetComponent<Chair>().DealSittingRpc(FullChair);
        Debug.Log("emptychair"+FullChair);
        Debug.Log("ischairfull"+playerControl.chairTransform.GetComponent<Chair>().IsChairFull);
        playerControl.SetAnimationBool(playerControl.sitParam,false);
        playerControl.TriggerAnimation(movementParam);
        
    }
}
