using System.Collections;
using System.Linq.Expressions;
using Cinemachine;
using ExitGames.Client.Photon.StructWrapping;
using Fusion;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerControl : NetworkBehaviour
{
    // statemanager and states
    public PlayerStateManager movementSM;
    public bool canMove;
    
    
    // USERNAME
    
    public string userName { get; set; }

    public SitState sit;
    public Movement movement;
    public DanceTalkState dancetalkState;
    public int danceortalkparam;
    public JumpState jumpState;

    // will be called from movement
    public int sitParam => Animator.StringToHash("Sit");
    public int dancetalkParam = Animator.StringToHash("DanceTalk");
    public int movementParam = Animator.StringToHash("movementParam");
    public int jumpParam = Animator.StringToHash("jump");

    //controllers
    public static PlayerControl Local { get; set; }
    public Animator characterAnimator;

    public CinemachineFreeLook localCamera;
    public CharacterController charController;
    public Canvas sittingCanvas;

    //  sitting position and sitting state check of player
    public Transform chairTransform;
    [Networked] public bool IsSitting { get; set; }
    [Networked] public bool IsTyping { get; set; }

    public NetworkMecanimAnimator _networkedMechanimAnim;

    //check authorithy
    public string X;


    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    public override void Spawned()
    {
        // instantiate states and state machnine manager

        //usernamePanel.gameObject.SetActive(true);
        //characterAnimator.enabled = false;
        movementSM = new PlayerStateManager();
        movement = new Movement(this, movementSM);
        sit = new SitState(this, movementSM);
        dancetalkState = new DanceTalkState(this, movementSM);
        jumpState = new JumpState(this, movementSM);

        // add if username is set 


        movementSM.Initialize(movement);



      
        if (Object.HasStateAuthority)
        {
            Local = this;
            localCamera.transform.parent = null;

        }
        else
        {

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
        
        // raycast for sitting 
        if (Input.GetMouseButtonDown(0))
        {
            
            Debug.Log("left click is pressed");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Chair"))
                {
                    if (!hit.transform.GetComponent<Chair>().IsChairFull)
                    {
                        chairTransform = hit.collider.transform;
                        sittingCanvas.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public override void FixedUpdateNetwork()
    {

        if (HasStateAuthority == false)
        {
            return;
        }

        movementSM.CurrentState.HandleInput();
        movementSM.CurrentState.PhysicsUpdate();
        movementSM.CurrentState.LogicUpdate();

    }

    
    public void ResetMoveParams()
    {
        characterAnimator.SetFloat("walkSpeed", 0f);
    }

    public void SetAnimationBool(int param, bool value)
    {
        characterAnimator.SetBool(param, value);
    }
    
    public void TriggerAnimation(int param)
    {
        characterAnimator.SetTrigger(param);
    }
    public void ChangeInteger(string state, int param)
    {
        characterAnimator.SetInteger(state, param);
    }
    

    // move function for character controller, will be running on movement
    public void Move(Vector3 move)
    {
        charController.Move(move * 5 * Runner.DeltaTime);
    }
    
}
    
