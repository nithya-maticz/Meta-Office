using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem.XR;
using NUnit.Framework;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour
{
    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;
    [SerializeField] GameObject cameraHolder;
    float verticalLookRotation;
    bool grounded;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;
    Rigidbody rb;
    PhotonView PV;
    public Animator animatorRef;
   
    public List<GameObject> charRef;
   


    /// <summary>


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
      
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);

        }
        if(PV.IsMine)
        {
            for(int i=0;i<= charRef.Count;i++)
            {
                charRef[i].GetComponent<SkinnedMeshRenderer>().enabled = false;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
            return;
        Look();
        Move();
        Jump();
        Dance();
        
    }
    void Look()
    {
       
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);
        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
       cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;

    }
    void Move()
    {
        

        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
         if(moveDir != Vector3.zero)
         {
            
             animatorRef.SetBool("isWalking", true);
         }
         else
         {
             
             animatorRef.SetBool("isWalking", false);
         }

        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
    }
    void Jump()
    {
      
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
          
            rb.AddForce(transform.up * jumpForce);
        }
    }
    void Dance()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            animatorRef.SetTrigger("Dancing");
        }
    }
    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;

    }
    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;

        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);

    }
}
