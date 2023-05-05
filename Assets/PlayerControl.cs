using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Fusion;
using UnityEngine;


public class PlayerControl : NetworkBehaviour
{
    [SerializeField] private float rotationSpeed = 0.01f;
    public float raycastDistance = 2f;
    public LayerMask interactableLayers;
    public static PlayerControl Local { get; set; }
    public Animator characterAnimator;
    private NetworkCharacterControllerPrototype _cc;
    public CinemachineVirtualCamera localCamera;
    private Vector3 _forward;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterControllerPrototype>();
        _forward = transform.forward;
    }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;

        }
        else
        {
            //CinemachineVirtualCamera localCamera = GetComponent<CinemachineVirtualCamera>();
            localCamera.enabled = false;
            
        }
    }

 

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            float inputMagnitude = Mathf.Clamp01(data.direction.magnitude);
            data.direction = Quaternion.AngleAxis(Camera.main.transform.rotation.eulerAngles.y, Vector3.up) *
                             data.direction;
            data.direction.Normalize();
            
            // actual move part 
            _cc.Move(5*data.direction*Runner.DeltaTime);
            
            if (data.direction.sqrMagnitude > 0)
                _forward = data.direction;
            
            
            if (data.direction != Vector3.zero)
            {
            
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(data.direction),
                    rotationSpeed);
            }
            
            // to control animation blend tree 
            /*
            Vector3 movementDir = new Vector3(_cc.Velocity.x, 0, _cc.Velocity.z);
            movementDir.Normalize();
            */
            characterAnimator.SetFloat("walkSpeed", inputMagnitude);

            
            if (data.HiphopAnim)
            {
                StartCoroutine(HipHopAnimation());
            }
            if (data.SillyDanceAnim)
            {
                StartCoroutine(SillyDanceAnimation());
            }
            if (data.TalkingAnim)
            {
                StartCoroutine(TalkingAnimation());
            }

        }
        
        
    }

    public IEnumerator HipHopAnimation()
    {
        characterAnimator.SetBool("HipHop", true);
        yield return new WaitForSeconds(0.5f);
        characterAnimator.SetBool("HipHop", false);
    }
    public IEnumerator SillyDanceAnimation()
    {
        characterAnimator.SetBool("SillyDance", true);
        yield return new WaitForSeconds(1f);
        characterAnimator.SetBool("SillyDance", false);
    }
    public IEnumerator TalkingAnimation()
    {
        characterAnimator.SetBool("talking", true);
        yield return new WaitForSeconds(3f);
        characterAnimator.SetBool("talking", false);
    }
    
    
    
}
