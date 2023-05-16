using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Fusion;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerControl : NetworkBehaviour
{
    [SerializeField] private float rotationSpeed = 0.01f;
    
    //controllers
    public static PlayerControl Local { get; set; }
    public Animator characterAnimator;
    private NetworkCharacterControllerPrototype _cc;
    public CinemachineVirtualCamera localCamera;
    public CharacterController charController;
    public Canvas sittingCanvas;
    
    //movement
    private Vector3 _forward;
    private Transform chairTransform;
    [Networked] public bool IsSitting{ get; set; }
    [Networked] public bool IsStanding{ get; set; }
    
    //check
    public string X;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        _cc = GetComponent<NetworkCharacterControllerPrototype>();
        _forward = transform.forward;
        
        
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
                        //Rpc_IWantToSitBilader();
                        //transform.position = Vector3.Lerp(transform.position,
                           // chairTransform.GetChild(0).transform.position, 5f);
                        hit.collider.GetComponentInParent<Chair>().IsChairFull = true;
                    }
                    /*
                    var chair = hit.transform.gameObject.GetComponent<Chair>();
                    chair.IsChairFull = !chair.IsChairFull;
                    */
                }
                
            }
        }
        
        // oturuyor halinde olması lazım, is sitting güzel bunun için
        if (IsSitting)
        {
            
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                //sittingCanvas.gameObject.GetComponent<SittingCanvas>().yesPressed = false;
                StartCoroutine(SitToStandAnimation());
                if (IsStanding)
                {
                    sittingCanvas.gameObject.GetComponent<SittingCanvas>().yesPressed = false;
                    IsSitting = false;
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
    
    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void Rpc_IWantToSitBilader()
    {
        Debug.Log("lel");
    }


    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            // oturuyor halde olması yine issitting okay otururken input alma
            if (!IsSitting)
            {
                float inputMagnitude = Mathf.Clamp01(data.direction.magnitude);
                data.direction = Quaternion.AngleAxis(Camera.main.transform.rotation.eulerAngles.y, Vector3.up) *
                                 data.direction;
                data.direction.Normalize();

                // actual move part 
                _cc.Move(5 * data.direction * Runner.DeltaTime);

                if (data.direction.sqrMagnitude > 0)
                    _forward = data.direction;


                if (data.direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(data.direction),
                        rotationSpeed);
               
                }
            
                characterAnimator.SetFloat("walkSpeed", inputMagnitude);
            
                if (data.HiphopAnim) {
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
                /*
                if (data.stand)
                {
                    IsSitting = false;
                    characterAnimator.SetBool("Stand", true);
                }
                */
            }

            if (sittingCanvas.gameObject.GetComponent<SittingCanvas>().yesPressed)
            {
                Rpc_IWantToSitBilader();
                charController.enabled = false;
                transform.position = Vector3.Lerp(transform.position,
                    chairTransform.GetChild(0).transform.position, 5f);
                characterAnimator.SetBool("Sit", true);
                IsSitting = true;
            }
        
        }
    }
    

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
    
    public IEnumerator SitToStandAnimation()
    {
        characterAnimator.SetBool("Sit", false);
        characterAnimator.SetBool("Stand", true);
        transform.position = Vector3.Lerp(transform.position,
            chairTransform.GetChild(0).transform.position, 5f);
        yield return new WaitForSeconds(1.5f);
        IsStanding = true;
        characterAnimator.SetBool("Stand", false);
    }

    
    
    
}