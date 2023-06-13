using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : PlayerBaseState
{
    protected float speed;
    protected float turnSpeed =0.05f;
    
    private float horizontalInput;
    private float verticalInput;
    private Vector3 move;

    public Movement(PlayerControl playerControl, PlayerStateManager playerStateManager) : base(playerControl, playerStateManager)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        horizontalInput = verticalInput = 0.0f;
    }
    
    public override void HandleInput()
    {
        base.HandleInput();
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        
        move = new Vector3(horizontalInput, 0, verticalInput);
        move.Normalize();
        move = Quaternion.AngleAxis(Camera.main.transform.rotation.eulerAngles.y, Vector3.up) * move;
        if (move != Vector3.zero)
        {
            playerControl.transform.forward = move;
            playerControl.transform.rotation = Quaternion.Slerp(playerControl.transform.rotation, Quaternion.LookRotation(move),
                turnSpeed);
        }
    }
    
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        playerControl.characterAnimator.SetFloat("walkSpeed", move.magnitude);
        playerControl.Move(move);
    }

    public override void Exit()
    {
        base.Exit();
        move = Vector3.zero;
    }
}
