using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerBaseState
{


    public JumpState(PlayerControl playerControl, PlayerStateManager playerStateManager) : base(playerControl, playerStateManager)
    {
    }

    public override void Enter()
    
    {   
        base.Enter();
        playerControl.TriggerAnimation(playerControl.jump);
    }



    public override void LogicUpdate()  
    {
        base.LogicUpdate();
        var verticalInput = Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");

        if ((verticalInput != 0 || horizontalInput != 0))
        {
            playerStateManager.ChangeState(playerControl.movement);

        }
    }

   public override void Exit()
   {
       base.Exit();
       
   }
   
}
