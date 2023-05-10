using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Fusion;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerControl : NetworkBehaviour
{
    [SerializeField] private float rotationSpeed = 0.01f;
    public float raycastDistance = 2f;
    public static PlayerControl Local { get; set; }
    [Networked] public NetworkButtons ButtonsPrevious { get; set; }
    public Animator characterAnimator;
    private NetworkCharacterControllerPrototype _cc;
    public CinemachineVirtualCamera localCamera;
    private Vector3 _forward;
    public NetworkBool sit;
    public NetworkBool stand;



    
    public string X;

   

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
                    //sittingCanvas.gameObject.SetActive(true);
                    Rpc_IWantToSitBilader();
                    Debug.Log("hit the chair");
                    //var chair = hit.transform.gameObject.GetComponent<Chair>();
                    //chair.IsChairFull = !chair.IsChairFull;
                }
            }
        }
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
            if (!Chair.chairInstance.isEmpty)
            {
                sit = true;
                if (sit)
                {
                    transform.position =  Vector3.MoveTowards(transform.position,Chair.chairInstance.sittingTransform.position, Runner.DeltaTime * 50f);
                    //transform.position = Chair.chairInstance.sittingTransform.position;
                    characterAnimator.SetBool("Sit", true);
                    RPC_SendMessage(sit);
                }
                
            }

            if (data.stand)
            {
                sit = false;
                stand = true;
                characterAnimator.SetBool("Stand", true);
                Debug.Log("stand animation will run");
                //characterAnimator.SetBool("Stand", true);
            }
            */

            
        }
    }
    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SendMessage(NetworkBool isSitting)
    {
        isSitting = sit;
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