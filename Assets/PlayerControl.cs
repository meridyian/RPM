using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Fusion;
using UnityEngine;


public class PlayerControl : NetworkBehaviour
{
    public static PlayerControl Local { get; set; }
    public Animator characterAnimator;
    private NetworkCharacterControllerPrototype _cc;
    public CinemachineVirtualCamera localCamera;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterControllerPrototype>();
    }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;
            Debug.Log("Spawned local player");
        }
        else
        {
            //CinemachineVirtualCamera localCamera = GetComponent<CinemachineVirtualCamera>();
            localCamera.enabled = false;
            
            Debug.Log("spawned remote player");
            
            
        }
        
        
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
