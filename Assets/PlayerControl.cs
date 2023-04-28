using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;


public class PlayerControl : NetworkBehaviour
{
    public Animator characterAnimator;
    private NetworkCharacterControllerPrototype _cc;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterControllerPrototype>();
    }
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.direction.Normalize();
            _cc.Move(5*data.direction*Runner.DeltaTime);
            Vector3 movementDir = new Vector3(_cc.Velocity.x, 0, _cc.Velocity.z);
            movementDir.Normalize();
            float movementSpeed = movementDir.magnitude;
            characterAnimator.SetFloat("walkSpeed",movementSpeed);
            
        }
    }
}
