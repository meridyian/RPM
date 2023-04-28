using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float speedSmoothVelocity = 0f;
    [SerializeField] private float speedSmoothTime = 0.1f;

    public float currentSpeed = 0f;
    private CharacterController charController;
    private Animator anim;
    private float targetSpeed = 0f;

    public void Start()
    {
        charController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    public void Update()
    {
        Move();

        if(Input.GetKey("T"))
        {
            anim.SetBool("Talk", true);
        }
    }

    public void Move()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        targetSpeed = walkSpeed * inputMagnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
        charController.Move(movementDirection * currentSpeed * Time.deltaTime);
        Debug.Log(inputMagnitude);
        movementDirection.Normalize();
        if (inputMagnitude > 0)
        {
            anim.SetBool("Walk", true);
        }
        
       
    }
    
}
