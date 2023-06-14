using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : PlayerBaseState
{
    protected float movementMagnitude;
    protected float turnSpeed =0.05f;


    private float horizontalInput;
    private float verticalInput;
    private Vector3 move;

    private int movementParam = Animator.StringToHash("movementParam");
    private int dancetalkParam = Animator.StringToHash("DanceTalk");
    

    public Movement(PlayerControl playerControl, PlayerStateManager playerStateManager) : base(playerControl, playerStateManager)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerControl.TriggerAnimation(movementParam);
        horizontalInput = verticalInput = 0.0f;
        playerControl.charController.enabled = true;

    }


    public override void HandleInput()
    {
        base.HandleInput();
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        
        move = new Vector3(horizontalInput, 0, verticalInput);
        move.Normalize();
        movementMagnitude = Mathf.Clamp01(move.magnitude);
        move = Quaternion.AngleAxis(Camera.main.transform.rotation.eulerAngles.y, Vector3.up) * move;
        if (move != Vector3.zero)
        {
            playerControl.transform.forward = move;
            playerControl.transform.rotation = Quaternion.Slerp(playerControl.transform.rotation, Quaternion.LookRotation(move),
                turnSpeed);
        }



        if (Input.GetKeyDown(KeyCode.H) || Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.X))
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                playerControl.danceortalkparam = 0;
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                playerControl.danceortalkparam = 1;
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                playerControl.danceortalkparam = 2;
            }
            playerControl.TriggerAnimation(dancetalkParam);
            playerStateManager.ChangeState(playerControl.dancetalkState);
        }


    }
    
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        playerControl.characterAnimator.SetFloat("walkSpeed",movementMagnitude );
        playerControl.Move(move);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Debug.Log(playerControl.IsSitting);
        
        if (PlayerControl.Local.IsSitting)
        {
            playerStateManager.ChangeState(playerControl.sit);
        }
        
        
    }
    public override void Exit()
    {
        base.Exit();
        playerControl.ResetMoveParams();
    }
    
    
    
}
