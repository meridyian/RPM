using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitState : PlayerBaseState
{
    
    public SitState(PlayerControl playerControl, PlayerStateManager playerStateManager) : base(playerControl, playerStateManager)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerControl.IsSitting = true;
        playerControl.charController.enabled = false;
        playerControl.transform.position = Vector3.Lerp(playerControl.transform.position,
            playerControl.chairTransform.GetChild(0).transform.position, 5f);
        playerControl.characterAnimator.SetBool("Sit",true);
        
    }

    public override void Exit()
    {
        base.Exit();
        playerControl.characterAnimator.SetBool("Sit",false);
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            playerControl.FillChair = false;
            playerControl.IsSitting = false;
            playerControl.chairTransform.GetComponent<Chair>().DealSittingRpc(playerControl.FillChair);
            playerStateManager.ChangeState(playerControl.movement);
        }
    }
}
