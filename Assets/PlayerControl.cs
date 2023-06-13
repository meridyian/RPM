using System.Collections;
using System.Linq.Expressions;
using Cinemachine;
using Fusion;
using UnityEngine;


public class PlayerControl : NetworkBehaviour
{
    [SerializeField] private float rotationSpeed = 0.01f;
    public float PlayerSpeed = 5f;

    public float GravityValue = -9.81f;
    [Networked] public bool FillChair{ get; set; }

    private Vector3 velocity;
    private bool _jumpPressed;
    
    // state machine
    public PlayerStateManager movementSM;
    public HipHopState hipHopState;
    public TalkingState talkingState;
    public SillyDanceState sillyDanceState;
    public Movement movement;
    
    
    
    
    
    //controllers
    public static PlayerControl Local { get; set; }
    public Animator characterAnimator;

    public CinemachineFreeLook localCamera;
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
        movementSM = new PlayerStateManager();

        hipHopState = new HipHopState(this,movementSM);
        talkingState = new TalkingState(this, movementSM);
        sillyDanceState = new SillyDanceState(this, movementSM);
        movement = new Movement(this, movementSM);
        
        movementSM.Initialize(movement);
        
        IsStanding = true;
        if (Object.HasStateAuthority)
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
                    if (!hit.transform.GetComponent<Chair>().IsChairFull)
                    {
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
                    Debug.Log("chair is full");
                    

                }
            }
        }
        
        
        // oturuyor halinde olması lazım, is sitting güzel bunun için
        if (IsSitting)
        {
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                
                IsStanding = true;
                FillChair = false;
                chairTransform.GetComponent<Chair>().DealSittingRpc(FillChair);
                //sittingCanvas.gameObject.GetComponent<SittingCanvas>().yesPressed = false;
                StartCoroutine(SitToStandAnimation());
                if (IsStanding)
                {
                    sittingCanvas.gameObject.GetComponent<SittingCanvas>().yesPressed = false;
                    
                }
                
            }
            
        }
  
        
    }
    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SendMessage()
    {
        Debug.Log("sitting animation will play");
    }
    
    public void TriggerAnimation(int param)
    {
        characterAnimator.SetTrigger(param);
    }

    public void Move(Vector3 move)
    {
        charController.Move(move* Runner.DeltaTime);
    }


    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority == false)
        {
            return;
        }
        movementSM.CurrentState.HandleInput();
        movementSM.CurrentState.PhysicsUpdate();
        movementSM.CurrentState.Exit();
        
        
    }
    /*
    public override void FixedUpdateNetwork()
    {
        
        

        
        
        if (!IsSitting || IsStanding)
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
            
        }
        

    }
    */
    
    
    
    

    //dance animations
    

    
    public IEnumerator SitToStandAnimation()
    {
        IsSitting = false;
        characterAnimator.SetBool("Sit", false);
        characterAnimator.SetBool("Stand", true);
        yield return new WaitForSeconds(3f);
        IsStanding = true;
        yield return new WaitForSeconds(.75f);
        charController.enabled = true;
        //chair.DealSittingRpc(FillChair);

    }
}
    
