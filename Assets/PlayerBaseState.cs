using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    protected PlayerControl playerControl;
    protected PlayerStateManager playerStateManager;

    protected PlayerBaseState(PlayerControl playerControl, PlayerStateManager playerStateManager)
    {
        this.playerControl = playerControl;
        this.playerStateManager = playerStateManager;
    }

    public virtual void Enter()
    {
        
    }

    public virtual void HandleInput()
    {
        
    }
    public virtual void LogicUpdate()
    {
        
    }
    public virtual void PhysicsUpdate()
    {
        
    }
    public virtual void Exit()
    {
        
    }

}
