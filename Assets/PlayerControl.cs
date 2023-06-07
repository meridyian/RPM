using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Fusion;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerControl : NetworkBehaviour
{
    [SerializeField] private float rotationSpeed = 0.01f;
    public float PlayerSpeed = 5f;

    public float GravityValue = -9.81f;
    [Networked] public bool FillChair{ get; set; }

    private Vector3 velocity;
    private bool _jumpPressed;
    
    //controllers
    public static PlayerControl Local { get; set; }
    public Animator characterAnimator;

    public CinemachineVirtualCamera localCamera;
    public CharacterController charController;
    public Canvas sittingCanvas;
    
    //movement

    private Transform chairTransform;
    [Networked] public bool IsSitting{ get; set; }
    [Networked] public bool IsStanding{ get; set; }
    
    //check
    public string X;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        

    }

    public override void Spawned()
    {

        if (Object.HasInputAuthority)
        {
            Local = this;
            localCamera.transform.parent = null;
        }
        else
        {
            //CinemachineVirtualCamera localCamera = GetComponent<CinemachineVirtualCamera>();
            localCamera.enabled = false;
        }
    }

    public void Update()
    {
        if (Object != null)
        {
            X = Object.StateAuthority.ToString();
        }
        
        //input authority only belongs to host, check if yo are the host
        
        if (!Object.HasInputAuthority)
            return;
            
        
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("left click is pressed");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Chair"))
                {
                    Debug.Log("hit the chair");
                    chairTransform = hit.collider.transform;
                    sittingCanvas.gameObject.SetActive(true);
                    // is sitting yerine sit gibi oturucak olmasına bakan bi bool yap canvasta yese basınca true olsun
                    if (sittingCanvas.gameObject.GetComponent<SittingCanvas>().yesPressed)
                    {
                        FillChair = true; 
                        if(hit.transform.TryGetComponent<Chair>(out var chair )) 
                            chair.DealSittingRpc(FillChair);
                    }

                }
            }
        }
        
        
        // oturuyor halinde olması lazım, is sitting güzel bunun için
        if (IsSitting)
        {
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                IsSitting = false;
                IsStanding = true;
                FillChair = false;
                chairTransform.GetComponent<Chair>().DealSittingRpc(FillChair);
                //sittingCanvas.gameObject.GetComponent<SittingCanvas>().yesPressed = false;
                StartCoroutine(SitToStandAnimation());
                if (IsStanding)
                {
                    sittingCanvas.gameObject.GetComponent<SittingCanvas>().yesPressed = false;
                    charController.enabled = true;
                }
                
            }
            
        }
  
        
    }
    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SendMessage()
    {
        Debug.Log("sitting animation will play");
    }
    
    


    public override void FixedUpdateNetwork()
    {
        
        if (HasStateAuthority == false)
        {
            return;
        }

        if (!IsSitting)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))*Runner.DeltaTime* PlayerSpeed;
            move.Normalize();
            float movementMagnitude = Mathf.Clamp01(move.magnitude);
            move = Quaternion.AngleAxis(Camera.main.transform.rotation.eulerAngles.y, Vector3.up) * move;
            characterAnimator.SetFloat("walkSpeed", movementMagnitude);
            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move),
                    rotationSpeed);
            }
            charController.Move(move*5* Runner.DeltaTime);
        }
        
        if (sittingCanvas.gameObject.GetComponent<SittingCanvas>().yesPressed )
        {
            IsStanding = false;
            charController.enabled = false;
            transform.position = Vector3.Lerp(transform.position,
                chairTransform.GetChild(0).transform.position, 5f);
            characterAnimator.SetBool("Sit",true);
            IsSitting = true;
            
            //hit.collider.GetComponentInParent<Chair>().IsChairFull = true;
            //transform.position = Vector3.Lerp(transform.position,

        }
        

    }
    
/*
    //dance animations
    
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
    */
    public IEnumerator SitToStandAnimation()
    {
        characterAnimator.SetBool("Sit", false);
        characterAnimator.SetBool("Stand", true);
        transform.position = Vector3.Lerp(transform.position,
            chairTransform.GetChild(0).transform.position, 5f);
        yield return new WaitForSeconds(1.5f);
        IsStanding = true;

    }
}
    
