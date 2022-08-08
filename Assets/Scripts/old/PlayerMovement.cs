using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float meleeAttackSpeed;

    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    private float moveZ, moveX;

    //References
    private CharacterController controller;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        if (isGrounded && velocity.y <0)
        {
            //stop aplying gravity if grounded
            velocity.y = -2f;
        }
        moveZ = Input.GetAxis("Vertical");
        moveX = Input.GetAxis("Horizontal");
        moveDirection = new Vector3(moveX, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);
        if (isGrounded)
        {
            if(moveDirection != Vector3.zero)
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    //Run
                    Run();
                }
                else
                {
                    //walk
                    Walk();
                }

            }
            else
            {
                //idle
                Idle();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                MeleeAttack();
            }
        }

        moveDirection *= moveSpeed ;
        controller.Move(moveDirection *Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    private void MeleeAttack()
    {
        animator.SetTrigger("MeleeAttack");
        moveSpeed = meleeAttackSpeed;
    }
    private void Idle()
    {
        animator.SetFloat("ForwardSpeed", 0, 0.1f, Time.deltaTime);
        animator.SetFloat("SideSpeed", 0, 0.1f, Time.deltaTime);
    }
    private void Walk()
    {
        moveSpeed = walkSpeed;
        animator.SetFloat("ForwardSpeed", 1f * moveZ, 0.1f, Time.deltaTime);
        animator.SetFloat("SideSpeed", 1f * moveX, 0.1f, Time.deltaTime);


    }
    private void Run()
    {
        moveSpeed = runSpeed;
        animator.SetFloat("ForwardSpeed", 2f * moveZ, 0.1f, Time.deltaTime);
        animator.SetFloat("SideSpeed", 2f * moveX, 0.1f, Time.deltaTime);
    }
    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        animator.SetTrigger("Jump");
    }
}
