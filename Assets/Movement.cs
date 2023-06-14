using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : PlayerBaseState
{
    protected float movementMagnitude;
    protected float turnSpeed =0.05f;
    private bool hiphop;
    private bool talk;
    private bool silly;

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
        playerControl.charController.enabled = true;
        hiphop = false;
        talk = false;
        silly = false;
    }


    public override void HandleInput()
    {
        base.HandleInput();

        Debug.Log("g");
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

        hiphop = Input.GetKeyDown(KeyCode.H);
        talk = Input.GetKeyDown(KeyCode.T);
        silly = Input.GetKeyDown(KeyCode.X);

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
        if (PlayerControl.playerInstance.IsSitting)
        {
            
            playerStateManager.ChangeState(playerControl.sit);
        }
        
        if (hiphop)
        {
            playerStateManager.ChangeState(playerControl.hipHopState);
        }
        if (talk)
        {
            playerStateManager.ChangeState(playerControl.talkingState);
        }
        if (silly)
        {
            playerStateManager.ChangeState(playerControl.sillyDanceState);
        }
        
    }
    public override void Exit()
    {
        base.Exit();
        playerControl.ResetMoveParams();
    }
    
    
    
}
